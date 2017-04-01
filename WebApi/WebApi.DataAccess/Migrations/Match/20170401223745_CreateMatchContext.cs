using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations.Match
{
    public partial class CreateMatchContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchReference",
                columns: table => new
                {
                    MatchId = table.Column<long>(nullable: false),
                    Champion = table.Column<long>(nullable: false),
                    Lane = table.Column<string>(nullable: true),
                    PlatformId = table.Column<string>(nullable: true),
                    Queue = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Season = table.Column<string>(nullable: true),
                    Timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchReference", x => x.MatchId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchReference");
        }
    }
}
