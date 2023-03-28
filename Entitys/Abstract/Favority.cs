using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Abstract
{
    public class Favority
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public int ShoesId { get; set; }
        public virtual Shoe Shoes { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
    }
}
