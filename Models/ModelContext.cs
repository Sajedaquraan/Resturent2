using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Resturent2.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rcategory> Rcategories { get; set; }

    public virtual DbSet<Rcustomer> Rcustomers { get; set; }

    public virtual DbSet<Rproduct> Rproducts { get; set; }

    public virtual DbSet<Rproductcustomer> Rproductcustomers { get; set; }

    public virtual DbSet<Rrole> Rroles { get; set; }

    public virtual DbSet<Ruserlogin> Ruserlogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521) (CONNECT_DATA=(SERVICE_NAME=xe))));User Id=C##Resturent; Password=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##RESTURENT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Rcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008519");

            entity.ToTable("RCATEGORY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
        });

        modelBuilder.Entity<Rcustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008513");

            entity.ToTable("RCUSTOMER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNAME");
        });

        modelBuilder.Entity<Rproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008521");

            entity.ToTable("RPRODUCT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryId)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Namee)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAMEE");
            entity.Property(e => e.Price)
                .HasColumnType("FLOAT")
                .HasColumnName("PRICE");
            entity.Property(e => e.Sale)
                .HasColumnType("NUMBER")
                .HasColumnName("SALE");

            entity.HasOne(d => d.Category).WithMany(p => p.Rproducts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_CATEGORY");
        });

        modelBuilder.Entity<Rproductcustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008524");

            entity.ToTable("RPRODUCTCUSTOMER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.DateFrom)
                .HasColumnType("DATE")
                .HasColumnName("DATE_FROM");
            entity.Property(e => e.DateTo)
                .HasColumnType("DATE")
                .HasColumnName("DATE_TO");
            entity.Property(e => e.ProductId)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");

            entity.HasOne(d => d.Customer).WithMany(p => p.Rproductcustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CUSTOMER2");

            entity.HasOne(d => d.Product).WithMany(p => p.Rproductcustomers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_PRODUCT");
        });

        modelBuilder.Entity<Rrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008511");

            entity.ToTable("RROLES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Ruserlogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008515");

            entity.ToTable("RUSERLOGIN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Passwordd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORDD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.Customer).WithMany(p => p.Ruserlogins)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CUSTOMER");

            entity.HasOne(d => d.Role).WithMany(p => p.Ruserlogins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_ROLES");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
