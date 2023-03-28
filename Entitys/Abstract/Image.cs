using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Abstract
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ShoesId { get; set; }
        public virtual Shoe Shoes { get; set; }
    }
}
