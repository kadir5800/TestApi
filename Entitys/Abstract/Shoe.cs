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
        public int? BrandsId { get; set; }
        public int ColorsId { get; set; }
        public int CategoryId { get; set; }
        public int? CampaignsId { get; set; }
        public virtual Campaign Campaigns { get; set; }
        public virtual Brand Brands { get; set; }
        public virtual Color Colors { get; set; }
        public virtual Category Categories { get; set; }

    }
}
