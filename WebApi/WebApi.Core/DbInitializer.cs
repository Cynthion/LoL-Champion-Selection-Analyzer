using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApi.Core.DbContexts;
using WebApi.Model.Dtos.League;

namespace WebApi.Core
{
    public static class DbInitializer
    {
        public static void Initialize(LeagueContext context)
        {
            context.Database.EnsureCreated();

            if (context.Leagues.Any())
            {
                return;
            }

            context.Leagues.Add(new LeagueDto
            {
                ParticipantId = "TestParticipant",
                Queue = "TestQueue",
                Tier = "TestTier"
            });
            context.SaveChanges();
        }
    }
}
