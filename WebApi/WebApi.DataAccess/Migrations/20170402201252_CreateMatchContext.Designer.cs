using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.DataAccess.DbContexts;

namespace WebApi.DataAccess.Migrations
{
    [DbContext(typeof(MatchContext))]
    [Migration("20170402201252_CreateMatchContext")]
    partial class CreateMatchContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Model.Dtos.Match.MatchDetail", b =>
                {
                    b.Property<long>("MatchId");

                    b.Property<long>("MatchCreation");

                    b.Property<long>("MatchDuration");

                    b.Property<string>("QueueType");

                    b.Property<string>("Region");

                    b.Property<string>("Season");

                    b.HasKey("MatchId");

                    b.ToTable("MatchDetail");
                });

            modelBuilder.Entity("WebApi.Model.Dtos.Match.MatchReference", b =>
                {
                    b.Property<long>("MatchId");

                    b.Property<long>("Champion");

                    b.Property<string>("Lane");

                    b.Property<string>("PlatformId");

                    b.Property<string>("Queue");

                    b.Property<string>("Region");

                    b.Property<string>("Role");

                    b.Property<string>("Season");

                    b.Property<long>("Timestamp");

                    b.HasKey("MatchId");

                    b.ToTable("MatchReference");
                });

            modelBuilder.Entity("WebApi.Model.Dtos.Match.Participant", b =>
                {
                    b.Property<long>("MatchId");

                    b.Property<int>("ChampionId");

                    b.Property<string>("Lane");

                    b.Property<long?>("MatchDetailMatchId");

                    b.Property<int>("ParticipantId");

                    b.Property<string>("Role");

                    b.Property<int>("TeamId");

                    b.HasKey("MatchId");

                    b.HasIndex("MatchDetailMatchId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("WebApi.Model.Dtos.Match.Team", b =>
                {
                    b.Property<long>("MatchId");

                    b.Property<int>("BaronKills");

                    b.Property<int>("DragonKills");

                    b.Property<bool>("FirstBaron");

                    b.Property<bool>("FirstBlood");

                    b.Property<bool>("FirstDragon");

                    b.Property<bool>("FirstInhibitor");

                    b.Property<bool>("FirstRiftHerald");

                    b.Property<bool>("FirstTower");

                    b.Property<int>("InhibitorKills");

                    b.Property<long?>("MatchDetailMatchId");

                    b.Property<int>("RiftHeraldKills");

                    b.Property<int>("TeamId");

                    b.Property<int>("TowerKills");

                    b.Property<bool>("Winner");

                    b.HasKey("MatchId");

                    b.HasIndex("MatchDetailMatchId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("WebApi.Model.Dtos.Match.Participant", b =>
                {
                    b.HasOne("WebApi.Model.Dtos.Match.MatchDetail")
                        .WithMany("Participants")
                        .HasForeignKey("MatchDetailMatchId");
                });

            modelBuilder.Entity("WebApi.Model.Dtos.Match.Team", b =>
                {
                    b.HasOne("WebApi.Model.Dtos.Match.MatchDetail")
                        .WithMany("Teams")
                        .HasForeignKey("MatchDetailMatchId");
                });
        }
    }
}
