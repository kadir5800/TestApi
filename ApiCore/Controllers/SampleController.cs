using ApiCore.Infrastructure.Middleware;
using ApiCore.Model;
using ApiCore.Model.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IFakeResponse _fakeResponse;

        public SampleController(IFakeResponse fakeResponse)
        {
            _fakeResponse=fakeResponse;
        }
        /// <summary>
        /// Login işlemi yapar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(response), 200)]
        public JsonResult Login([FromBody] getAllMemberCardRequest request)
        {
            var data=_fakeResponse.GetResponse();
            var response=new JsonResult(data);

            return response;
        }

    }
}
