using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebsiteServerApp.DataAccess.Models.Base;

/// <summary>
/// Base DB model.
/// </summary>
public abstract class BaseModel
{
    /// <summary>
    /// The identifier field in the DB.
    /// </summary>
    [BsonId]
    public ObjectId Id { get; set; }

    protected BaseModel()
    {
        Id = ObjectId.GenerateNewId();
    }

    protected BaseModel(ObjectId id)
    {
        Id = id;
    }
}
