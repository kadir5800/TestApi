using Business.DTO.BaseObjects;
using Business.DTO.Material;
using Business.IMeneger;
using Business.Meneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialManager _materialManager;

        public MaterialController(IMaterialManager materialManager)
        {
            _materialManager = materialManager;
        }
        /// <summary>
        /// Materyal ekler 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addMaterial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addMaterial([FromBody] addUpdateMaterialRequest request)
        {
            var response = _materialManager.addMaterial(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Materyal günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateMaterial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult updateMaterial([FromBody] addUpdateMaterialRequest request)
        {
            var response = _materialManager.updateMaterial(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Materyal günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteMaterial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteMaterial([FromBody] getOneRequest request)
        {
            var response = _materialManager.deleteMaterial(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Tüm Materyalları getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllMaterial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getAllMaterialResponse>), 200)]
        public JsonResult getAllMaterial([FromBody] dataTableRequest request)
        {
            var response = _materialManager.getAllMaterial(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Materyal getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneMaterial")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneMaterialResponse>), 200)]
        public JsonResult getOneMaterial([FromBody] getOneRequest request)
        {
            var response = _materialManager.getOneMaterial(request);
            return new JsonResult(response);
        }
    }
}
