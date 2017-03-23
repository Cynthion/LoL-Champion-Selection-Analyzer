using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.Data;

namespace WebApi.Migrations
{
    [DbContext(typeof(StatsContext))]
    partial class StatsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Models.Stats.AggregatedInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BotGamesPlayed");

                    b.Property<int>("KillingSpree");

                    b.Property<int>("MaxChampionsKilled");

                    b.Property<int>("MaxLargestCriticalStrike");

                    b.Property<int>("MaxLargestKillingSpree");

                    b.Property<int>("MaxNumDeaths");

                    b.Property<int>("MaxTimePlayed");

                    b.Property<int>("MaxTimeSpentLiving");

                    b.Property<int>("MostChampionKillsPerSession");

                    b.Property<int>("MostSpellsCast");

                    b.Property<int>("NormalGamesPlayed");

                    b.Property<int>("RankedPremadeGamesPlayed");

                    b.Property<int>("RankedSoloGamesPlayed");

                    b.Property<int>("TotalAssists");

                    b.Property<int>("TotalChampionKills");

                    b.Property<int>("TotalDamageDealt");

                    b.Property<int>("TotalDamageTaken");

                    b.Property<int>("TotalDeathsPerSession");

                    b.Property<int>("TotalDoubleKills");

                    b.Property<int>("TotalFirstBlood");

                    b.Property<int>("TotalGoldEarned");

                    b.Property<int>("TotalHeal");

                    b.Property<int>("TotalMagicDamageDealt");

                    b.Property<int>("TotalMinionKills");

                    b.Property<int>("TotalNeutralMinionsKilled");

                    b.Property<int>("TotalPentaKills");

                    b.Property<int>("TotalPhysicalDamageDealt");

                    b.Property<int>("TotalQuadraKills");

                    b.Property<int>("TotalSessionsLost");

                    b.Property<int>("TotalSessionsPlayed");

                    b.Property<int>("TotalSessionsWon");

                    b.Property<int>("TotalTripleKills");

                    b.Property<int>("TotalTurretsKilled");

                    b.Property<int>("TotalUnrealKills");

                    b.HasKey("Id");

                    b.ToTable("AggregatedInfo");
                });

            modelBuilder.Entity("WebApi.Models.Stats.ChampionInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AggregatedInfoId");

                    b.Property<int>("ChampionId");

                    b.Property<long?>("RankedInfoId");

                    b.HasKey("Id");

                    b.HasIndex("AggregatedInfoId");

                    b.HasIndex("RankedInfoId");

                    b.ToTable("ChampionInfo");
                });

            modelBuilder.Entity("WebApi.Models.Stats.RankedInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ModifyDate");

                    b.Property<long>("SummonerId");

                    b.HasKey("Id");

                    b.ToTable("RankedInfo");
                });

            modelBuilder.Entity("WebApi.Models.Stats.ChampionInfo", b =>
                {
                    b.HasOne("WebApi.Models.Stats.AggregatedInfo", "AggregatedInfo")
                        .WithMany()
                        .HasForeignKey("AggregatedInfoId");

                    b.HasOne("WebApi.Models.Stats.RankedInfo")
                        .WithMany("ChampionInfos")
                        .HasForeignKey("RankedInfoId");
                });
        }
    }
}
