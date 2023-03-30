using MongoDB.Bson;

namespace Entitys.Abstract
{
    public class Token : DefOb
    {
        public string CustomerId { get; set; }
        public string token { get; set; }
        public DateTime TokenDate { get; set; }
    }
}
