using Microsoft.Azure.Functions.Worker;

namespace PghAzUg.FunctionAppDemo.CSharp.Models;

public class ProcessBlobResponse
{
    [ServiceBusOutput("process-entity", Connection = "AzureServiceBus")]
    public IEnumerable<EntityMessage> Messages { get; set; } = Enumerable.Empty<EntityMessage>();
}

public class EntityMessage
{
    public Guid JobId { get; set; }
    public Entity? Data { get; set; }
}

public class Entity
{
    public string? Name { get; set; }
    public string? Rank { get; set; }
    public string? Position { get; set; }
    public string? Species { get; set; }
    public bool? EatenByArmus { get; set; }
}
