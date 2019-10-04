using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace inttrust.core.service
{
    /// <summary>
    /// Generic service for CRUD functions
    /// </summary>
    /// <typeparam name="TEnt"></typeparam>
    public interface IEntityService<TEnt, TContext>
        where TEnt : class
        where TContext : DbContext
    {
        /// <summary>
        /// Generic Get entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEnt> GetEntities();
        /// <summary>
        /// Generic Get entities that match a where clause
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<TEnt> GetEntities(Expression<Func<TEnt, bool>> where);
        /// <summary>
        /// Generic Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEnt GetEntity(int id);
        /// <summary>
        /// Generic Create a new entity
        /// </summary>
        /// <param name="entity"></param>
        void CreateEntity(TEnt entity);
        /// <summary>
        /// Generic Update an entity
        /// </summary>
        /// <param name="entity"></param>
        void UpdateEntity(TEnt entity);
        /// <summary>
        /// Generic Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void DeleteEntity(TEnt entity);
        /// <summary>
        /// Save changes to database
        /// </summary>
        void Save();
    }
}
