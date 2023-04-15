using Business.DTO.BaseObjects;

namespace Business.DTO.City
{
    public class getAllCityResponse:dataTableResponse
    {
        public List<getOneCityResponse> data { get; set; }
    }
}
