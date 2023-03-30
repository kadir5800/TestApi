using Azure;
using Business.DTO.BaseObjects;
using Business.DTO.Login;
using Business.IMeneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public LoginController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager=authenticationManager;
        }
        /// <summary>
        /// Yeni Kullanıcı Ekler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult Register([FromBody] registerRequest request)
        {
            var response = _authenticationManager.Register(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Giriş Yapar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<loginResponse>), 200)]
        public JsonResult Login([FromBody] loginRequest request)
        {
            var response = _authenticationManager.login(request);
            return new JsonResult(response);
        }
    }
}
