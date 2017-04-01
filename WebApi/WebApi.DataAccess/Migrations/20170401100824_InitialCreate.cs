using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    ParticipantId = table.Column<string>(nullable: false),
                    Queue = table.Column<string>(nullable: true),
                    Tier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.ParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "LeagueEntry",
                columns: table => new
                {
                    PlayerOrTeamId = table.Column<string>(nullable: false),
                    Division = table.Column<string>(nullable: true),
                    LeagueDtoParticipantId = table.Column<string>(nullable: true),
                    PlayerOrTeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueEntry", x => x.PlayerOrTeamId);
                    table.ForeignKey(
                        name: "FK_LeagueEntry_League_LeagueDtoParticipantId",
                        column: x => x.LeagueDtoParticipantId,
                        principalTable: "League",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeagueEntry_LeagueDtoParticipantId",
                table: "LeagueEntry",
                column: "LeagueDtoParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueEntry");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
