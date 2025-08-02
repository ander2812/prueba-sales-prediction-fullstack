using SalesDatePrediction.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Dtos
{
    public class OrderDetailDto
    {
        public int orderid { get; set; }

        public int productid { get; set; }

        public decimal unitprice { get; set; }

        public short qty { get; set; }

        public decimal discount { get; set; }

        public virtual Order order { get; set; } = null!;

        public virtual Product product { get; set; } = null!;
    }
}
