using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIProjecte.Models;

public partial class EasydriveContext : DbContext
{
    public EasydriveContext()
    {
    }

    public EasydriveContext(DbContextOptions<EasydriveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cotxe> Cotxes { get; set; }

    public virtual DbSet<DadesPagament> DadesPagaments { get; set; }

    public virtual DbSet<Estat> Estats { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuari> Usuaris { get; set; }

    public virtual DbSet<Viatge> Viatges { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection=True; Encrypt=false; Database=easydrive");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cotxe>(entity =>
        {
            entity.HasKey(e => e.Matricula).HasName("PK__Cotxe__30962D14580D0035");

            entity.ToTable("Cotxe");

            entity.Property(e => e.Matricula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("matricula");
            entity.Property(e => e.Any).HasColumnName("any");
            entity.Property(e => e.Capacitat).HasColumnName("capacitat");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.FotoFitxaTecnica).HasColumnName("foto_fitxa_tecnica");
            entity.Property(e => e.HoresTreballades).HasColumnName("hores_treballades");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.Tipus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipus");
        });

        modelBuilder.Entity<DadesPagament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DadesPag__3213E83FD89270C7");

            entity.ToTable("DadesPagament");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataExpiracio).HasColumnName("data_expiracio");
            entity.Property(e => e.IdUsuari)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("id_usuari");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_tarjeta");
            entity.Property(e => e.Titular)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titular");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.DadesPagaments)
                .HasForeignKey(d => d.IdUsuari)
                .HasConstraintName("FK__DadesPaga__id_us__3B75D760");
        });

        modelBuilder.Entity<Estat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estat__3213E83F002A11AC");

            entity.ToTable("Estat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estat1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estat");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserva__3213E83FD56DAA07");

            entity.ToTable("Reserva");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataReserva).HasColumnName("data_reserva");
            entity.Property(e => e.DataViatge).HasColumnName("data_viatge");
            entity.Property(e => e.Desti)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("desti");
            entity.Property(e => e.Estat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estat");
            entity.Property(e => e.IdEstat).HasColumnName("id_estat");
            entity.Property(e => e.IdUsuari)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("id_usuari");
            entity.Property(e => e.Origen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("origen");
            entity.Property(e => e.Preu)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("preu");

            entity.HasOne(d => d.IdEstatNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstat)
                .HasConstraintName("FK__Reserva__id_esta__46E78A0C");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuari)
                .HasConstraintName("FK__Reserva__id_usua__45F365D3");
        });

        modelBuilder.Entity<Usuari>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PK__Usuari__C035B8DCC0129FE2");

            entity.ToTable("Usuari");

            entity.Property(e => e.Dni)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Cognom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cognom");
            entity.Property(e => e.DataNaixement).HasColumnName("data_naixement");
            entity.Property(e => e.Disponibilitat).HasColumnName("disponibilitat");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FotoCarnet).HasColumnName("foto_carnet");
            entity.Property(e => e.FotoPerfil).HasColumnName("foto_perfil");
            entity.Property(e => e.Horari)
                .HasColumnType("datetime")
                .HasColumnName("horari");
            entity.Property(e => e.IdZona).HasColumnName("id_zona");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passwordHash");
            entity.Property(e => e.Rol).HasColumnName("rol");
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefon");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Usuaris)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("FK__Usuari__id_zona__38996AB5");

            entity.HasMany(d => d.Matriculas).WithMany(p => p.IdUsuaris)
                .UsingEntity<Dictionary<string, object>>(
                    "CotxeUsuari",
                    r => r.HasOne<Cotxe>().WithMany()
                        .HasForeignKey("Matricula")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CotxeUsua__matri__412EB0B6"),
                    l => l.HasOne<Usuari>().WithMany()
                        .HasForeignKey("IdUsuari")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CotxeUsua__id_us__403A8C7D"),
                    j =>
                    {
                        j.HasKey("IdUsuari", "Matricula").HasName("PK__CotxeUsu__B7D3A84984AC0FDF");
                        j.ToTable("CotxeUsuari");
                        j.IndexerProperty<string>("IdUsuari")
                            .HasMaxLength(9)
                            .IsUnicode(false)
                            .HasColumnName("id_usuari");
                        j.IndexerProperty<string>("Matricula")
                            .HasMaxLength(20)
                            .IsUnicode(false)
                            .HasColumnName("matricula");
                    });
        });

        modelBuilder.Entity<Viatge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Viatge__3213E83F3FAE0D2D");

            entity.ToTable("Viatge");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comentari)
                .HasColumnType("text")
                .HasColumnName("comentari");
            entity.Property(e => e.Distancia)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("distancia");
            entity.Property(e => e.Durada).HasColumnName("durada");
            entity.Property(e => e.IdCotxe)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_cotxe");
            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.IdTaxista)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("id_taxista");
            entity.Property(e => e.IdZona).HasColumnName("id_zona");
            entity.Property(e => e.Valoracio)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("valoracio");

            entity.HasOne(d => d.IdCotxeNavigation).WithMany(p => p.Viatges)
                .HasForeignKey(d => d.IdCotxe)
                .HasConstraintName("FK__Viatge__id_cotxe__4CA06362");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Viatges)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK__Viatge__id_reser__4BAC3F29");

            entity.HasOne(d => d.IdTaxistaNavigation).WithMany(p => p.Viatges)
                .HasForeignKey(d => d.IdTaxista)
                .HasConstraintName("FK__Viatge__id_taxis__4AB81AF0");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Viatges)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("FK__Viatge__id_zona__49C3F6B7");
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Zona__3213E83F9DBBC9BB");

            entity.ToTable("Zona");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ciutat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ciutat");
            entity.Property(e => e.CodiPostal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codi_postal");
            entity.Property(e => e.Comarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("comarca");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");

            entity.HasMany(d => d.IdTaxista).WithMany(p => p.IdZonas)
                .UsingEntity<Dictionary<string, object>>(
                    "ZonaUsuari",
                    r => r.HasOne<Usuari>().WithMany()
                        .HasForeignKey("IdTaxista")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ZonaUsuar__id_ta__5070F446"),
                    l => l.HasOne<Zona>().WithMany()
                        .HasForeignKey("IdZona")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ZonaUsuar__id_zo__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("IdZona", "IdTaxista").HasName("PK__ZonaUsua__C6A9E9F2EDC334BB");
                        j.ToTable("ZonaUsuari");
                        j.IndexerProperty<int>("IdZona").HasColumnName("id_zona");
                        j.IndexerProperty<string>("IdTaxista")
                            .HasMaxLength(9)
                            .IsUnicode(false)
                            .HasColumnName("id_taxista");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
