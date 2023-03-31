using System.ComponentModel.DataAnnotations;

namespace Entitys.Abstract
{
    public class Customer:DefOb
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Phone2 { get; set; }
        public string Addres { get; set; }
    }
}
