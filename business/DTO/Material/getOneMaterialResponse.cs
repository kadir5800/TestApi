using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Material
{
    public class getOneMaterialResponse:addUpdateMaterialRequest
    {
        public DateTime CreationDate { get; set; }
    }
}
