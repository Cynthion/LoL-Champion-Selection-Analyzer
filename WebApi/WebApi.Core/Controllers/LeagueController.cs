using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.League;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class LeagueController : Controller
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeagueController(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        // GET /api/league
        [HttpGet]
        public IEnumerable<LeagueDto> GetAll()
        {
            return _leagueRepository.GetAll();
        }

        // GET /api/league/{id}
        [HttpGet("{id}", Name = "GetLeague")]
        public IActionResult GetById(string participantId)
        {
            var item = _leagueRepository.Find(participantId);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/league
        [HttpPost]
        public IActionResult Create([FromBody]LeagueDto item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _leagueRepository.Add(item);

            return CreatedAtRoute("GetLeague", new { id = item.ParticipantId }, item);
        }

        // PUT api/league/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string participantId, [FromBody]LeagueDto item)
        {
            if (item == null || item.ParticipantId != participantId)
            {
                return BadRequest();
            }

            var existingItem = _leagueRepository.Find(participantId);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Entries = item.Entries;
            existingItem.Queue = item.Queue;
            existingItem.Tier = item.Tier;

            _leagueRepository.Update(existingItem);
            return new NoContentResult();
        }

        // DELETE api/league/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var item = _leagueRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _leagueRepository.Remove(id);
            return new NoContentResult();
        }
    }
}
