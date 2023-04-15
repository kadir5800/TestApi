using Business.DTO.BaseObjects;

namespace Business.DTO.Color
{
    public class getAllColorResponse:dataTableResponse
    {
        public List<getOneColorResponse> data { get; set; }
    }
}
