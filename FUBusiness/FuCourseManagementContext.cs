using System;
using System.Collections.Generic;
using FUBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FUBusiness;

public partial class FuCourseManagementContext : DbContext
{
    public FuCourseManagementContext()
    {
    }

    public FuCourseManagementContext(DbContextOptions<FuCourseManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<EnrollmentRecord> EnrollmentRecords { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).Build();
        optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC27A4E30B93");

            entity.HasIndex(e => e.CourseCode, "UQ__Courses__FC00E0004E57F71E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CourseCode).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<EnrollmentRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Enrollme__3214EC2774CB0487");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Dropped).HasDefaultValue(false);
            entity.Property(e => e.EnrollDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.EnrollmentRecords)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Enrollmen__Cours__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.EnrollmentRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Enrollmen__UserI__534D60F1");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Sessions__C9F49270F990B0FE");

            entity.Property(e => e.SessionId)
                .HasMaxLength(50)
                .HasColumnName("SessionID");
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Sessions__UserID__59063A47");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27478C9187");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534EFC0EE96").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
