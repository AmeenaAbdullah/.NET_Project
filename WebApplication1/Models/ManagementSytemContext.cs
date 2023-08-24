using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class ManagementSytemContext : DbContext
{
    public ManagementSytemContext()
    {
    }

    public ManagementSytemContext(DbContextOptions<ManagementSytemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studentcourse> Studentcourses { get; set; }

    public virtual DbSet<Studentteacher> Studentteachers { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseMySql("server=localhost;database=management_sytem;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("course");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cid)
                .HasColumnType("int(11)")
                .HasColumnName("cid");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity
                .ToTable("__efmigrationshistory")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("student");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Studentcourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("studentcourse");

            entity.HasIndex(e => e.Cid, "FK_studentcourse_course");

            entity.HasIndex(e => e.Sid, "FK_studentcourse_student");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cid).HasColumnType("int(11)");
            entity.Property(e => e.Sid).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Studentteacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("studentteacher");

            entity.HasIndex(e => e.Sid, "FK_studentteacher_student");

            entity.HasIndex(e => e.Tid, "FK_studentteacher_teacher");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Sid).HasColumnType("int(11)");
            entity.Property(e => e.Tid).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teacher");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
