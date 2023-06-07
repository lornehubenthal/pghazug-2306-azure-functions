using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PghAzUg.FunctionAppDemo.CSharp.Models;

namespace PghAzUg.FunctionAppDemo.CSharp;

public static class LogCrewMembers
{
   
    [Function("LogCrewMembers")]
    public static LogCrewMemberResponse LogCrewMember([ServiceBusTrigger("process-entity", Connection = "AzureServiceBus")] EntityMessage message, FunctionContext context)
    {
        ILogger logger = context.GetLogger("LogCrewMembers");
        logger.LogInformation("C# ServiceBus queue trigger function processed message for Job ID: {JobID} Crew Name: {Name}", message.JobId, message.Data?.Name);

        if (message.Data == null)
            throw new Exception("Could not process message.");
        
        if (message.Data.EatenByArmus)
            throw new Exception("Crew Member not available.");
        
        return new LogCrewMemberResponse { CrewMember = message.Data };
    }
    
    [Function("LogCrewMembersError")]
    public static LogCrewMemberErrorResponse LogCrewMemberError([ServiceBusTrigger("process-entity/$DeadLetterQueue", Connection = "AzureServiceBus")] EntityMessage message, FunctionContext context)
    {
        ILogger logger = context.GetLogger("LogCrewMembers");
        logger.LogInformation("C# ServiceBus dead letter queue trigger function processed message for Job ID: {JobID} Crew Name: {Name}", message.JobId, message.Data?.Name);

        return new LogCrewMemberErrorResponse { CrewMember = message.Data };
    }
}
