using System.ComponentModel.DataAnnotations;

namespace Entitys.Abstract
{
    public class DefOb
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
