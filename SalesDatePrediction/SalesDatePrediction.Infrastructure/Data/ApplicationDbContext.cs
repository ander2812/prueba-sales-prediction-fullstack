using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CustOrder> CustOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderTotalsByYear> OrderTotalsByYears { get; set; }

    public virtual DbSet<OrderValue> OrderValues { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories", "Production");

            entity.HasIndex(e => e.categoryname, "categoryname");

            entity.Property(e => e.categoryname).HasMaxLength(15);
            entity.Property(e => e.description).HasMaxLength(200);
        });

        modelBuilder.Entity<CustOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CustOrders", "Sales");

            entity.Property(e => e.ordermonth).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.custid);

            entity.ToTable("Customers", "Sales");

            entity.HasIndex(e => e.city, "idx_nc_city");

            entity.HasIndex(e => e.companyname, "idx_nc_companyname");

            entity.HasIndex(e => e.postalcode, "idx_nc_postalcode");

            entity.HasIndex(e => e.region, "idx_nc_region");

            entity.Property(e => e.address).HasMaxLength(60);
            entity.Property(e => e.city).HasMaxLength(15);
            entity.Property(e => e.companyname).HasMaxLength(40);
            entity.Property(e => e.contactname).HasMaxLength(30);
            entity.Property(e => e.contacttitle).HasMaxLength(30);
            entity.Property(e => e.country).HasMaxLength(15);
            entity.Property(e => e.fax).HasMaxLength(24);
            entity.Property(e => e.phone).HasMaxLength(24);
            entity.Property(e => e.postalcode).HasMaxLength(10);
            entity.Property(e => e.region).HasMaxLength(15);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.empid);

            entity.ToTable("Employees", "HR");

            entity.HasIndex(e => e.lastname, "idx_nc_lastname");

            entity.HasIndex(e => e.postalcode, "idx_nc_postalcode");

            entity.Property(e => e.address).HasMaxLength(60);
            entity.Property(e => e.birthdate).HasColumnType("datetime");
            entity.Property(e => e.city).HasMaxLength(15);
            entity.Property(e => e.country).HasMaxLength(15);
            entity.Property(e => e.firstname).HasMaxLength(10);
            entity.Property(e => e.hiredate).HasColumnType("datetime");
            entity.Property(e => e.lastname).HasMaxLength(20);
            entity.Property(e => e.phone).HasMaxLength(24);
            entity.Property(e => e.postalcode).HasMaxLength(10);
            entity.Property(e => e.region).HasMaxLength(15);
            entity.Property(e => e.title).HasMaxLength(30);
            entity.Property(e => e.titleofcourtesy).HasMaxLength(25);

            entity.HasOne(d => d.mgr).WithMany(p => p.Inversemgr)
                .HasForeignKey(d => d.mgrid)
                .HasConstraintName("FK_Employees_Employees");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders", "Sales");

            entity.HasIndex(e => e.custid, "idx_nc_custid");

            entity.HasIndex(e => e.empid, "idx_nc_empid");

            entity.HasIndex(e => e.orderdate, "idx_nc_orderdate");

            entity.HasIndex(e => e.shippeddate, "idx_nc_shippeddate");

            entity.HasIndex(e => e.shipperid, "idx_nc_shipperid");

            entity.HasIndex(e => e.shippostalcode, "idx_nc_shippostalcode");

            entity.Property(e => e.freight).HasColumnType("money");
            entity.Property(e => e.orderdate).HasColumnType("datetime");
            entity.Property(e => e.requireddate).HasColumnType("datetime");
            entity.Property(e => e.shipaddress).HasMaxLength(60);
            entity.Property(e => e.shipcity).HasMaxLength(15);
            entity.Property(e => e.shipcountry).HasMaxLength(15);
            entity.Property(e => e.shipname).HasMaxLength(40);
            entity.Property(e => e.shippeddate).HasColumnType("datetime");
            entity.Property(e => e.shippostalcode).HasMaxLength(10);
            entity.Property(e => e.shipregion).HasMaxLength(15);

            entity.HasOne(d => d.cust).WithMany(p => p.Orders)
                .HasForeignKey(d => d.custid)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.emp).WithMany(p => p.Orders)
                .HasForeignKey(d => d.empid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.shipper).WithMany(p => p.Orders)
                .HasForeignKey(d => d.shipperid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Shippers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.orderid, e.productid });

            entity.ToTable("OrderDetails", "Sales");

            entity.HasIndex(e => e.orderid, "idx_nc_orderid");

            entity.HasIndex(e => e.productid, "idx_nc_productid");

            entity.Property(e => e.discount).HasColumnType("numeric(4, 3)");
            entity.Property(e => e.qty).HasDefaultValue((short)1);
            entity.Property(e => e.unitprice).HasColumnType("money");

            entity.HasOne(d => d.order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<OrderTotalsByYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrderTotalsByYear", "Sales");
        });

        modelBuilder.Entity<OrderValue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrderValues", "Sales");

            entity.Property(e => e.orderdate).HasColumnType("datetime");
            entity.Property(e => e.val).HasColumnType("numeric(12, 2)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products", "Production");

            entity.HasIndex(e => e.categoryid, "idx_nc_categoryid");

            entity.HasIndex(e => e.productname, "idx_nc_productname");

            entity.HasIndex(e => e.supplierid, "idx_nc_supplierid");

            entity.Property(e => e.productname).HasMaxLength(40);
            entity.Property(e => e.unitprice).HasColumnType("money");

            entity.HasOne(d => d.category).WithMany(p => p.Products)
                .HasForeignKey(d => d.categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.supplierid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.ToTable("Shippers", "Sales");

            entity.Property(e => e.companyname).HasMaxLength(40);
            entity.Property(e => e.phone).HasMaxLength(24);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Suppliers", "Production");

            entity.HasIndex(e => e.companyname, "idx_nc_companyname");

            entity.HasIndex(e => e.postalcode, "idx_nc_postalcode");

            entity.Property(e => e.address).HasMaxLength(60);
            entity.Property(e => e.city).HasMaxLength(15);
            entity.Property(e => e.companyname).HasMaxLength(40);
            entity.Property(e => e.contactname).HasMaxLength(30);
            entity.Property(e => e.contacttitle).HasMaxLength(30);
            entity.Property(e => e.country).HasMaxLength(15);
            entity.Property(e => e.fax).HasMaxLength(24);
            entity.Property(e => e.phone).HasMaxLength(24);
            entity.Property(e => e.postalcode).HasMaxLength(10);
            entity.Property(e => e.region).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
