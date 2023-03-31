using Business.DTO.BaseObjects;

namespace Business.DTO.Brand
{
    public class getAllBrandResponse : dataTableResponse
    {
        public List<getOneBrandResponse> data { get; set; }
    }
}
