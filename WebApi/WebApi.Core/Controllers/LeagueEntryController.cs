using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.League;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class LeagueEntryController : Controller
    {
        private readonly ILeagueEntryRepository _repository;

        public LeagueEntryController(ILeagueEntryRepository repository)
        {
            _repository = repository;
        }

        // GET /api/leagueentry
        [HttpGet]
        public IEnumerable<LeagueEntry> GetAll()
        {
            return _repository.GetAll();
        }

        // GET /api/leagueentry/count
        // TODO how to find this action?
        public long GetCount()
        {
            return _repository.Count();
        }

        // GET /api/leagueentry/{id}
        [HttpGet("{id}", Name = "GetLeague")]
        public IActionResult GetById(long playerOrTeamId)
        {
            var entity = _repository.Find(playerOrTeamId);
            if (entity == null)
            {
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/leagueentry
        [HttpPost]
        public IActionResult Create([FromBody]LeagueEntry entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _repository.Add(entity);

            return CreatedAtRoute("GetLeague", new { id = entity.PlayerOrTeamId }, entity);
        }

        // PUT api/leagueentry/{id}
        [HttpPut("{id}")]
        public IActionResult Update(long playerOrTeamId, [FromBody]LeagueEntry entity)
        {
            if (entity == null || entity.PlayerOrTeamId != playerOrTeamId)
            {
                return BadRequest();
            }

            var existingEntity = _repository.Find(playerOrTeamId);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.PlayerOrTeamName = entity.PlayerOrTeamName;
            existingEntity.Division = entity.Division;
            existingEntity.Region = entity.Region;

            _repository.Update(existingEntity);
            return new NoContentResult();
        }

        // DELETE api/leagueentry/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long playerOrTeamId)
        {
            var entity = _repository.Find(playerOrTeamId);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.Remove(playerOrTeamId);
            return new NoContentResult();
        }
    }
}
