using Business.DTO.BaseObjects;
using Business.DTO.Material;

namespace Business.IMeneger
{
    public interface IMaterialManager
    {
        ClientResult addMaterial(addUpdateMaterialRequest request);
        ClientResult updateMaterial(addUpdateMaterialRequest request);
        ClientResult deleteMaterial(getOneRequest request);
        ClientResult<getOneMaterialResponse> getOneMaterial(getOneRequest request);
        ClientResult<getAllMaterialResponse> getAllMaterial(dataTableRequest request);
    }
}
