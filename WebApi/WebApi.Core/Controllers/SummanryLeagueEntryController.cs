using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Entities;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class SummanryLeagueEntryController : Controller
    {
        private readonly ISummonerLeagueEntryRepository _repository;

        public SummanryLeagueEntryController(ISummonerLeagueEntryRepository repository)
        {
            _repository = repository;
        }

        // GET /api/[controller]
        [HttpGet]
        public IEnumerable<SummonerLeagueEntry> GetAll()
        {
            return _repository.GetAll();
        }

        // GET /api/[controller]/count
        [HttpGet("count")]
        public long GetCount()
        {
            return _repository.Count();
        }

        // GET /api/[controller]/{id}
        [HttpGet("{id}", Name = "GetSummonerLeagueEntry")]
        public IActionResult GetById(long playerId)
        {
            var entity = _repository.Find(playerId);
            if (entity == null)
            {
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/[controller]
        [HttpPost]
        public IActionResult Create([FromBody]SummonerLeagueEntry entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _repository.Add(entity);

            return Created("GetSummonerLeagueEntry", entity);
        }

        // PUT api/[controller]/{id}
        [HttpPut("{id}")]
        public IActionResult Update(long playerId, [FromBody]SummonerLeagueEntry entity)
        {
            if (entity == null || entity.PlayerId != playerId)
            {
                return BadRequest();
            }

            var existingEntity = _repository.Find(playerId);
            if (existingEntity == null)
            {
                return NotFound();
            }

            existingEntity.LeaguePoints = entity.LeaguePoints;
            existingEntity.Wins = entity.Wins;
            existingEntity.Losses = entity.Losses;
            existingEntity.Region = entity.Region;

            _repository.Update(existingEntity);
            return new NoContentResult();
        }

        // DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long playerId)
        {
            var entity = _repository.Find(playerId);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.Remove(playerId);
            return new NoContentResult();
        }
    }
}
