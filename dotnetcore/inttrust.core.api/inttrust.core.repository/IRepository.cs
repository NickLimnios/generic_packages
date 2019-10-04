using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace inttrust.core.repository
{
    /// <summary>
    /// Generic Repository for CRUD functions
    /// </summary>
    /// <typeparam name="TEnt"></typeparam>
    public interface IRepository<TEnt, TContext> 
        where TEnt : class 
        where TContext : DbContext
    {
        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEnt entity);
        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEnt entity);
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEnt entity);
        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEnt GetById(int id);
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEnt> GetAll();
        /// <summary>
        /// Get entities that matches a where clause
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<TEnt> GetMany(Expression<Func<TEnt, bool>> where);
        /// <summary>
        /// Save DbContext changes
        /// </summary>
        void Save();
    }
}
