using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Customer
{
    public class getOneCustomerResponse:addCustomerRequest
    {
        public DateTime CreationDate { get; set; }
    }
}
