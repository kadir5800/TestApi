using Business.DTO.BaseObjects;
using Business.DTO.Category;

namespace Business.IMeneger
{
    public interface ICategoryManager
    {
        ClientResult addCategory(addCategoryRequest request);
        ClientResult updateCategory(addCategoryRequest request);
        ClientResult deleteCategory(getOneRequest request);
        ClientResult<getOneCategoryResponse> getOneCategory(getOneRequest request);
        ClientResult<getAllCategoryResponse> getAllCategory(dataTableRequest request);
    }
}
