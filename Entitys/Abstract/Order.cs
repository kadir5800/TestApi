namespace Entitys.Abstract
{
    public class Order : DefOb
    {
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public int CitiesId { get; set; }
        public int DistrictId { get; set; }
        public int CustomerId { get; set; }
        public virtual City Cities { get; set; }
        public virtual District District { get; set; }
        public virtual Customer Customers { get; set; }
    }
}
