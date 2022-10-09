using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace DocumentTemplatesWebApp.Models;

public class MongoLog 
{

    public MongoLog(string documentName, bool isSuccess)
    {
        Timestamp = DateTime.Now;
        DocumentName = documentName;
        IsSuccess = isSuccess;
    }

    public MongoLog(string documentName, bool isSuccess, string message) : this(documentName, isSuccess)
    {
        Message = message;    
    }

    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Timestamp { get; set; }
    [BsonRepresentation(BsonType.String)]
    public string DocumentName { get; set; }
    [BsonRepresentation(BsonType.Boolean)]
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}