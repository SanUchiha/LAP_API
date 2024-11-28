using Microsoft.EntityFrameworkCore;

namespace SimpleLAP.Models;

public partial class LapDbSimpleContext : DbContext
{
    public LapDbSimpleContext()
    {
    }

    public LapDbSimpleContext(DbContextOptions<LapDbSimpleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.IdCampus).HasName("PK__Campus__DA49C2DEE0D19954");

            entity.ToTable("Campus");

            entity.HasIndex(e => e.Nombre, "idx_Campus_Nombre");

            entity.Property(e => e.Ciudad).HasMaxLength(30);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.FormaPagoDos).HasMaxLength(150);
            entity.Property(e => e.FormaPagoTres).HasMaxLength(150);
            entity.Property(e => e.FormaPagoUno).HasMaxLength(150);
            entity.Property(e => e.Localidad).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Pais).HasMaxLength(30);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.IdParticipante).HasName("PK__Particip__561392425A856454");

            entity.ToTable("Participante");

            entity.HasIndex(e => e.Dnitutor, "UQ__Particip__5D67C3BEC43AABBD").IsUnique();

            entity.HasIndex(e => e.Dniparticipante, "UQ__Particip__AD99A6EE3D510BC1").IsUnique();

            entity.HasIndex(e => e.IdCampus, "idx_Participante_IdCampus");

            entity.HasIndex(e => e.Nombre, "idx_Participante_Nombre");

            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CorreoParticipante).HasMaxLength(70);
            entity.Property(e => e.DireccionParticipante).HasMaxLength(100);
            entity.Property(e => e.Dniparticipante)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("DNIParticipante");
            entity.Property(e => e.Dnitutor)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("DNITutor");
            entity.Property(e => e.Localidad).HasMaxLength(30);
            entity.Property(e => e.Nombre).HasMaxLength(30);
            entity.Property(e => e.NombreTutor).HasMaxLength(30);
            entity.Property(e => e.PrimerApellido).HasMaxLength(30);
            entity.Property(e => e.PrimerApellidoTutor).HasMaxLength(30);
            entity.Property(e => e.SegundoApellido).HasMaxLength(30);
            entity.Property(e => e.SegundoApellidoTutor).HasMaxLength(30);
            entity.Property(e => e.TallaCamiseta).HasMaxLength(1);
            entity.Property(e => e.TelefonoPrincipal)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TelefonoSecundario)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCampusNavigation).WithMany(p => p.Participantes)
                .HasForeignKey(d => d.IdCampus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Campus_Participantes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
