using Core.Abstract;
using EntityFramework.Abstract;
using EntityFramework.Concrete;
using EntityFramework.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework
{
    public static class DAServiceCollection
    {
        public static IServiceCollection AddEntityFrameworkCollection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepositoryBase<>));
            services.AddScoped<ITokenDataAccess, TokenDataAccess>();
            services.AddScoped<IBasketDataAccess, BasketDataAccess>();
            services.AddScoped<IBrandDataAccess, BrandDataAcces>();
            services.AddScoped<ICampaignDataAccess, CampaignDataAcces>();
            services.AddScoped<ICategoryDataAccess, CategoryDataAccess>();
            services.AddScoped<ICityDataAccess, CityDataAccess>();
            services.AddScoped<IColorDataAccess, ColorDataAccess>();
            services.AddScoped<ICustomerDataAccess, CustomerDataAccess>();
            services.AddScoped<IDistrictDataAccess, DistrictDataAccess>();
            services.AddScoped<IFavorityDataAccess, FavorityDataAccess>();
            services.AddScoped<IImageDataAccess, ImageDataAccess>();
            services.AddScoped<IMaterialDataAccess, MaterialDataAccess>();
            services.AddScoped<IModelDataAccess, ModelDataAccess>();
            services.AddScoped<IOrderDataAccess, OrderDataAccess>();
            services.AddScoped<IShoesDataAccess, ShoesDataAccess>();
            return services;
        }
    }
}
