using System.Text.RegularExpressions;
using Xceed.Words.NET;
using Xceed.Document.NET;
using DocumentTemplatesWebApp.Models;

namespace DocumentTemplatesWebApp.Services;

public class WordFileHandler : FileHandler
{
    public WordFileHandler(Settings settings) : base(settings)
    {

    }

    public override string GetFileText(string fileName) 
    {
        var filePath = Path.Combine(settings.TemplatesDirPath, fileName);
        using (var document = DocX.Load(filePath)) {
            var result = document.Text;
            return result;
        }
    }

    public override void GenerateDocument(string template, Dictionary<string, string> replacePatterns)
    {
        var filePath = Path.Combine(settings.TemplatesDirPath, template);
        using (var document = DocX.Load(filePath))
        {
            if (document.FindUniqueByPattern(@"<[\w \=]{4,}>", RegexOptions.IgnoreCase).Count > 0) 
            {
                foreach (var pattern in replacePatterns) 
                {
                    document.ReplaceText("<(.*?)>", (string findStr) => 
                    {
                        if (pattern.Key.Equals(findStr.ToLower()))
                            return pattern.Value;
                        return $"<{findStr}>";
                    },
                    false, RegexOptions.IgnoreCase);
                }
            }
            document.Save();
        }
    }
}