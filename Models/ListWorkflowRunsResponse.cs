using Newtonsoft.Json;

public class ListWorkflowRunsResponse
{
    [JsonProperty(PropertyName = "workflow_runs")]
    public List<WorkflowRun> WorkflowRuns { get; set; } = new();
}