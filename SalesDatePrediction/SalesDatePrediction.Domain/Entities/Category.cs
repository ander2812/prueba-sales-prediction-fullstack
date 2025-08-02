using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Infrastructure;

public partial class Category
{
    public int categoryid { get; set; }

    public string categoryname { get; set; } = null!;

    public string description { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
