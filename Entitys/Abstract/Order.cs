using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Abstract
{
    public class Order
    {
        [Key]
        public int Id{ get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public int CitiesId { get; set; }
        public int DistrictId { get; set; }
        public int CustomerId { get; set; }
        public virtual City Cities { get; set; }
        public virtual District District { get; set; }
        public virtual Customer Customers{ get; set; }
    }
}
