using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations
{
    public partial class CreateMatchContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchDetail",
                columns: table => new
                {
                    MatchId = table.Column<long>(nullable: false),
                    MatchCreation = table.Column<long>(nullable: false),
                    MatchDuration = table.Column<long>(nullable: false),
                    QueueType = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Season = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDetail", x => x.MatchId);
                });

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

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    MatchId = table.Column<long>(nullable: false),
                    ChampionId = table.Column<int>(nullable: false),
                    Lane = table.Column<string>(nullable: true),
                    MatchDetailMatchId = table.Column<long>(nullable: true),
                    ParticipantId = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Participant_MatchDetail_MatchDetailMatchId",
                        column: x => x.MatchDetailMatchId,
                        principalTable: "MatchDetail",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    MatchId = table.Column<long>(nullable: false),
                    BaronKills = table.Column<int>(nullable: false),
                    DragonKills = table.Column<int>(nullable: false),
                    FirstBaron = table.Column<bool>(nullable: false),
                    FirstBlood = table.Column<bool>(nullable: false),
                    FirstDragon = table.Column<bool>(nullable: false),
                    FirstInhibitor = table.Column<bool>(nullable: false),
                    FirstRiftHerald = table.Column<bool>(nullable: false),
                    FirstTower = table.Column<bool>(nullable: false),
                    InhibitorKills = table.Column<int>(nullable: false),
                    MatchDetailMatchId = table.Column<long>(nullable: true),
                    RiftHeraldKills = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TowerKills = table.Column<int>(nullable: false),
                    Winner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Team_MatchDetail_MatchDetailMatchId",
                        column: x => x.MatchDetailMatchId,
                        principalTable: "MatchDetail",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_MatchDetailMatchId",
                table: "Participant",
                column: "MatchDetailMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_MatchDetailMatchId",
                table: "Team",
                column: "MatchDetailMatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchReference");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "MatchDetail");
        }
    }
}
