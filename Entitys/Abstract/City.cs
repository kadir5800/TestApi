namespace Entitys.Abstract
{
    public class City : DefOb
    {
        public City()
        {
            this.Districts = new List<District>();
        }
        public string Name { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
