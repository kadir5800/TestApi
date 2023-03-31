using Business.DTO.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Basket
{
    public class getAllBasketResponse:dataTableResponse
    {
        public List<getOneBasketResponse> data { get; set; }
    }
}
