using Business.DTO.BaseObjects;
using Business.IMeneger;
using Business.Meneger;
using Core.Abstract;
using EntityFramework.Abstract;
using EntityFramework.Concrete;
using EntityFramework.Repository;
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
           
            return services;
        }
    }
}
