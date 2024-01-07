using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Models;

public partial class EventDbContext : DbContext
{
    public EventDbContext()
    {
    }

    public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("");
    //=> optionsBuilder.UseSqlServer("");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Henkilo__HenkiloId");

            entity.ToTable("Henkilo");

            entity.Property(e => e.PersonId)
                .ValueGeneratedOnAdd()
                .HasColumnName("HenkiloID");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("Nimi");
            entity.Property(e => e.Birthdate).HasColumnType("datetime").HasColumnName("Syntymaaika");

            entity.HasMany(d => d.Events).WithMany(p => p.Persons)
                .UsingEntity<Dictionary<string, object>>(
                    "HenkiloTapahtuma",
                    r => r.HasOne<Event>().WithMany()
                        .HasForeignKey("TapahtumaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__HenkiloTa__TapahtumaId"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("HenkiloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__HenkiloTa__HenkiloId"),
                    j =>
                    {
                        j.HasKey("HenkiloID", "TapahtumaID").HasName("PK__HenkiloTa__HeTaId");
                        j.ToTable("HenkiloTapahtuma");
                        j.IndexerProperty<int>("HenkiloID").HasColumnName("HenkiloID");
                        j.IndexerProperty<int>("TapahtumaID").HasColumnName("TapahtumaID");
                    });
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Tapahtum__TapahtumaId");

            entity.ToTable("Tapahtuma");

            entity.Property(e => e.EventId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TapahtumaID");
            entity.Property(e => e.Time).HasColumnType("datetime").HasColumnName("Aika");
            entity.Property(e => e.Type).HasMaxLength(100).HasColumnName("Tyyppi");
           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
