using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Parcial.Models;

public partial class DbAa579fProg3w2Context : DbContext
{
    public DbAa579fProg3w2Context()
    {
    }

    public DbAa579fProg3w2Context(DbContextOptions<DbAa579fProg3w2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Albanile> Albaniles { get; set; }

    public virtual DbSet<AlbanilesXObra> AlbanilesXObras { get; set; }

    public virtual DbSet<Obra> Obras { get; set; }

    public virtual DbSet<TiposObra> TiposObras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbAa579fProg3w2Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Albanile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Albanile__3214EC073B144B8D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Apellido).HasMaxLength(255);
            entity.Property(e => e.Calle).HasMaxLength(255);
            entity.Property(e => e.CodPost).HasMaxLength(50);
            entity.Property(e => e.Dni).HasMaxLength(50);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Numero).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<AlbanilesXObra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Albanile__3214EC07456FA6B2");

            entity.ToTable("AlbanilesXObra");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");

            entity.HasOne(d => d.IdAlbanilNavigation).WithMany(p => p.AlbanilesXObras)
                .HasForeignKey(d => d.IdAlbanil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbanilesXObra_Albanil");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.AlbanilesXObras)
                .HasForeignKey(d => d.IdObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbanilesXObra_Obra");
        });

        modelBuilder.Entity<Obra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Obras__3214EC070CAB2808");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(255);

            entity.HasOne(d => d.IdTipoObraNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.IdTipoObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Obras_TiposObra");
        });

        modelBuilder.Entity<TiposObra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposObr__3214EC0784C70DCD");

            entity.ToTable("TiposObra");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
