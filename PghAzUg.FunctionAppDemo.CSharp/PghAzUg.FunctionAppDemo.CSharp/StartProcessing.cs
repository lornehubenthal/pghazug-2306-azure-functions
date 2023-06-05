using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PghAzUg.FunctionAppDemo.CSharp.Models;

namespace PghAzUg.FunctionAppDemo.CSharp;

public class StartProcessing
{
    private readonly ILogger<StartProcessing> _logger;

    public StartProcessing(ILogger<StartProcessing> logger) => _logger = logger;

    [Function("ProcessBlob")]
    public ProcessBlobResponse ProcessBlob(
        [BlobTrigger("submissions/{name}.{extension}", Connection = "AzureWebJobsStorage")]
        string data, string name,
        string extension,
        FunctionContext context)
    {
        _logger.LogInformation("File {Filename} of type {Extension} picked up for processing", name, extension);

        if (extension != "csv")
            throw new Exception(
                "Unsupported file uploaded.  The only file types supported at this time are of type CSV.");

        Guid jobId = Guid.NewGuid();

        return new ProcessBlobResponse
        {
            Messages = data
                .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(entityLine =>
                {
                    string[] columns = entityLine.Split(',');
                    return new EntityMessage
                    {
                        JobId = jobId,
                        Data = new Entity
                        {
                            Name = columns[0],
                            Rank = columns[1],
                            Position = columns[2],
                            Species = columns[3],
                            EatenByArmus = Convert.ToBoolean(columns[4])
                        }
                    };
                })
        };
    }

    [Function("ProcessBlobPoisonQueue")]
    public PoisonBlobProcessingResponse ProcessBlobPoisonQueue(
        [QueueTrigger("webjobs-blobtrigger-poison", Connection = "AzureWebJobsStorage")]
        PoisonBlobQueueItem item,
        DateTimeOffset insertionTime,
        FunctionContext context)
    {
        _logger.LogInformation("Poison Blob {Filename} picked up for processing.  Time inserted into Queue: {Time}",
            item.BlobName, insertionTime);

        return new PoisonBlobProcessingResponse
        {
            Message = new PoisonBlobMessage
            {
                From = new EmailAddress("noreply@lorne.dev", "Demo Notifications"),
                Personalizations =
                    new[] { new Personalization { Tos = new[] { new EmailAddress("demo-user@mailinator.com") } } },
                Subject = "Blob Processing Failed",
                Contents = new[] { new Content($"The blob {item.BlobName} failed to be processed at {insertionTime}") }
            }
        };
    }
}
