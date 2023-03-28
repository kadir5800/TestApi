namespace Entitys.Abstract
{
    public class Model : DefOb
    {
        public Model()
        {
            this.Shoes = new List<Shoe>();
        }
        public string Name { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; }

    }
}
