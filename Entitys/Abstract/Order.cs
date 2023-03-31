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
        public string CitiesId { get; set; }
        public string DistrictId { get; set; }
        public string CustomerId { get; set; }
    }
}
