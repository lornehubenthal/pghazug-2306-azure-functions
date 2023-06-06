using Microsoft.Azure.Functions.Worker;

namespace PghAzUg.FunctionAppDemo.CSharp.Models;

public class ProcessBlobResponse
{
    [ServiceBusOutput("process-entity", Connection = "AzureServiceBus")]
    public IEnumerable<EntityMessage> Messages { get; set; } = Enumerable.Empty<EntityMessage>();
}
