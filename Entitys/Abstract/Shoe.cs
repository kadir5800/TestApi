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
        public long? BrandsId { get; set; }
        public long ColorsId { get; set; }
        public long CategoryId { get; set; }
        public long? CampaignsId { get; set; }
        public long ModelId { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual Model Model{ get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Color Color { get; set; }
        public virtual Category Categorie { get; set; }

    }
}
