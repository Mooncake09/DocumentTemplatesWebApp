namespace DocumentTemplatesWebApp.Services;

public abstract class FileHandler
{
    protected readonly Settings settings;
    public FileHandler(Settings settings)
    {
        this.settings = settings;
        EnsureFilesDirExsist();
    }

    public abstract string GetFileText(string fileName);
    public abstract void GenerateDocument(string fileName, string content);

    private void EnsureFilesDirExsist() 
    {
        var dirPath = settings.TemplatesDirPath;
        if (!Directory.Exists(dirPath))
            throw new DirectoryNotFoundException($"Directory {dirPath} not exsist");

        var filePaths = Directory.GetFiles(dirPath);
        if (filePaths.Length == 0)
            throw new FileLoadException($"The directory {dirPath} does not contain any template files");
    }
}