using Microsoft.Azure.Functions.Worker;

namespace PghAzUg.FunctionAppDemo.CSharp.Models;

public class LogCrewMemberResponse
{
    [BlobOutput("processed/{jobId}/{data.name}.json")]
    public Entity? CrewMember { get; set; }
}
