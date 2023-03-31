using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.BaseObjects
{
    public class dataTableResponse
    {
        public string draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
    }
}
