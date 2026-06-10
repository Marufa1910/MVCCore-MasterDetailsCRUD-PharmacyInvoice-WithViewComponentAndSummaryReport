using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _1293481Evidence.Models;

public partial class StoredProcedureDbpharmaContext : DbContext
{
    public StoredProcedureDbpharmaContext()
    {
    }

    public StoredProcedureDbpharmaContext(DbContextOptions<StoredProcedureDbpharmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StoredProcedureDBPharma;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAB5E93BEE1D");

            entity.ToTable("Invoice");

            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl).IsUnicode(false);
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReferrerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("FK__Invoice__Transac__4BAC3F29");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey(e => e.SaleItemId).HasName("PK__SaleItem__C6059401575C490A");

            entity.ToTable("SaleItem");

            entity.Property(e => e.MedicineName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__SaleItem__Invoic__4E88ABD4");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__Transact__20266D0BDAFDD8AC");

            entity.ToTable("TransactionType");

            entity.Property(e => e.TransactionTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Seed();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionType>().HasData(
            new TransactionType { TransactionTypeId = 1, TransactionTypeName = "WholeSale" },
            new TransactionType { TransactionTypeId = 2, TransactionTypeName = "Retail" },
            new TransactionType { TransactionTypeId = 3, TransactionTypeName = "Internal Sale" }
        );
    }
}