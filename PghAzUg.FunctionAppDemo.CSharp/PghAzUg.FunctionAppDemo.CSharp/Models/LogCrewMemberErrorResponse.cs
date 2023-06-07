using Microsoft.Azure.Functions.Worker;

namespace PghAzUg.FunctionAppDemo.CSharp.Models;

public class LogCrewMemberErrorResponse
{
    [BlobOutput("failed/{jobId}/{data.name}.json")]
    public Entity? CrewMember { get; set; }
}
