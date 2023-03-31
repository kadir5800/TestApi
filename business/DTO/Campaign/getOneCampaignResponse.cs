using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Campaign
{
    public class getOneCampaignResponse:addUpdateCampaignRequest
    {
        public DateTime CreationDate { get; set; }
    }
}
