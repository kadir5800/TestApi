using Business.DTO.BaseObjects;
using Business.DTO.City;

namespace Business.IMeneger
{
    public interface ICityManager
    {
        ClientResult addCity(addUpdateCityRequest request);
        ClientResult updateCity(addUpdateCityRequest request);
        ClientResult deleteCity(getOneRequest request);
        ClientResult<getOneCityResponse> getOneCity(getOneRequest request);
        ClientResult<getAllCityResponse> getAllCity(dataTableRequest request);
    }
}
