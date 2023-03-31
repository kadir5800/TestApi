using Business.DTO.BaseObjects;

namespace Business.DTO.Customer
{
    public class getAllCustomerRequest : dataTableRequest
    {
        public List<getOneCustomerResponse> data { get; set; }
    }
}
