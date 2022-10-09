using System.Text.RegularExpressions;
using Xceed.Words.NET;
using Xceed.Document.NET;
using DocumentTemplatesWebApp.Models;

namespace DocumentTemplatesWebApp.Services;

public class MSWordService : FileHandlerService
{
    public MSWordService(Settings settings, MongoLoggerService logger) : base(settings, logger)
    {

    }

    public override string GetFileText(string fileName) 
    {
        var filePath = Path.Combine(_settings.TemplatesDirPath, fileName);
        using (var document = DocX.Load(filePath)) {
            var result = document.Text;
            return result;
        }
    }

    public override async Task GenerateDocument(string template, Dictionary<string, string> replacePatterns)
    {
        try 
        {
            var filePath = Path.Combine(_settings.TemplatesDirPath, template);
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
                var newDocPath = Path.Combine(_settings.SavedFilesDirPath, $"Vacation-{DateTime.Now.ToString("yyyy-MM-dd HH-MM-ss")}");
                document.SaveAs(newDocPath);
                await _logger.Log(template);
            }
        }
        catch(Exception e)
        {
           await _logger.LogError(template, e.Message);
           throw;
        }
    }
}