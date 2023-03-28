using System.ComponentModel.DataAnnotations;

namespace Entitys.Abstract
{
    public class Customer
    {
        public Customer()
        {
            this.Orders = new List<Order>();
            this.Favorities = new List<Favority>();
            this.Baskets = new List<Basket>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Phone2 { get; set; }
        public string Addres { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Favority> Favorities { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }

    }
}
