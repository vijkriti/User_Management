using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataContext.Models;

namespace DataContext.Data;

public partial class SDirectContext : DbContext
{
    public SDirectContext()
    {
    }

    public SDirectContext(DbContextOptions<SDirectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<KritiUser> KritiUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KritiUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__KritiUse__1788CCAC4390CBC1");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AlternatePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ImgPath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryCity)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryCountry)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryState)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryZipCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.ResetExpiryToken).HasColumnType("datetime");
            entity.Property(e => e.ResetPasswordToken)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.SecondaryAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SecondaryCity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecondaryCountry)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecondaryState)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecondaryZipCode)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
