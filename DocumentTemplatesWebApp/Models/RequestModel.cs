namespace DocumentTemplatesWebApp.Models;

public class RequestModel
{
    public string? Template { get; set; }
    public Dictionary<string, string>? Content { get; set; }
}