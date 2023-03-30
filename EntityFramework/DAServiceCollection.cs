using Core.Abstract;
using EntityFramework.Abstract;
using EntityFramework.Concrete;
using EntityFramework.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    public static class DAServiceCollection
    {
        public static IServiceCollection AddEntityFrameworkCollection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepositoryBase<>));
            services.AddScoped<ITokenDataAccess, TokenDataAccess>();
            services.AddScoped<ICustomerDataAccess, CustomerDataAccess>();
            return services;
        }
    }
}
