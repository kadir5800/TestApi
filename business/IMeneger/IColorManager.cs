using Business.DTO.BaseObjects;
using Business.DTO.Color;

namespace Business.IMeneger
{
    public interface IColorManager
    {
        ClientResult addColor(addUpdateColorRequest request);
        ClientResult updateColor(addUpdateColorRequest request);
        ClientResult deleteColor(getOneRequest request);
        ClientResult<getOneColorResponse> getOneColor(getOneRequest request);
        ClientResult<getAllColorResponse> getAllColor(dataTableRequest request);
    }
}
