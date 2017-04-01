using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.DataAccess.DbContexts;

namespace WebApi.DataAccess.Migrations
{
    [DbContext(typeof(LeagueContext))]
    [Migration("20170401100824_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Model.Dtos.League.LeagueDto", b =>
                {
                    b.Property<string>("ParticipantId");

                    b.Property<string>("Queue");

                    b.Property<string>("Tier");

                    b.HasKey("ParticipantId");

                    b.ToTable("League");
                });

            modelBuilder.Entity("WebApi.Model.Dtos.League.LeagueEntryDto", b =>
                {
                    b.Property<string>("PlayerOrTeamId");

                    b.Property<string>("Division");

                    b.Property<string>("LeagueDtoParticipantId");

                    b.Property<string>("PlayerOrTeamName");

                    b.HasKey("PlayerOrTeamId");

                    b.HasIndex("LeagueDtoParticipantId");

                    b.ToTable("LeagueEntry");
                });

            modelBuilder.Entity("WebApi.Model.Dtos.League.LeagueEntryDto", b =>
                {
                    b.HasOne("WebApi.Model.Dtos.League.LeagueDto")
                        .WithMany("Entries")
                        .HasForeignKey("LeagueDtoParticipantId");
                });
        }
    }
}
