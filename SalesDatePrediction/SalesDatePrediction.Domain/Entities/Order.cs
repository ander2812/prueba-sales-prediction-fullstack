using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Infrastructure;

public partial class Order
{
    public int orderid { get; set; }

    public int? custid { get; set; }

    public int empid { get; set; }

    public DateTime orderdate { get; set; }

    public DateTime requireddate { get; set; }

    public DateTime? shippeddate { get; set; }

    public int shipperid { get; set; }

    public decimal freight { get; set; }

    public string shipname { get; set; } = null!;

    public string shipaddress { get; set; } = null!;

    public string shipcity { get; set; } = null!;

    public string? shipregion { get; set; }

    public string? shippostalcode { get; set; }

    public string shipcountry { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Customer? cust { get; set; }

    public virtual Employee emp { get; set; } = null!;

    public virtual Shipper shipper { get; set; } = null!;
}
