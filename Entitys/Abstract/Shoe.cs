using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Abstract
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public int Gender { get; set; }
        public int Number { get; set; }
        public decimal DiscountPrice { get; set; }
        public int? BrandsId { get; set; }
        public int ColorsId { get; set; }
        public int CategoriesId { get; set; }
        public int? CampaignsId { get; set; }
        public virtual Campaign Campaigns { get; set; }
        public virtual Brand Brands { get; set; }
        public virtual Color Colors { get; set; }

    }
}
