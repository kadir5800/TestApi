namespace Entitys.Abstract
{
    public class Category : DefOb
    {
        public Category()
        {
            this.Shoes = new List<Shoe>();
        }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; }

    }
}
