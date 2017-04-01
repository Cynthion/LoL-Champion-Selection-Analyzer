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
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ILeagueEntryRepository _leagueEntryRepository;

        public LeagueEntryController(ILeagueEntryRepository leagueEntryRepository)
        {
            _leagueEntryRepository = leagueEntryRepository;
        }

        // GET /api/leagueentry
        [HttpGet]
        public IEnumerable<LeagueEntry> GetAll()
        {
            return _leagueEntryRepository.GetAll();
        }

        // GET /api/leagueentry/{id}
        [HttpGet("{id}", Name = "GetLeague")]
        public IActionResult GetById(long playerOrTeamId)
        {
            var item = _leagueEntryRepository.Find(playerOrTeamId);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/leagueentry
        [HttpPost]
        public IActionResult Create([FromBody]LeagueEntry item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _leagueEntryRepository.Add(item);

            return CreatedAtRoute("GetLeague", new { id = item.PlayerOrTeamId }, item);
        }

        // PUT api/leagueentry/{id}
        [HttpPut("{id}")]
        public IActionResult Update(long playerOrTeamId, [FromBody]LeagueEntry item)
        {
            if (item == null || item.PlayerOrTeamId != playerOrTeamId)
            {
                return BadRequest();
            }

            var existingItem = _leagueEntryRepository.Find(playerOrTeamId);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.PlayerOrTeamName = item.PlayerOrTeamName;
            existingItem.Division = item.Division;
            existingItem.Region = item.Region;

            _leagueEntryRepository.Update(existingItem);
            return new NoContentResult();
        }

        // DELETE api/leagueentry/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long playerOrTeamId)
        {
            var item = _leagueEntryRepository.Find(playerOrTeamId);
            if (item == null)
            {
                return NotFound();
            }

            _leagueEntryRepository.Remove(playerOrTeamId);
            return new NoContentResult();
        }
    }
}
