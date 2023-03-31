using Business.DTO.BaseObjects;
using Business.DTO.Customer;
using Business.IMeneger;
using EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Meneger
{
    public class CustomerManager : ZServerService, ICustomerManager
    {
        private readonly ICustomerDataAccess _customerDataAccess;

        public CustomerManager(IServiceProvider serviceProvider, ICustomerDataAccess customerDataAccess) : base(serviceProvider)
        {
            _customerDataAccess = customerDataAccess;
        }

        public ClientResult addCustomer(addCustomerRequest request)
        {
            var existingcustomer = _customerDataAccess.GetById(request.Id);
            if (existingcustomer.Entity == null || existingcustomer.Success == false)
            {
                return Error(message: "Kullanıcı Bulunamadı");
            }
            existingcustomer.Entity.Email = request.Email;
            existingcustomer.Entity.Addres = request.Addres;
            existingcustomer.Entity.Name = request.Name;
            existingcustomer.Entity.Surname = request.Surname;
            existingcustomer.Entity.Phone = request.Phone;
            existingcustomer.Entity.Phone2 = request.Phone2;
            var update = _customerDataAccess.ReplaceOne(existingcustomer.Entity,existingcustomer.Entity.Id.ToString());
            if (!update.Success)
            {
                return Error(message: update.Message);
            }
            return Success(message: "Başarılı");

        }
    }
}
