using Business.DTO.BaseObjects;
using Business.IMeneger;
using Business.Meneger;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCore.Infrastructure
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddBusinesCollection(this IServiceCollection services)
        {
            services.AddScoped<ITokenControl, TokenControl>();
            services.AddScoped<IClientContext, ClientContext>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IBasketManager, BasketManager>();
            services.AddScoped<IBrandManager, BrandManager>();

            return services;
        }
    }
}