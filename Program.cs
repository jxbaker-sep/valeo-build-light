// See https://aka.ms/new-console-template for more information

using Flurl;
using Flurl.Http;

var personalAccessToken = Environment.GetEnvironmentVariable("VBL_GH_PAT");

const string githubRoot = "https://api.github.com/repos/valeofinancial/vtap";
const string userAgent = "Valeo Build Light v0.1";

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
    // TODO: change color of bulb
}

await BuildLightAlgorithm();