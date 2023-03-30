using System.ComponentModel.DataAnnotations;

namespace Entitys.Abstract
{
    public class Customer:DefOb
    {
        public Customer()
        {
            this.Orders = new List<Order>();
            this.Favorities = new List<Favority>();
            this.Baskets = new List<Basket>();
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Phone2 { get; set; }
        public string Addres { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Favority> Favorities { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }

    }
}
