using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Entities;

namespace UserService.Controllers
{
    public abstract class DatabaseController<TEntity> : ControllerBase where TEntity : Entity, new()
    {
        protected readonly IEntityManager<TEntity> _manager;

        public DatabaseController(IEntityManager<TEntity> manager)
        {
            this._manager = manager;
        }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult<TEntity>> Get([FromQuery]TEntity item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var entity = await _manager.Get(item);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(string id)
        {
            Guid.TryParse(id, out Guid guid);

            if (guid == Guid.Empty)
            {
                return BadRequest();
            }

            return await this.Get(new TEntity { ID = guid });
        }

        [HttpGet("all")]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var entities = await _manager.GetAll();

            return new JsonResult(entities);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Post([FromBody]TEntity item)
        {
            var entity = await _manager.AddOrUpdate(item);

            return CreatedAtAction("Get", entity);
        }

        [HttpPost("{id}")]
        public virtual async Task<ActionResult<TEntity>> Post(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            return await this.Post(new TEntity { ID = id });
        }

        [HttpDelete]
        public virtual async Task<ActionResult<TEntity>> Delete(TEntity item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            await _manager.Remove(item);

            return item;
        }

        [HttpDelete]
        public virtual async Task<ActionResult<TEntity>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            return await this.Delete(new TEntity { ID = id });
        }
    }
}