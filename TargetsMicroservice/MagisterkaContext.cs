using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TargetsMicroservice.Models;

namespace TargetsMicroservice;

public partial class MagisterkaContext : DbContext
{
    public MagisterkaContext()
    {
    }

    public MagisterkaContext(DbContextOptions<MagisterkaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Crucialplace> Crucialplaces { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<Platoon> Platoons { get; set; }

    public virtual DbSet<Reconarea> Reconareas { get; set; }

    public virtual DbSet<Target> Targets { get; set; }

    public virtual DbSet<Targettype> Targettypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Magisterka;Username=myuser;Password=zaq1@WSX", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<Crucialplace>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("crucialplace_pkey");

            entity.ToTable("crucialplace");

            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Crucialplaceid).HasColumnName("crucialplaceid");
            entity.Property(e => e.Location)
                .HasColumnType("geometry(PointZ,4326)")
                .HasColumnName("location");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Flightid).HasName("flight_pkey");

            entity.ToTable("flight");

            entity.Property(e => e.Flightid)
                .ValueGeneratedNever()
                .HasColumnName("flightid");
            entity.Property(e => e.Beginpoint)
                .HasColumnType("geometry(PointZ,4326)")
                .HasColumnName("beginpoint");
            entity.Property(e => e.Begintime).HasColumnName("begintime");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Operatorid).HasColumnName("operatorid");

            entity.HasOne(d => d.Operator).WithMany(p => p.Flights)
                .HasForeignKey(d => d.Operatorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_flight_operator");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.HasKey(e => e.Operatorid).HasName("operator_pkey");

            entity.ToTable("operator");

            entity.Property(e => e.Operatorid)
                .ValueGeneratedNever()
                .HasColumnName("operatorid");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Teamid).HasColumnName("teamid");

            entity.HasOne(d => d.Team).WithMany(p => p.Operators)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_operator_team");
        });

        modelBuilder.Entity<Platoon>(entity =>
        {
            entity.HasKey(e => e.Platoonid).HasName("platoon_pkey");

            entity.ToTable("platoon");

            entity.Property(e => e.Platoonid)
                .ValueGeneratedNever()
                .HasColumnName("platoonid");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Reconarea>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("reconarea_pkey");

            entity.ToTable("reconarea");

            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.Area)
                .HasColumnType("jsonb")
                .HasColumnName("area");
            entity.Property(e => e.Areashape)
                .HasMaxLength(30)
                .HasColumnName("areashape");
            entity.Property(e => e.Comment)
                .HasMaxLength(70)
                .HasColumnName("comment");
            entity.Property(e => e.Operatorid).HasColumnName("operatorid");
            entity.Property(e => e.Periodfrom).HasColumnName("periodfrom");
            entity.Property(e => e.Periodto).HasColumnName("periodto");
            entity.Property(e => e.Platoonid).HasColumnName("platoonid");
            entity.Property(e => e.Reconareaid).HasColumnName("reconareaid");
            entity.Property(e => e.Teamid).HasColumnName("teamid");

            entity.HasOne(d => d.Operator).WithMany(p => p.Reconareas)
                .HasForeignKey(d => d.Operatorid)
                .HasConstraintName("fk_reconarea_operatorid");

            entity.HasOne(d => d.Platoon).WithMany(p => p.Reconareas)
                .HasForeignKey(d => d.Platoonid)
                .HasConstraintName("fk_reconarea_platoonid");

            entity.HasOne(d => d.Team).WithMany(p => p.Reconareas)
                .HasForeignKey(d => d.Teamid)
                .HasConstraintName("fk_reconarea_teamid");
        });

        modelBuilder.Entity<Target>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("target_pkey");

            entity.ToTable("target");

            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(70)
                .HasColumnName("comment");
            entity.Property(e => e.Detectiontime)
                .HasDefaultValueSql("now()")
                .HasColumnName("detectiontime");
            entity.Property(e => e.Flightid).HasColumnName("flightid");
            entity.Property(e => e.Imagelink)
                .HasMaxLength(150)
                .HasColumnName("imagelink");
            entity.Property(e => e.Location)
                .HasColumnType("geometry(PointZ,4326)")
                .HasColumnName("location");
            entity.Property(e => e.Targetid).HasColumnName("targetid");
            entity.Property(e => e.Targettypeid).HasColumnName("targettypeid");

            entity.HasOne(d => d.Flight).WithMany(p => p.Targets)
                .HasForeignKey(d => d.Flightid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_target_flight");

            entity.HasOne(d => d.Targettype).WithMany(p => p.Targets)
                .HasForeignKey(d => d.Targettypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_target_targettype");
        });

        modelBuilder.Entity<Targettype>(entity =>
        {
            entity.HasKey(e => e.Targettypeid).HasName("targettype_pkey");

            entity.ToTable("targettype");

            entity.Property(e => e.Targettypeid).HasColumnName("targettypeid");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Teamid).HasName("team_pkey");

            entity.ToTable("team");

            entity.Property(e => e.Teamid)
                .ValueGeneratedNever()
                .HasColumnName("teamid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Platoonid).HasColumnName("platoonid");

            entity.HasOne(d => d.Platoon).WithMany(p => p.Teams)
                .HasForeignKey(d => d.Platoonid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_team_platoon");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
