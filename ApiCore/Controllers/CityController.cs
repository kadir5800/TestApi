using Business.DTO.BaseObjects;
using Business.DTO.City;
using Business.IMeneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityManager _cityManager;

        public CityController(ICityManager cityManager)
        {
            _cityManager = cityManager;
        }

        /// <summary>
        /// Şehir ekler 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addCity")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addCity([FromBody] addUpdateCityRequest request)
        {
            var response = _cityManager.addCity(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Şehir günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateCity")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult updateCity([FromBody] addUpdateCityRequest request)
        {
            var response = _cityManager.updateCity(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Şehir günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteCity")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteCity([FromBody] getOneRequest request)
        {
            var response = _cityManager.deleteCity(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Tüm Şehirları getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllCity")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getAllCityResponse>), 200)]
        public JsonResult getAllCity([FromBody] dataTableRequest request)
        {
            var response = _cityManager.getAllCity(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Şehir getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneCity")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneCityResponse>), 200)]
        public JsonResult getOneCity([FromBody] getOneRequest request)
        {
            var response = _cityManager.getOneCity(request);
            return new JsonResult(response);
        }
    }
}
