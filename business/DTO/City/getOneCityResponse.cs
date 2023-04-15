using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.City
{
    public class getOneCityResponse:addUpdateCityRequest
    {
        public DateTime CreationDate { get; set; }
    }
}
