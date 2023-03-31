using Business.DTO.BaseObjects;
using Business.DTO.Basket;
using Business.DTO.Brand;
using Business.IMeneger;
using Business.Meneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandManager _brandManager;

        public BrandController(IBrandManager brandManager)
        {
            _brandManager = brandManager;
        }
        /// <summary>
        /// Marka ekler 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addBrand")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addBrand([FromBody] addUpdateBrandRequest request)
        {
            var response = _brandManager.addBrand(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Marka günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateBrand")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult updateBrand([FromBody] addUpdateBrandRequest request)
        {
            var response = _brandManager.updateBrand(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Marka günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteBrand")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteBrand([FromBody] getOneRequest request)
        {
            var response = _brandManager.deleteBrand(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Tüm Markaları getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllBrand")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getAllBrandResponse>), 200)]
        public JsonResult getAllBrand([FromBody] dataTableRequest request)
        {
            var response = _brandManager.getAllBrand(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Marka getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneBrand")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneBrandResponse>), 200)]
        public JsonResult getOneBrand([FromBody] getOneRequest request)
        {
            var response = _brandManager.getOneBrand(request);
            return new JsonResult(response);
        }
    }
}
