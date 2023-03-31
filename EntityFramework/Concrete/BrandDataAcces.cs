using Core.Settings;
using EntityFramework.Abstract;
using EntityFramework.Context;
using EntityFramework.Repository;
using Entitys.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Concrete
{
    public class BrandDataAcces : MongoRepositoryBase<Brand>, IBrandDataAccess
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Token> _collection;
        public BrandDataAcces(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<Token>();
        }
    }
}
