﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Abstract
{
    public class Campaign
    {
        public Campaign()
        {
            this.Shoes = new List<Shoe>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Shoe> Shoes { get; set; }
    }
}
