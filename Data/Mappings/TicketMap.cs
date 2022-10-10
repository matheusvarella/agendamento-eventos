using AgendamentoEventos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AgendamentoEventos.Data.Mappings
{
    public class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(e => e.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(11);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(12)
                .HasDefaultValue("Comprado");

            builder.Property(e => e.Timestamps)
                .IsRequired()
                .HasColumnName("Timestamps")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.HasOne(e => e.Event)
                .WithMany(e => e.Tickets)
                .HasConstraintName("FK_Ticket_Event")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.Participant)
                .WithMany(e => e.Tickets)
                .HasConstraintName("FK_Ticket_Participant")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
