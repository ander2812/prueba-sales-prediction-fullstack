using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Infrastructure;

public partial class OrderValue
{
    public int orderid { get; set; }

    public int? custid { get; set; }

    public int empid { get; set; }

    public int shipperid { get; set; }

    public DateTime orderdate { get; set; }

    public decimal? val { get; set; }
}
