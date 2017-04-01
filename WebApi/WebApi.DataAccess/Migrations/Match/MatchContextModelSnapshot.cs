using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.DataAccess.DbContexts;

namespace WebApi.DataAccess.Migrations.Match
{
    [DbContext(typeof(MatchContext))]
    partial class MatchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
        }
    }
}
