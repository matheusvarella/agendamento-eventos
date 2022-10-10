using AgendamentoEventos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AgendamentoEventos.Data.Mappings
{
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(e => e.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("Value")
                .HasColumnType("DECIMAL(7, 2)");

            builder.Property(e => e.TicketLimit)
                .IsRequired()
                .HasColumnName("TicketLimit")
                .HasColumnType("INTEGER")
                .HasDefaultValue(0);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250);

            builder.Property(e => e.StartDate)
                .IsRequired()
                .HasColumnName("StartDate")
                .HasColumnType("DATE");

            builder.Property(e => e.FinalDate)
                .IsRequired()
                .HasColumnName("FinalDate")
                .HasColumnType("DATE");

            builder.Property(e => e.Timestamps)
                .IsRequired()
                .HasColumnName("Timestamps")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.HasOne(e => e.Organizer)
                .WithMany(e => e.Events)
                .HasConstraintName("FK_Event_Organizer")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
