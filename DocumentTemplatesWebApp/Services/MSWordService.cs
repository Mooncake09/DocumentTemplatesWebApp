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
    /// <summary>
    /// Возвращает текстовое содержимое документа
    /// </summary>
    /// <param name="fileName">имя документа</param>
    /// <returns>текстовое содержимое документа fileName</returns>
    public override string GetFileText(string fileName) 
    {
        var filePath = Path.Combine(_settings.SavedFilesDirPath, fileName);
        using (var document = DocX.Load(filePath)) {
            var result = document.Text;
            return result;
        }
    }

    /// <summary>
    /// Генерирует документ из полученных данных
    /// </summary>
    /// <param name="template">Имя шаблона из которого будет генерировать готовый документ</param>
    /// <param name="replacePatterns">Dictionary, где ключ - это название параметра в шаблоне</param>
    /// <returns>Название сгенерированного документа</returns>
    public override async Task<string> GenerateDocument(string template, Dictionary<string, string> replacePatterns)
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

                return Path.GetFileName(newDocPath);
            }
        }
        catch(Exception e)
        {
           await _logger.LogError(template, e.Message);
           throw;
        }
    }
}