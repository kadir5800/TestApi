namespace Entitys.Abstract
{
    public class User : DefOb
    {
        public User()
        {
            this.Customers = new List<Customer>();
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime TokenDate { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
