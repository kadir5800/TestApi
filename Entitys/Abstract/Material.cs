using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Abstract
{
    public class Material : DefOb
    {
        public Material()
        {
            this.Shoes = new List<Shoe>();
        }
        public string Name { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; }
    }
}
