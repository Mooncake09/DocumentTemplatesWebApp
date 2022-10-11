public class Settings {
    public string TemplatesDirPath { get; set; }
    public string SavedFilesDirPath { get; set; }
    public DocumentSettings[] Documents { get; set; }
    public MongoDBSettings Mongo { get; set; }
}

public class DocumentSettings 
{
    public string Title { get; set; }
    public string Template { get; set; }
    public TemplateFieldInfo[] TemplateFields { get; set; }
}

public class TemplateFieldInfo 
{
    public string Type { get; set; }
    public string Title { get; set; }
    public string Pattern { get; set; }
}

public class MongoDBSettings
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
    public string Collection { get; set; }
}