using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Dtos
{
    public class ProductDto
    {
        public int productid { get; set; }
        public string productname { get; set; } = null!;
    }
}
