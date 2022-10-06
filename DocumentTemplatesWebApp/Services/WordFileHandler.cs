using Xceed.Words.NET;
using Xceed.Document.NET;

namespace DocumentTemplatesWebApp.Services;

public class WordFileHandler {
    private readonly Settings settings;
    private readonly Dictionary<string, string> FilesContent;
    public WordFileHandler(Settings settings) {
        this.settings = settings;
    }

    public string GetFileContent(string fileName) {
        var filePath = Path.Combine(settings.TemplatesDirPath, fileName);
        using (var document = DocX.Load(filePath)) {
            var result = document.Text;
            return result;
        }
    }
}