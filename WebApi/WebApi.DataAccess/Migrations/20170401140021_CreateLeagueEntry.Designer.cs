using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.DataAccess.DbContexts;

namespace WebApi.DataAccess.Migrations
{
    [DbContext(typeof(LeagueContext))]
    [Migration("20170401140021_CreateLeagueEntry")]
    partial class CreateLeagueEntry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Model.Dtos.League.LeagueEntry", b =>
                {
                    b.Property<long>("PlayerOrTeamId");

                    b.Property<string>("Division");

                    b.Property<string>("PlayerOrTeamName");

                    b.HasKey("PlayerOrTeamId");

                    b.ToTable("LeagueEntry");
                });
        }
    }
}
