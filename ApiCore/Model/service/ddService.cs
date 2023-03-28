using ApiCore.Infrastructure.Middleware;

namespace ApiCore.Model.service
{
    public class ddService : IddServices
    {
        protected readonly IClientContext ClientContext;

        protected readonly IServiceProvider ServiceProvider;
        protected ddService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ClientContext = ServiceProvider.GetService<IClientContext>();
        }
    }
}
