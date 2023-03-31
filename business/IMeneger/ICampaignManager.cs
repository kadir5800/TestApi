using Business.DTO.BaseObjects;
using Business.DTO.Campaign;

namespace Business.IMeneger
{
    public interface ICampaignManager
    {
        ClientResult addCampaign(addUpdateCampaignRequest request);
        ClientResult updateCampaign(addUpdateCampaignRequest request);
        ClientResult deleteCampaign(getOneRequest request);
        ClientResult<getOneCampaignResponse> getOneCampaign(getOneRequest request);
        ClientResult<getAllCampaignResponse> getAllCampaign(dataTableRequest request);
    }
}
