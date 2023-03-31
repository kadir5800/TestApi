using Business.DTO.BaseObjects;
using Business.DTO.Basket;
using Business.DTO.Customer;
using Business.IMeneger;
using Business.Meneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketManager _basketManager;

        public BasketController(IBasketManager basketManager)
        {
            _basketManager = basketManager;
        }
        /// <summary>
        /// Sepet ekler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addBasket")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addBasket([FromBody] addBasketRequest request)
        {
            var response = _basketManager.addBasket(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Sepet siler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteBasket")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteBasket([FromBody] getOneRequest request)
        {
            var response = _basketManager.deleteBasket(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Sepet getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneBasket")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult getOneBasket([FromBody] getOneRequest request)
        {
            var response = _basketManager.getOneBasket(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// kullanıcıya ayit Sepet getiri
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getCustomerBaskets")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult getCustomerBaskets([FromBody] getOneRequest request)
        {
            var response = _basketManager.getCustomerBaskets(request);
            return new JsonResult(response);
        }
    }
}
