using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AggregatedInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BotGamesPlayed = table.Column<int>(nullable: false),
                    KillingSpree = table.Column<int>(nullable: false),
                    MaxChampionsKilled = table.Column<int>(nullable: false),
                    MaxLargestCriticalStrike = table.Column<int>(nullable: false),
                    MaxLargestKillingSpree = table.Column<int>(nullable: false),
                    MaxNumDeaths = table.Column<int>(nullable: false),
                    MaxTimePlayed = table.Column<int>(nullable: false),
                    MaxTimeSpentLiving = table.Column<int>(nullable: false),
                    MostChampionKillsPerSession = table.Column<int>(nullable: false),
                    MostSpellsCast = table.Column<int>(nullable: false),
                    NormalGamesPlayed = table.Column<int>(nullable: false),
                    RankedPremadeGamesPlayed = table.Column<int>(nullable: false),
                    RankedSoloGamesPlayed = table.Column<int>(nullable: false),
                    TotalAssists = table.Column<int>(nullable: false),
                    TotalChampionKills = table.Column<int>(nullable: false),
                    TotalDamageDealt = table.Column<int>(nullable: false),
                    TotalDamageTaken = table.Column<int>(nullable: false),
                    TotalDeathsPerSession = table.Column<int>(nullable: false),
                    TotalDoubleKills = table.Column<int>(nullable: false),
                    TotalFirstBlood = table.Column<int>(nullable: false),
                    TotalGoldEarned = table.Column<int>(nullable: false),
                    TotalHeal = table.Column<int>(nullable: false),
                    TotalMagicDamageDealt = table.Column<int>(nullable: false),
                    TotalMinionKills = table.Column<int>(nullable: false),
                    TotalNeutralMinionsKilled = table.Column<int>(nullable: false),
                    TotalPentaKills = table.Column<int>(nullable: false),
                    TotalPhysicalDamageDealt = table.Column<int>(nullable: false),
                    TotalQuadraKills = table.Column<int>(nullable: false),
                    TotalSessionsLost = table.Column<int>(nullable: false),
                    TotalSessionsPlayed = table.Column<int>(nullable: false),
                    TotalSessionsWon = table.Column<int>(nullable: false),
                    TotalTripleKills = table.Column<int>(nullable: false),
                    TotalTurretsKilled = table.Column<int>(nullable: false),
                    TotalUnrealKills = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregatedInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankedInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifyDate = table.Column<long>(nullable: false),
                    SummonerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankedInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChampionInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AggregatedInfoId = table.Column<long>(nullable: true),
                    ChampionId = table.Column<int>(nullable: false),
                    RankedInfoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChampionInfo_AggregatedInfo_AggregatedInfoId",
                        column: x => x.AggregatedInfoId,
                        principalTable: "AggregatedInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChampionInfo_RankedInfo_RankedInfoId",
                        column: x => x.RankedInfoId,
                        principalTable: "RankedInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChampionInfo_AggregatedInfoId",
                table: "ChampionInfo",
                column: "AggregatedInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionInfo_RankedInfoId",
                table: "ChampionInfo",
                column: "RankedInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChampionInfo");

            migrationBuilder.DropTable(
                name: "AggregatedInfo");

            migrationBuilder.DropTable(
                name: "RankedInfo");
        }
    }
}
