namespace Entitys.Abstract
{
    public class Brand : DefOb
    {
        public Brand()
        {
            this.Shoes = new List<Shoe>();
        }
        public string Name { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; }
    }
}
