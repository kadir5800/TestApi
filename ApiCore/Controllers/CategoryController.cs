using Business.DTO.BaseObjects;
using Business.DTO.Category;
using Business.IMeneger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        /// <summary>
        /// Kategori ekler 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addCategory")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult addCategory([FromBody] addCategoryRequest request)
        {
            var response = _categoryManager.addCategory(request);
            return new JsonResult(response);
        }

        /// <summary>
        /// Kategori günceller 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateCategory")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult updateCategory([FromBody] addCategoryRequest request)
        {
            var response = _categoryManager.updateCategory(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Kategori Siler 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteCategory")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult), 200)]
        public JsonResult deleteCategory([FromBody] getOneRequest request)
        {
            var response = _categoryManager.deleteCategory(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Tüm Kategoriları getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllCategory")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getAllCategoryResponse>), 200)]
        public JsonResult getAllCategory([FromBody] dataTableRequest request)
        {
            var response = _categoryManager.getAllCategory(request);
            return new JsonResult(response);
        }
        /// <summary>
        /// Kategori getirir
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getOneCategory")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ClientResult<getOneCategoryResponse>), 200)]
        public JsonResult getOneCategory([FromBody] getOneRequest request)
        {
            var response = _categoryManager.getOneCategory(request);
            return new JsonResult(response);
        }
    }
}
