namespace Entitys.Abstract
{
    public class Image : DefOb
    {
        public string Path { get; set; }
        public string ShoesId { get; set; }
        public virtual Shoe Shoes { get; set; }
    }
}
