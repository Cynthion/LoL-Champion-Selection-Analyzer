using Microsoft.AspNetCore.Mvc;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Entities;

namespace WebApi.Core.Controllers
{
    public class MatchController : BaseController<Match>
    {
        public MatchController(IMatchRepository repository) 
            : base(repository)
        {
        }

        // GET /api/[controller]/{id}
        [HttpGet("{id}", Name = "GetMatch")]
        public override IActionResult GetById(long entityId)
        {
            var entity = Repository.Find(entityId);
            if (entity == null)
            {
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        protected override IActionResult DoCreate(Match entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            Repository.Add(entity);

            return CreatedAtRoute("GetMatch", new { id = entity.MatchId }, entity);
        }

        protected override IActionResult DoUpdate(long entityId, Match entity)
        {
            if (entity == null || entity.MatchId != entityId)
            {
                return BadRequest();
            }

            var existingEntity = Repository.Find(entityId);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.MatchCreation = entity.MatchCreation;
            existingEntity.MatchDuration = entity.MatchDuration;
            existingEntity.Season = entity.Season;
            existingEntity.Region = entity.Region;
            existingEntity.QueueType = entity.QueueType;
            existingEntity.Teams = entity.Teams;

            Repository.Update(existingEntity);
            return new NoContentResult();
        }
    }
}
