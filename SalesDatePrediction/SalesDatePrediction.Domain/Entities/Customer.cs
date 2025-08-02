using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Infrastructure;

public partial class Customer
{
    public int custid { get; set; }

    public string companyname { get; set; } = null!;

    public string contactname { get; set; } = null!;

    public string contacttitle { get; set; } = null!;

    public string address { get; set; } = null!;

    public string city { get; set; } = null!;

    public string? region { get; set; }

    public string? postalcode { get; set; }

    public string country { get; set; } = null!;

    public string phone { get; set; } = null!;

    public string? fax { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
