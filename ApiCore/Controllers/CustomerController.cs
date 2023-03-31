using Business.DTO.BaseObjects;
using Business.DTO.Customer;
using Business.DTO.Login;
using Business.IMeneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager=customerManager;
        }
        /// <summary>
        /// Kullanıcı Bilgilerini Günceller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addUpdateCustomer")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addUpdateCustomer([FromBody] addCustomerRequest request)
        {
            var response = _customerManager.addCustomer(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Kullanıcı Bilgilerini Getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneCustomer")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneCustomerResponse>), 200)]
        public JsonResult getOneCustomer([FromBody] getOneCustomerRequest request)
        {
            var response = _customerManager.getOneCustomer(request);
            return new JsonResult(response);
        }

    }
}
