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
        ClientResult<getOneCustomerResponse> getOneCustomer(getOneCustomerRequest request);
        ClientResult<getAllCustomerResponse> getAllCustomer(getAllCustomerRequest request);
        ClientResult delteCustomer(getOneRequest request);
    }
}
