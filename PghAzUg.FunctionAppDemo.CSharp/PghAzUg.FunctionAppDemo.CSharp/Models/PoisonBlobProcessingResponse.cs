using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;

namespace PghAzUg.FunctionAppDemo.CSharp.Models;

public class PoisonBlobProcessingResponse
{
    [SendGridOutput] public PoisonBlobMessage Message { get; set; } = new();
}

public class PoisonBlobMessage
{
    public EmailAddress? From { get; set; }
    public IEnumerable<Personalization> Personalizations { get; set; }  = Enumerable.Empty<Personalization>();
    public string? Subject { get; set; }
    [JsonPropertyName("content")] public IEnumerable<Content> Contents { get; set; } = Enumerable.Empty<Content>();
}

public class Personalization
{
    [JsonPropertyName("to")] public IEnumerable<EmailAddress> Tos { get; set; } = Enumerable.Empty<EmailAddress>();
}

public class EmailAddress
{
    public EmailAddress()
    {
    }

    public EmailAddress(string email, string? name = null)
    {
        Email = email;
        Name = name ?? string.Empty;
    }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class Content
{
    public Content()
    {
    }

    public Content(string value, string type = "plain/text")
    {
        Type = type;
        Value = value;
    }

    public string Type { get; set; } = "text/plain";
    public string? Value { get; set; }
}
