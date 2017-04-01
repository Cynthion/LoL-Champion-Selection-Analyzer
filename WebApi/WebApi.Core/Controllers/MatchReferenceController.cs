using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.Match;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class MatchReferenceController : Controller
    {
        private readonly IMatchReferenceRepository _repository;

        public MatchReferenceController(IMatchReferenceRepository repository)
        {
            _repository = repository;
        }

        // GET /api/matchreference
        [HttpGet]
        public IEnumerable<MatchReference> GetAll()
        {
            return _repository.GetAll();
        }

        // GET /api/matchreference/{id}
        [HttpGet("{id}", Name = "GetMatchReference")]
        public IActionResult GetById(long matchId)
        {
            var entity = _repository.Find(matchId);
            if (entity == null)
            {
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/matchreference
        [HttpPost]
        public IActionResult Create([FromBody]MatchReference entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _repository.Add(entity);

            return CreatedAtRoute("GetMatchReference", new { id = entity.MatchId }, entity);
        }

        // PUT api/matchreference/{id}
        [HttpPut("{id}")]
        public IActionResult Update(long matchId, [FromBody]MatchReference entity)
        {
            if (entity == null || entity.MatchId != matchId)
            {
                return BadRequest();
            }

            var existingEntity = _repository.Find(matchId);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.Champion = entity.Champion;
            existingEntity.Timestamp = entity.Timestamp;
            existingEntity.Season = entity.Season;
            existingEntity.Region = entity.Region;
            existingEntity.Queue = entity.Queue;
            existingEntity.Lane = entity.Lane;
            existingEntity.Role = entity.Role;
            existingEntity.PlatformId = entity.PlatformId;

            _repository.Update(existingEntity);
            return new NoContentResult();
        }

        // DELETE api/matchreference/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long matchId)
        {
            var entity = _repository.Find(matchId);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.Remove(matchId);
            return new NoContentResult();
        }
    }
}
