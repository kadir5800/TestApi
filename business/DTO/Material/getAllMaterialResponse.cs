using Business.DTO.BaseObjects;

namespace Business.DTO.Material
{
    public class getAllMaterialResponse : dataTableResponse
    {
        public List<getOneMaterialResponse> data { get; set; }
    }
}
