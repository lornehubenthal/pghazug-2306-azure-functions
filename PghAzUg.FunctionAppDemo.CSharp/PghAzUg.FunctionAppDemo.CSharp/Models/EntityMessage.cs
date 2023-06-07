namespace PghAzUg.FunctionAppDemo.CSharp.Models;

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
    public bool EatenByArmus { get; set; }
}