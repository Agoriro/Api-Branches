using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiBranch.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BranchTest> BranchTests { get; set; }

    public virtual DbSet<CurrencyTest> CurrencyTests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BranchTest>(entity =>
        {
            entity.HasKey(e => e.IdBranch);

            entity.ToTable("Branch_Test");

            entity.Property(e => e.BranchAddress).HasMaxLength(250);
            entity.Property(e => e.BranchDateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BranchDescription).HasMaxLength(250);
            entity.Property(e => e.BranchId).HasMaxLength(50);

            entity.HasOne(d => d.IdCurrencyNavigation).WithMany(p => p.BranchTests)
                .HasForeignKey(d => d.IdCurrency)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_Test_Currency_Test");
        });

        modelBuilder.Entity<CurrencyTest>(entity =>
        {
            entity.HasKey(e => e.IdCurrency);

            entity.ToTable("Currency_Test");

            entity.Property(e => e.CurrencyName).HasMaxLength(50);
            entity.Property(e => e.CurrencySymbol).HasMaxLength(5);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
