namespace Entitys.Abstract
{
    public class Image : DefOb
    {
        public string Path { get; set; }
        public long ShoesId { get; set; }
        public virtual Shoe Shoes { get; set; }
    }
}
