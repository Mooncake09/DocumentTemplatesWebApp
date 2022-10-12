namespace DocumentTemplatesWebApp.Models;

public class Request
{
    public string Template { get; set; }
    public Dictionary<string, string> Content { get; set; }
}

// public class RequestPattern {
//     public string Pattern { get; set; }
//     public string Value { get; set; }
// }