using Business.DTO.BaseObjects;
using Business.DTO.Brand;
using Business.DTO.Campaign;
using Business.IMeneger;
using Business.Meneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignManager _campaignManager;

        public CampaignController(ICampaignManager campaignManager)
        {
            _campaignManager = campaignManager;
        }
        /// <summary>
        /// Kampanyayı getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneCampaign")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneCampaignResponse>), 200)]
        public JsonResult getOneCampaign([FromBody] getOneRequest request)
        {
            var response = _campaignManager.getOneCampaign(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Kampanya Ekler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addCampaign")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addCampaign([FromBody] addUpdateCampaignRequest request)
        {
            var response = _campaignManager.addCampaign(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Kampanya Günceller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateCampaign")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult updateCampaign([FromBody] addUpdateCampaignRequest request)
        {
            var response = _campaignManager.updateCampaign(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Kampanya Siler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteCampaign")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteCampaign([FromBody] getOneRequest request)
        {
            var response = _campaignManager.deleteCampaign(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Tüm Kampanyaları Getiri
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllCampaign")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getAllCampaignResponse>), 200)]
        public JsonResult getAllCampaign([FromBody] dataTableRequest request)
        {
            var response = _campaignManager.getAllCampaign(request);
            return new JsonResult(response);
        }
    }
}
