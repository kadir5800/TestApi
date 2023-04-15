using Business.DTO.BaseObjects;
using Business.DTO.District;

namespace Business.IMeneger
{
    public interface IDistrictManager
    {
        ClientResult addDistrict(addUpdateDistrictRequest request);
        ClientResult updateDistrict(addUpdateDistrictRequest request);
        ClientResult deleteDistrict(getOneRequest request);
        ClientResult<getOneDistrictResponse> getOneDistrict(getOneRequest request);
        ClientResult<getAllDistrictResponse> getAllDistrict(dataTableRequest request);
    }
}
