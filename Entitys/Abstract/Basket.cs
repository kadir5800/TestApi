namespace Entitys.Abstract
{
    public class Basket : DefOb
    {
        public int ShoesId { get; set; }
        public virtual Shoe Shoes { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
    }
}
