using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations
{
    public partial class CreateLeagueEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueEntry",
                columns: table => new
                {
                    PlayerOrTeamId = table.Column<long>(nullable: false),
                    Division = table.Column<string>(nullable: true),
                    PlayerOrTeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueEntry", x => x.PlayerOrTeamId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueEntry");
        }
    }
}
