using Business.DTO.BaseObjects;
using Business.DTO.Login;
using Business.IMeneger;
using EntityFramework.Abstract;
using Entitys.Abstract;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Business.Meneger
{
    public class AuthenticationManager : ZServerService, IAuthenticationManager
    {
        private readonly ITokenDataAccess _tokenDataAccess;
        private readonly ICustomerDataAccess _customerDataAccess;

        public AuthenticationManager(IServiceProvider serviceProvider, ITokenDataAccess tokenDataAccess, ICustomerDataAccess customerDataAccess) : base(serviceProvider)
        {
            _tokenDataAccess=tokenDataAccess;
            _customerDataAccess=customerDataAccess;
        }

        public ClientResult<loginResponse> login(loginRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return Error<loginResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var password = MD5Hash(request.Password);
            var existingLogin = _customerDataAccess.FilterBy(f => f.UserName==request.UserName && f.Password== password);
           
            if (existingLogin.Success==false || existingLogin.Result==null)
            {
                return Error<loginResponse>(message: "Kullanıcı Adı Veya Şifre Yanlış");
            }
            var user = existingLogin.Result.First();
            var existingtoken=_tokenDataAccess.FilterBy(f=> f.CustomerId==user.Id.ToString()).Result.First();
            if (existingtoken.TokenDate <=DateTime.Now.AddDays(-7))
            {
                existingtoken.token=Guid.NewGuid().ToString();
            }
            var response = new loginResponse()
            {
                Id=user.Id.ToString(),
                Token=existingtoken.token,
            };
            return Success<loginResponse>(message: "Başarılı", data: response);
        }

        public ClientResult Register(registerRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            if (request.Password!=request.RePassword)
            {
                return Error(message: "Şifreler Uyuşmuyor");
            }
            var pass = MD5Hash(request.Password);
            var ownUser = _customerDataAccess.FilterBy(f => f.UserName==request.UserName).Result.Count();
            if (ownUser > 0)
            {
                return Error(message: "Var olan kullanıcı adı");
            }
            var data = _customerDataAccess.InsertOne(new Customer()
            {
                UserName=request.UserName,
                Password=pass,
                CreationDate=DateTime.Now,
                IsDeleted=false,
               
            });
            _tokenDataAccess.InsertOne(new Token()
            {
                IsDeleted=false,
                CustomerId=data.Entity.Id.ToString(),
                TokenDate=DateTime.Now,
                token=Guid.NewGuid().ToString(),
            });
            if (data.Success==false)
            {
                return Error(message: data.Message);
            }
            return Success(message: "Başarılı");
        }
        public static string MD5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
