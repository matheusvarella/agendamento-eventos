using AgendamentoEventos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AgendamentoEventos.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(e => e.CnpjCpf)
                .IsRequired()
                .HasColumnName("CnpjCpf")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasColumnName("PhoneNumber")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(15);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(e => e.TypeUser)
                .IsRequired()
                .HasColumnName("TypeUser")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(12)
                .HasDefaultValue("Participant");

            builder.Property(e => e.Timestamps)
                .IsRequired()
                .HasColumnName("Timestamps")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValue(DateTime.Now.ToUniversalTime());
        }
    }
}
