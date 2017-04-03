using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity> : Controller, ICrudController<TEntity>
    {
        protected readonly ICrudRepository<TEntity> Repository;

        protected BaseController(ICrudRepository<TEntity> repository)
        {
            Repository = repository;
        }

        // GET /api/[controller]
        [HttpGet]
        public IEnumerable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        // GET /api/[controller]/count
        [HttpGet("count")]
        public long GetCount()
        {
            return Repository.Count();
        }

        // GET /api/[controller]/{id}
        [HttpGet("{id}")]
        public virtual IActionResult GetById(long entityId)
        {
            var entity = Repository.Find(entityId);
            if (entity == null)
            {
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/[controller]
        [HttpPost]
        public IActionResult Create([FromBody] TEntity entity)
        {
            return DoCreate(entity);
        }

        protected abstract IActionResult DoCreate(TEntity entity);

        // PUT api/[controller]/{id}
        [HttpPut("{id}")]
        public IActionResult Update(long entityId, [FromBody]TEntity entity)
        {
            return DoUpdate(entityId, entity);
        }

        protected abstract IActionResult DoUpdate(long entityId, TEntity entity);

        // DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long playerId)
        {
            var entity = Repository.Find(playerId);
            if (entity == null)
            {
                return NotFound();
            }

            Repository.Remove(playerId);
            return new NoContentResult();
        }
    }
}
