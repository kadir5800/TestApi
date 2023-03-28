namespace Entitys.Abstract
{
    public class Color : DefOb
    {
        public Color()
        {
            this.Shoes = new List<Shoe>();
        }
        public string Name { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; }
    }
}
