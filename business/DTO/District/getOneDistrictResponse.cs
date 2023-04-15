using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.District
{
    public class getOneDistrictResponse:addUpdateDistrictRequest
    {
        public DateTime CreationDate { get; set; }
    }
}
