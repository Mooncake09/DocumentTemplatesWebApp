public class Settings {
    public string TemplatesDirPath { get; set; }
    public string SavedFilesDirPath { get; set; }
    public MongoDBSettings Mongo { get; set; }
}

public class MongoDBSettings
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
    public string Collection { get; set; }
}