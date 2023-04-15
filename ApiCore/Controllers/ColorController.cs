using Business.DTO.BaseObjects;
using Business.DTO.Color;
using Business.IMeneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorManager _colorManager;

        public ColorController(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }
        /// <summary>
        /// Renk ekler 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addColor")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addColor([FromBody] addUpdateColorRequest request)
        {
            var response = _colorManager.addColor(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Renk günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateColor")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult updateColor([FromBody] addUpdateColorRequest request)
        {
            var response = _colorManager.updateColor(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Renk günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteColor")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteColor([FromBody] getOneRequest request)
        {
            var response = _colorManager.deleteColor(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Tüm Renkları getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllColor")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getAllColorResponse>), 200)]
        public JsonResult getAllColor([FromBody] dataTableRequest request)
        {
            var response = _colorManager.getAllColor(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Renk getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneColor")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneColorResponse>), 200)]
        public JsonResult getOneColor([FromBody] getOneRequest request)
        {
            var response = _colorManager.getOneColor(request);
            return new JsonResult(response);
        }
    }
}
