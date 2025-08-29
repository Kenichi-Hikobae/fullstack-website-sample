using MongoDB.Bson;
using WebsiteServerApp.DataAccess.Models.Base;

namespace WebsiteServerApp.DataAccess.Interfaces;

/// <summary>
/// Interface that contains base data access methods for operations on the repository collection.
/// </summary>
/// <typeparam name="T">The DB model to be used on this repository.</typeparam>
public interface IMongoDBRepository<T> where T : BaseModel
{
    /// <summary>
    /// Insert a new model to the DB.
    /// </summary>
    /// <param name="document">The document model to be inserted.</param>
    /// <returns></returns>
    Task<T> InsertAsync(T document);
    /// <summary>
    /// Update an existing model in the DB.
    /// </summary>
    /// <param name="id">The id of the model to be search.</param>
    /// <param name="document">The document model to be updated with the new values.</param>
    /// <returns>The model updated.</returns>
    Task<T> UpdateAsync(ObjectId id, T document);
    /// <summary>
    /// Delete a model from the DB.
    /// </summary>
    /// <param name="id">The id of the document to be search and deleted.</param>
    /// <returns>Whether the model was deleted or not.</returns>
    Task<bool> DeleteAsync(ObjectId id);
    /// <summary>
    /// Get a model from the collection.
    /// </summary>
    /// <param name="id">The id of the document to be search.</param>
    /// <returns>The model found.</returns>
    Task<T> GetAsync(ObjectId id);
    /// <summary>
    /// Get all the model in the current collection.
    /// </summary>
    /// <returns>All the model of the collection.</returns>
    Task<IList<T>> GetAllAsync();
}
