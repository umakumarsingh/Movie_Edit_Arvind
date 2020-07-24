using MongoDB.Driver;

namespace MoviePreFSEmaster.DataLayer
{
  public  interface IMongoDBContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
