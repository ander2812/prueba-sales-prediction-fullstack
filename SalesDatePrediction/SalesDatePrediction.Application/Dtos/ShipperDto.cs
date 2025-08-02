using SalesDatePrediction.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Dtos
{
    public class ShipperDto
    {
        public int shipperid { get; set; }

        public string companyname { get; set; } = null!;
    }
}
