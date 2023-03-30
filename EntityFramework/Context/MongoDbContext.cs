using Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EntityFramework.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var settings2 = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
            var client = new MongoClient(settings2);
            _database = client.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.Trim());
        }
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
