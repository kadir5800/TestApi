using Business.DTO.BaseObjects;
using Business.IMeneger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Meneger
{
    public class ZServerService : IZServerService
    {
        protected readonly IClientContext ClientContext;

        protected readonly IServiceProvider ServiceProvider;
        protected ZServerService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ClientContext = ServiceProvider.GetService<IClientContext>();
        }
        protected ClientResult Success(string message = default(string), object data = default(object), int code = 200)
        {
            var cr = new ClientResult
            {
                Success = true,
                Code = code,
                Message = message,
                Data = data
            };

            return cr;
        }

        protected ClientResult<T> Success<T>(string message = default(string), T data = default(T), int code = 200)
        {
            var cr = new ClientResult<T>
            {
                Success = true,
                Code = code,
                Message = message,
                Data = data
            };

            return cr;
        }

        protected ClientResult Error(string message = default(string), object data = default(object), int code = 500)
        {
            var cr = new ClientResult
            {
                Success = false,
                Code = code,
                Message = message,
                Data = data
            };

            return cr;
        }

        protected ClientResult<T> Error<T>(string message = default(string), T data = default(T), int code = 500)
        {
            var cr = new ClientResult<T>
            {
                Success = false,
                Code = code,
                Message = message,
                Data = data
            };

            return cr;
        }
    }
}
