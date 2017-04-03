using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Core.Controllers
{
    internal interface ICrudController<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        long GetCount();

        IActionResult GetById(long entityId);

        IActionResult Create(TEntity entity);

        IActionResult Update(long entityId, TEntity entity);

        IActionResult Delete(long entityId);
    }
}
