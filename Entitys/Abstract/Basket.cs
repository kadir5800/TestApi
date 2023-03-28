namespace Entitys.Abstract
{
    public class Basket : DefOb
    {
        public long ShoesId { get; set; }
        public virtual Shoe Shoes { get; set; }
        public long CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
    }
}
