namespace DocumentTemplatesWebApp.Models;

public class Request
{
    public string Template { get; set; }
    public Dictionary<string, string> Content { get; set; }
}