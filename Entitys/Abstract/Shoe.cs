namespace Entitys.Abstract
{
    public class Shoe : DefOb
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Gender { get; set; }
        public int Number { get; set; }
        public decimal DiscountPrice { get; set; }
        public string BrandsId { get; set; }
        public string ColorsId { get; set; }
        public string CategoryId { get; set; }
        public string CampaignsId { get; set; }
        public string ModelId { get; set; }
        public string MaterialId { get; set; }
    }
}
