// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization.Metadata;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using valeo_build_light.Models;

var personalAccessToken = Environment.GetEnvironmentVariable("VBL_GH_PAT");
var hueId = "ecb5fafffe0c17ef";

const string githubRoot = "https://api.github.com/repos/valeofinancial/vtap";
const string userAgent = "Valeo Build Light v0.1";
const string HueApplicationKey = "xkKXaHW2KNqQPz7AwoQFJoLwvPY4hLLjtkdsvNUo";
const string DeviceIp = "10.1.3.49";
const string DeviceUrl = $"https://{DeviceIp}/clip/v2/resource/light/39dfff2b-d1d6-4c7e-8252-9c3ccb7cf650";

async Task<List<Workflow>> GetWorkflows()
{
    var workflows = await githubRoot
        .AppendPathSegments("actions", "workflows")
        .WithHeader("Authorization", $"Bearer {personalAccessToken}")
        .WithHeader("User-Agent", userAgent)
        .GetJsonAsync<ListWorkflowsResponse>();
    return workflows.Workflows;
}

async Task<List<WorkflowRun>> GetWorkflowRuns(long workflowId)
{
    var workflows = await githubRoot
        .AppendPathSegments("actions", "workflows", workflowId, "runs")
        .WithHeader("Authorization", $"Bearer {personalAccessToken}")
        .WithHeader("User-Agent", userAgent)
        .SetQueryParam("branch", "main")
        .SetQueryParam("per_page", "1")
        .GetJsonAsync<ListWorkflowRunsResponse>();
    return workflows.WorkflowRuns;
}

async Task<BuildLightStatus> GetStatusForWorkflow(long workflowId)
{
    var runs = await GetWorkflowRuns(workflowId);
    if (runs.FirstOrDefault() is { } run)
    {
        if (run.Status != "completed")
        {
            return BuildLightStatus.Building;
        }
        if (run.Conclusion != "success")
        {
            return BuildLightStatus.Failure;
        }
    }

    return BuildLightStatus.Success;
}

BuildLightStatus DemuxStatuses(List<BuildLightStatus> statuses)
{
    if (statuses.Contains(BuildLightStatus.Building))
    {
        return BuildLightStatus.Building;
    }

    if (statuses.Contains(BuildLightStatus.Failure))
    {
        return BuildLightStatus.Failure;
    }

    return BuildLightStatus.Success;
}


Task<string> GetHueIpAddress()
{
    //var devices = await "https://discovery.meethue.com"
    //    .GetJsonAsync<List<HueDeviceIpRecord>>();
    //var ours = devices.Single(device => device.Id.ToLowerInvariant() == hueId.ToLowerInvariant()).IpAddress;

    FlurlHttp.ConfigureClient($"https://{DeviceIp}", cli =>
        cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());

    //var services = await $"https://{ours}"
        
    //    .AppendPathSegments("clip", "v2", "resource", "device")
    //    .WithHeader("hue-application-key", HueApplicationKey)
    //    .GetJsonAsync<HueResourceDeviceResponse>();

    //var ourService = services.Data.SelectMany(data => data.Services).First(service => service.Rtype == "light");

    //return $"https://{ours}"
    //    .AppendPathSegments("clip", "v2", "resource", "light", ourService.Rid);

    return Task.FromResult(DeviceUrl);
}


async Task SetHueDetails(string ip, BuildLightStatus status)
{
    var hue = status switch
    {
        BuildLightStatus.Building => Hue.blue,
        BuildLightStatus.Failure => Hue.red,
        BuildLightStatus.Success => Hue.green,
        _ => Hue.red
    };

    //FlurlHttp.Configure(settings => settings.BeforeCall = call =>
    //{
    //    Console.WriteLine(call);
    //});

    await ip
        .WithHeader("hue-application-key", HueApplicationKey)
        .PutJsonAsync(new HueResourceLightRequest
        {
            Color = new(){XY = hue},
            Dimming = new(){Brightness = 40},
            On = new(){On = true}
        });
}

async Task BuildLightAlgorithm()
{
    var workflows = await GetWorkflows();

    var statuses = new List<BuildLightStatus>();
    foreach (var workflow in workflows)
    {
        statuses.Add(await GetStatusForWorkflow(workflow.Id));
    }

    var status = DemuxStatuses(statuses);
    Console.WriteLine($"Build Light Status: {status}");

    var ip = await GetHueIpAddress();

    await SetHueDetails(ip, status);
}

await BuildLightAlgorithm();

