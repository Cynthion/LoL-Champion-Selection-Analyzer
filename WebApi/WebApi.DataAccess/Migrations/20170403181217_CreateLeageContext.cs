using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations
{
    public partial class CreateLeageContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SummonerLeagueEntry",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false),
                    LeaguePoints = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false),
                    Region = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerLeagueEntry", x => x.PlayerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SummonerLeagueEntry");
        }
    }
}
