namespace Entitys.Abstract
{
    public class Campaign : DefOb
    {
        public Campaign()
        {
            this.Shoes = new List<Shoe>();
        }
        public string Name { get; set; }

        public virtual ICollection<Shoe> Shoes { get; set; }
    }
}
