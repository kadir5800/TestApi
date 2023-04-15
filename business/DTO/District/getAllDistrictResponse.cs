using Business.DTO.BaseObjects;

namespace Business.DTO.District
{
    public class getAllDistrictResponse:dataTableResponse
    {
        public List<getOneDistrictResponse> data { get; set; }
    }
}
