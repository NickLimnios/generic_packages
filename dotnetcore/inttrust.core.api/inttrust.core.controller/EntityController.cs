using inttrust.core.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace inttrust.core.controller
{
    /// <summary>
    /// Generic Controller with CRUD Calls
    /// </summary>
    /// <typeparam name="TEnt"></typeparam>
    public class EntityController<TEnt, TContext> : ControllerBase
        where TEnt : class
        where TContext : DbContext
    {
        protected readonly IEntityService<TEnt, TContext> entityService;

        public EntityController(IEntityService<TEnt, TContext> entityService)
        {
            this.entityService = entityService;
        }

        /// <summary>
        /// Generic GET call to fetch entities of given type.
        /// Header parameters filter results.
        /// </summary>
        /// <returns>200 Status with list of entities</returns>
        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(entityService.GetEntities().ToList());
        }

        /// <summary>
        /// Generic GET call to fetch an entity by id, of given type.
        ///  Header parameters filter result.
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>200 Status with entity or NotFound result</returns>
        [HttpGet]
        public virtual IActionResult GetById(int id)
        {
            TEnt entity = entityService.GetEntity(id);

            if (entity != null)
                return Ok(entity);
            else
                return NotFound();
        }

        /// <summary>
        /// Generic POST call to create a new entity of given type.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>201 Status</returns>
        [HttpPost]
        public virtual IActionResult Create([FromBody] TEnt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entityService.CreateEntity(entity);
            entityService.Save();

            return CreatedAtAction(nameof(Get), entity);
        }

        /// <summary>
        /// Generic POST call to update an entity of given type.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>200 Status with entity</returns>
        [HttpPut]
        public virtual IActionResult Update([FromBody] TEnt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entityService.UpdateEntity(entity);
            entityService.Save();

            return Ok(entity);
        }

        /// <summary>
        /// Generic DELETE call to delete an entity of given type.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>200 Status</returns>
        [HttpDelete]
        public virtual IActionResult Delete([FromBody] TEnt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entityService.DeleteEntity(entity);
            entityService.Save();

            return Ok();
        }
    }
}
