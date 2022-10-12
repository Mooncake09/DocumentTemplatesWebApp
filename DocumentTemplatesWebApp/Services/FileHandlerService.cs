namespace DocumentTemplatesWebApp.Services;

public abstract class FileHandlerService
{
    protected readonly Settings _settings;
    protected readonly MongoLoggerService _logger;
    public FileHandlerService(Settings settings, MongoLoggerService logger)
    {
        _settings = settings;
        _logger = logger;
        EnsureFilesDirExsist();
    }

    public abstract string GetFileText(string fileName);
    public abstract Task<string> GenerateDocument(string template, Dictionary<string, string> content);
    
    /// <summary>
    /// Получить список всех доступных шаблонов документов
    /// </summary>
    /// <returns>Список всех шаблонов документов</returns>
    public IEnumerable<string> GetTemplatesList()
    {
        var templates = Directory.GetFiles(_settings.TemplatesDirPath);
        var files = templates.Select(item => Path.GetFileNameWithoutExtension(item));
        return files;
    }
    /// <summary>
    /// Возвращает список всех сохраненных документов
    /// </summary>
    /// <returns>Список сохраненных файлов</returns>
    public IEnumerable<string> GetSavedFilesList() 
    {
        var files = Directory.GetFiles(_settings.SavedFilesDirPath);
        return files.Select(file => Path.GetFileNameWithoutExtension(file));
    }
    /// <summary>
    /// Проверяет существуют ли нужные для работы приложения директории
    /// </summary>
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