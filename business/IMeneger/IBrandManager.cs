using Business.DTO.BaseObjects;
using Business.DTO.Brand;

namespace Business.IMeneger
{
    public interface IBrandManager
    {
        ClientResult addBrand(addUpdateBrandRequest request);
        ClientResult updateBrand(addUpdateBrandRequest request);
        ClientResult deleteBrand(getOneRequest request);
        ClientResult<getOneBrandResponse> getOneBrand(getOneRequest request);
        ClientResult<getAllBrandResponse> getAllBrand(dataTableRequest request);
    }
}
