using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendamentoEventos.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CnpjCpf = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Timestamps = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValue: new DateTime(2022, 10, 9, 19, 9, 44, 282, DateTimeKind.Utc).AddTicks(2776))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(7,2)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    StartDate = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    FinalDate = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    Timestamps = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValue: new DateTime(2022, 10, 9, 19, 9, 44, 264, DateTimeKind.Utc).AddTicks(9900))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Organizer",
                        column: x => x.OrganizerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR(11)", maxLength: 11, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR(2)", maxLength: 2, nullable: false),
                    Timestamps = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValue: new DateTime(2022, 10, 9, 19, 9, 44, 280, DateTimeKind.Utc).AddTicks(972))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Event",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Participant",
                        column: x => x.ParticipantId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_OrganizerId",
                table: "Event",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ParticipantId",
                table: "Ticket",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
