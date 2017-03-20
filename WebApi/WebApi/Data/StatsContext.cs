using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Stats;

namespace WebApi.Data
{
    public class StatsContext : DbContext
    {
        public StatsContext(DbContextOptions<StatsContext> options)
            : base(options)
        {
        }

        public DbSet<AggregatedInfo> AggregatedInfos { get; set; }

        public DbSet<ChampionInfo> ChampionInfos { get; set; }

        public DbSet<RankedInfo> RankedInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AggregatedInfo>().ToTable("AggregatedInfo");
            modelBuilder.Entity<ChampionInfo>().ToTable("ChampionInfo");
            modelBuilder.Entity<RankedInfo>().ToTable("RankedInfo");
        }
    }
}
