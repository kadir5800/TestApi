using Business.DTO.BaseObjects;
using Business.DTO.Customer;
using Business.DTO.Login;
using Business.IMeneger;
using EntityFramework.Abstract;
using MongoDB.Driver;
using System.Net.Http;

namespace Business.Meneger
{
    public class CustomerManager : ZServerService, ICustomerManager
    {
        private readonly ICustomerDataAccess _customerDataAccess;

        public CustomerManager(IServiceProvider serviceProvider, ICustomerDataAccess customerDataAccess) : base(serviceProvider)
        {
            _customerDataAccess=customerDataAccess;
        }

        public ClientResult addCustomer(addCustomerRequest request)
        {
            var existingcustomer = _customerDataAccess.GetById(request.Id);
            if (existingcustomer.Entity == null || existingcustomer.Success==false)
            {
                return Error(message: "Kullanıcı Bulunamadı");
            }
            existingcustomer.Entity.Email = request.Email;
            existingcustomer.Entity.Addres=request.Addres;
            existingcustomer.Entity.Name = request.Name;
            existingcustomer.Entity.Surname = request.Surname;
            existingcustomer.Entity.Phone = request.Phone;
            existingcustomer.Entity.Phone2 = request.Phone2;
            var update = _customerDataAccess.ReplaceOne(existingcustomer.Entity, existingcustomer.Entity.Id.ToString());
            if (!update.Success)
            {
                return Error(message: update.Message);
            }
            return Success();
        }

        public ClientResult delteCustomer(getOneRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message:"Id Boş");
            }

            var deleted=_customerDataAccess.DeleteById(request.Id);

            if (!deleted.Success)
            {
                return Error(message: deleted.Message);
            }
            return Success();
        }

        public ClientResult<getAllCustomerResponse> getAllCustomer(getAllCustomerRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var customerResponse = _customerDataAccess.GetAll();
            if (!customerResponse.Success)
            {
                return Error<getAllCustomerResponse>(message: customerResponse.Message);
            }
            var customerList= customerResponse.Result.Select(s=> new getOneCustomerResponse()
            {
                Surname= s.Surname,
                Phone2= s.Phone2,
                Phone=s.Phone,
                Name= s.Name,
                Email= s.Email,
                Addres = s.Addres,
                Id =s.Id.ToString(),
            }).ToList();
            
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                customerList = customerList.Where(x => x.Addres.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Email.ToString().Contains(request.SearchValue.ToLower())
                                               || x.Name.ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Phone.ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Phone2.ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Surname.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal=customerList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn =request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "Addres":
                    customerList = sortDirection == "asc" ? customerList.OrderBy(c => c.Addres) : customerList.OrderByDescending(c => c.Addres);
                    break;
                case "Email":
                    customerList = sortDirection == "asc" ? customerList.OrderBy(c => c.Email) : customerList.OrderByDescending(c => c.Email);
                    break;
                case "Name":
                    customerList = sortDirection == "asc" ? customerList.OrderBy(c => c.Name) : customerList.OrderByDescending(c => c.Name);
                    break;
                case "Phone":
                    customerList = sortDirection == "asc" ? customerList.OrderBy(c => c.Phone) : customerList.OrderByDescending(c => c.Phone);
                    break;
                case "Phone2":
                    customerList = sortDirection == "asc" ? customerList.OrderBy(c => c.Phone2) : customerList.OrderByDescending(c => c.Phone2);
                    break;
                case "Surname":
                    customerList = sortDirection == "asc" ? customerList.OrderBy(c => c.Surname) : customerList.OrderByDescending(c => c.Surname);
                    break;
            }
            customerList=customerList.Skip(skip).Take(takeA).ToList();
            var response = new getAllCustomerResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = customerList
            };
            return Success<getAllCustomerResponse>(data:response);
        }

        public ClientResult<getOneCustomerResponse> getOneCustomer(getOneCustomerRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneCustomerResponse>(message:"Kullanıcı Id null");
            }
            var existingcustomer=_customerDataAccess.GetById(request.Id);
            if (!existingcustomer.Success)
            {
                return Error<getOneCustomerResponse>(message: existingcustomer.Message);
            }
            var customer=existingcustomer.Entity;
            var response=new getOneCustomerResponse()
            {
                Addres=customer.Addres,
                Email=customer.Email,
                Id=customer.Id.ToString(),
                Name=customer.Name,
                Surname=customer.Surname,
                Phone=customer.Phone,
                Phone2=customer.Phone2,
            };
            return Success<getOneCustomerResponse>(data:response);
        }

    }
}
