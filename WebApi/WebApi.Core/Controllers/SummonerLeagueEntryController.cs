using Microsoft.AspNetCore.Mvc;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Entities;

namespace WebApi.Core.Controllers
{
    public class SummonerLeagueEntryController : BaseController<SummonerLeagueEntry>
    {
        public SummonerLeagueEntryController(ISummonerLeagueEntryRepository repository)
            : base(repository)
        {
        }

        // GET /api/[controller]/{id}
        [HttpGet("{id}", Name = "GetSummonerLeagueEntry")]
        public override IActionResult GetById(long entityId)
        {
            return base.GetById(entityId);
        }

        protected override IActionResult DoCreate(SummonerLeagueEntry entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            Repository.Add(entity);

            return CreatedAtRoute("GetSummonerLeagueEntry", new {id = entity.PlayerId}, entity);
        }

        protected override IActionResult DoUpdate(long entityId, SummonerLeagueEntry entity)
        {
            if (entity == null || entity.PlayerId != entityId)
            {
                return BadRequest();
            }

            var existingEntity = Repository.Find(entityId);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.LeaguePoints = entity.LeaguePoints;
            existingEntity.Wins = entity.Wins;
            existingEntity.Losses = entity.Losses;
            existingEntity.Region = entity.Region;

            Repository.Update(existingEntity);
            return new NoContentResult();
        }
    }
}
