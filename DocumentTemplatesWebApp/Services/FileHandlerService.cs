namespace DocumentTemplatesWebApp.Services;

public abstract class FileHandlerService
{
    protected readonly Settings _settings;
    public FileHandlerService(Settings settings)
    {
        _settings = settings;
        EnsureFilesDirExsist();
    }

    public abstract string GetFileText(string fileName);
    public abstract void GenerateDocument(string template, Dictionary<string, string> content);

    private void EnsureFilesDirExsist() 
    {
        var savedFilesDirPath = _settings.SavedFilesDirPath;

        if (!Directory.Exists(savedFilesDirPath))
            Directory.CreateDirectory(savedFilesDirPath);

        var templatesDirPath = _settings.TemplatesDirPath;
        if (!Directory.Exists(templatesDirPath))
            throw new DirectoryNotFoundException($"Directory {templatesDirPath} not exsist");

        var filePaths = Directory.GetFiles(templatesDirPath);
        if (filePaths.Length == 0)
            throw new FileLoadException($"The directory {templatesDirPath} does not contain any template files");
    }
}