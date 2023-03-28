using Business.DTO.BaseObjects;
using Business.DTO.Login;
using Business.IMeneger;
using EntityFramework.Context;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Meneger
{
    public class AuthenticationManager : ZServerService, IAuthenticationManager
    {
        private readonly ZDbContext _dbContext;
        protected AuthenticationManager(IServiceProvider serviceProvider, ZDbContext dbContext = null) : base(serviceProvider)
        {
            _dbContext=dbContext;
        }

        public ClientResult<loginResponse> login(loginRequest request)
        {
            if (string.IsNullOrEmpty(request.userName) || string.IsNullOrEmpty(request.Password))
            {
                return Error<loginResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var existingLogin = _dbContext.Users.FirstOrDefault(f => f.UserName==request.userName && f.Password==request.Password);
            if (existingLogin != null)
            {
                return Error<loginResponse>(message: "Kullanıcı Adı Veya Şifre Yanlış");
            }
            if (existingLogin.TokenDate <=DateTime.Now.AddDays(-7))
            {
                existingLogin.Token=Guid.NewGuid().ToString();
            }
            var response=new loginResponse()
            {
                Id=existingLogin.Id,
                Token=existingLogin.Token,
            };
            return Success<loginResponse>(message: "Başarılı",data:response);
        }
    }
}
