using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PghAzUg.FunctionAppDemo.CSharp.Models;

namespace PghAzUg.FunctionAppDemo.CSharp;

public static class LogCrewMembers
{
   
    [Function("LogCrewMembers")]
    public static LogCrewMemberResponse Run([ServiceBusTrigger("process-entity", Connection = "AzureServiceBus")] EntityMessage message, FunctionContext context)
    {
        ILogger logger = context.GetLogger("LogCrewMembers");
        logger.LogInformation("C# ServiceBus queue trigger function processed message for Job ID: {JobID} Crew Name: {Name}", message.JobId, message.Data?.Name);

        return new LogCrewMemberResponse { CrewMember = message.Data };
    }
}
