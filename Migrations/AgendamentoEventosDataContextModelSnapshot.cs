﻿// <auto-generated />
using System;
using AgendamentoEventos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgendamentoEventos.Migrations
{
    [DbContext(typeof(AgendamentoEventosDataContext))]
    partial class AgendamentoEventosDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AgendamentoEventos.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Description");

                    b.Property<DateTime>("FinalDate")
                        .HasColumnType("SMALLDATETIME")
                        .HasColumnName("FinalDate");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("SMALLDATETIME")
                        .HasColumnName("StartDate");

                    b.Property<DateTime>("Timestamps")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SMALLDATETIME")
                        .HasDefaultValue(new DateTime(2022, 10, 9, 19, 9, 44, 264, DateTimeKind.Utc).AddTicks(9900))
                        .HasColumnName("Timestamps");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Title");

                    b.Property<decimal>("Value")
                        .HasColumnType("DECIMAL(7,2)")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("AgendamentoEventos.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR(11)")
                        .HasColumnName("Cpf");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Name");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR(2)")
                        .HasColumnName("Status");

                    b.Property<DateTime>("Timestamps")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SMALLDATETIME")
                        .HasDefaultValue(new DateTime(2022, 10, 9, 19, 9, 44, 280, DateTimeKind.Utc).AddTicks(972))
                        .HasColumnName("Timestamps");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("AgendamentoEventos.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CnpjCpf")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR(20)")
                        .HasColumnName("CnpjCpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR(15)")
                        .HasColumnName("PhoneNumber");

                    b.Property<DateTime>("Timestamps")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SMALLDATETIME")
                        .HasDefaultValue(new DateTime(2022, 10, 9, 19, 9, 44, 282, DateTimeKind.Utc).AddTicks(2776))
                        .HasColumnName("Timestamps");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AgendamentoEventos.Models.Event", b =>
                {
                    b.HasOne("AgendamentoEventos.Models.User", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId")
                        .HasConstraintName("FK_Event_Organizer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("AgendamentoEventos.Models.Ticket", b =>
                {
                    b.HasOne("AgendamentoEventos.Models.Event", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK_Ticket_Event")
                        .IsRequired();

                    b.HasOne("AgendamentoEventos.Models.User", "Participant")
                        .WithMany("Tickets")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_Ticket_Participant")
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("AgendamentoEventos.Models.Event", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("AgendamentoEventos.Models.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
