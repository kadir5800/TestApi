using Business.DTO.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Campaign
{
    public class getAllCampaignResponse:dataTableResponse
    {
        public List<getOneCampaignResponse> data { get; set; }
    }
}
