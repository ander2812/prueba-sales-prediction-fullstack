using System;
using System.Collections.Generic;

namespace SalesDatePrediction.Infrastructure;

public partial class Employee
{
    public int empid { get; set; }

    public string lastname { get; set; } = null!;

    public string firstname { get; set; } = null!;

    public string title { get; set; } = null!;

    public string titleofcourtesy { get; set; } = null!;

    public DateTime birthdate { get; set; }

    public DateTime hiredate { get; set; }

    public string address { get; set; } = null!;

    public string city { get; set; } = null!;

    public string? region { get; set; }

    public string? postalcode { get; set; }

    public string country { get; set; } = null!;

    public string phone { get; set; } = null!;

    public int? mgrid { get; set; }

    public virtual ICollection<Employee> Inversemgr { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Employee? mgr { get; set; }
}
