using Business.DTO.BaseObjects;
using Business.DTO.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Category
{
    public class getAllCategoryResponse:dataTableResponse
    {
        public List<getOneCategoryResponse> data { get; set; }
    }
}
