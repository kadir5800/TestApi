using Business.DTO.BaseObjects;
using Business.IMeneger;
using EntityFramework.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Business.Meneger
{
    public class TokenControl : ITokenControl
    {
        private readonly ITokenDataAccess _tokenDataAccess;
        private readonly IServiceProvider ServiceProvider;
        public TokenControl(IServiceProvider serviceProvider,ITokenDataAccess tokenDataAccess)
        {
            ServiceProvider = serviceProvider;
            _tokenDataAccess = ServiceProvider.GetService<ITokenDataAccess>();
        }
        public ClientObject getToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                var data = new ClientObject() { Id="", token="", Status=false };
                return data;
            }

            var isCustomer = _tokenDataAccess.FilterBy(f => f.token==token).Result.First();
            if (isCustomer==null)
            {
                var clo = new ClientObject() { Id="", token="", Status=false };
                return clo;
            }
            else
            {
                var clo = new ClientObject() { Id=isCustomer.Id.ToString(), token=isCustomer.token, Status=true };
                return clo;
            }
        }
    }
}
