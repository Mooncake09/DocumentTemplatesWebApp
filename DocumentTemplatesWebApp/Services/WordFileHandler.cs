using System.Text.RegularExpressions;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace DocumentTemplatesWebApp.Services;

public class WordFileHandler : FileHandler
{
    private readonly Dictionary<string, string> replacePatterns;
    public WordFileHandler(Settings settings) : base(settings)
    {
        replacePatterns = new Dictionary<string, string>
        {
            {"TEST", "замененный текст"}
        };
    }

    public override string GetFileText(string fileName) 
    {
        var filePath = Path.Combine(settings.TemplatesDirPath, fileName);
        using (var document = DocX.Load(filePath)) {
            var result = document.Text;
            return result;
        }
    }

    public override void GenerateDocument(string fileName, string content)
    {
        var filePath = Path.Combine(settings.TemplatesDirPath, fileName);
        using (var document = DocX.Load(filePath))
        {
            if (document.FindUniqueByPattern(@"<[\w \=]{4,}>", RegexOptions.IgnoreCase).Count > 0) 
            {
                document.ReplaceText("<(.*?)>", ReplaceFunc, false, RegexOptions.IgnoreCase);
            }
            document.Save();
        }
    }

    private string ReplaceFunc( string findStr )
    {
        if( replacePatterns.ContainsKey( findStr ) )
        {
          return replacePatterns[ findStr ];
        }
        return findStr;
    }
}