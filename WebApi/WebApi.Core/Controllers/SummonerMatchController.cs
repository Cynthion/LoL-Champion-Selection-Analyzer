using Microsoft.AspNetCore.Mvc;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Entities;

namespace WebApi.Core.Controllers
{
    public class SummonerMatchController : BaseController<SummonerMatch>
    {
        public SummonerMatchController(ISummonerMatchRepository repository)
            : base(repository)
        {
        }

        // GET /api/[controller]/{id}
        [HttpGet("{id}", Name = "GetSummonerMatch")]
        public override IActionResult GetById(long entityId)
        {
            return base.GetById(entityId);
        }

        protected override IActionResult DoCreate(SummonerMatch entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            Repository.Add(entity);

            return CreatedAtRoute("GetSummonerMatch", new { id = entity.Key }, entity);
        }

        protected override IActionResult DoUpdate(long entityId, SummonerMatch entity)
        {
            if (entity == null || entity.Key != entityId)
            {
                return BadRequest();
            }

            var existingEntity = Repository.Find(entityId);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.SummonerId = entity.SummonerId;
            existingEntity.MatchId = entity.MatchId;
            existingEntity.Timestamp = entity.Timestamp;
            existingEntity.Champion = entity.Champion;
            existingEntity.Season = entity.Season;
            existingEntity.Region = entity.Region;
            existingEntity.Queue = entity.Queue;
            existingEntity.Lane = entity.Lane;
            existingEntity.Role = entity.Role;

            Repository.Update(existingEntity);
            return new NoContentResult();
        }
    }
}
