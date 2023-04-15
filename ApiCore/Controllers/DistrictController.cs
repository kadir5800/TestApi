using Business.DTO.BaseObjects;
using Business.DTO.District;
using Business.IMeneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictManager _districtManager;

        public DistrictController(IDistrictManager districtManager)
        {
            _districtManager = districtManager;
        }
        /// <summary>
        /// İlçe ekler 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addDistrict")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addDistrict([FromBody] addUpdateDistrictRequest request)
        {
            var response = _districtManager.addDistrict(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// İlçe günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateDistrict")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult updateDistrict([FromBody] addUpdateDistrictRequest request)
        {
            var response = _districtManager.updateDistrict(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// İlçe günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteDistrict")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteDistrict([FromBody] getOneRequest request)
        {
            var response = _districtManager.deleteDistrict(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Tüm İlçeları getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllDistrict")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getAllDistrictResponse>), 200)]
        public JsonResult getAllDistrict([FromBody] dataTableRequest request)
        {
            var response = _districtManager.getAllDistrict(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// İlçe getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneDistrict")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneDistrictResponse>), 200)]
        public JsonResult getOneDistrict([FromBody] getOneRequest request)
        {
            var response = _districtManager.getOneDistrict(request);
            return new JsonResult(response);
        }
    }
}
