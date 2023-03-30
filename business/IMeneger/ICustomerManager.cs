using Business.DTO.BaseObjects;
using Business.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IMeneger
{
    public interface ICustomerManager
    {
        ClientResult addCustomer(addCustomerRequest request);
    }
}
