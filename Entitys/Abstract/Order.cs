namespace Entitys.Abstract
{
    public class Order : DefOb
    {
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public string OrderTrackingNo { get; set; }
        public string CargoTrackingNo{ get; set; }
        public string CargoName { get; set; }
        public long CitiesId { get; set; }
        public long DistrictId { get; set; }
        public long CustomerId { get; set; }
        public virtual City Cities { get; set; }
        public virtual District District { get; set; }
        public virtual Customer Customers { get; set; }
    }
}
