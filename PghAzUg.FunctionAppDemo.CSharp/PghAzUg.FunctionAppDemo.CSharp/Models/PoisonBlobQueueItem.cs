namespace PghAzUg.FunctionAppDemo.CSharp.Models;

public class PoisonBlobQueueItem
{
    public string? Type { get; set; }
    public string? FunctionId { get; set; }
    public string? BlobType { get; set; }
    public string? ContainerName { get; set; }
    public string? BlobName { get; set; }
    public string? ETag { get; set; }
}
