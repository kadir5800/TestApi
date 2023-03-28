namespace Entitys.Abstract
{
    public class District : DefOb
    {
        public string Name { get; set; }
        public long CitiesId { get; set; }
        public virtual City Cities { get; set; }
    }
}
