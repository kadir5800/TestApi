using ApiCore.Infrastructure.Middleware;
using ApiCore.Model.service;

namespace ApiCore.Infrastructure
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddEntityFrameworkCollection(this IServiceCollection services)
        {

            services.AddScoped<IFakeResponse, FakeResponse>();
            services.AddScoped<IClientContext, ClientContext>();
            return services;
        }
    }
}
