using Core.Settings;
using EntityFramework.Abstract;
using EntityFramework.Context;
using EntityFramework.Repository;
using Entitys.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EntityFramework.Concrete
{
    public class TokenDataAccess : MongoRepositoryBase<Token>, ITokenDataAccess
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Token> _collection;
        public TokenDataAccess(IOptions<MongoSettings> settings) : base(settings)
        {
            _context=new MongoDbContext(settings);
            _collection=_context.GetCollection<Token>();
        }

    }
}
