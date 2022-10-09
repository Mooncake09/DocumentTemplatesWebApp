using DocumentTemplatesWebApp.Models;
using MongoDB.Driver;

namespace DocumentTemplatesWebApp.Services;

public class MongoLoggerService 
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<MongoLog> _logCollection;
    private readonly Settings _settings;

    public MongoLoggerService(Settings settings) 
    {
        _settings = settings;
        _mongoClient = new MongoClient(_settings.Mongo.ConnectionString);
        _database = _mongoClient.GetDatabase(_settings.Mongo.Database);
        _logCollection = _database.GetCollection<MongoLog>(_settings.Mongo.Collection);
    }

    public async Task Log(string documentName) 
    {
        var log = new MongoLog(documentName, true);
        await _logCollection.InsertOneAsync(log);
    }

    public async Task LogError(string documentName, string errorMessage) 
    {
        var log = new MongoLog(documentName, false, errorMessage);
        await _logCollection.InsertOneAsync(log);
    }
}