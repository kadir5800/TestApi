using Business.DTO.BaseObjects;

namespace Business.DTO.Customer
{
    public class getAllCustomerResponse : dataTableResponse
    {
        public List<getOneCustomerResponse> data { get; set; }
    }
}
