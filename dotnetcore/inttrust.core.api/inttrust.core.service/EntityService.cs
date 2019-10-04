using inttrust.core.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace inttrust.core.service
{
    public class EntityService<TEnt, TContext> : IEntityService<TEnt, TContext>
        where TEnt : class
        where TContext : DbContext
    {
        private readonly IRepository<TEnt, TContext> entityRepository;

        public EntityService(IRepository<TEnt, TContext> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        #region IEntityService Members
        public virtual IEnumerable<TEnt> GetEntities()
        {
            return entityRepository.GetAll();
        }

        public virtual IEnumerable<TEnt> GetEntities(Expression<Func<TEnt, bool>> where)
        {
            return entityRepository.GetMany(where);
        }

        public virtual TEnt GetEntity(int id)
        {
            return entityRepository.GetById(id);
        }

        public virtual void CreateEntity(TEnt entity)
        {
            entityRepository.Add(entity);
        }

        public virtual void UpdateEntity(TEnt entity)
        {
            entityRepository.Update(entity);
        }

        public virtual void DeleteEntity(TEnt entity)
        {
            entityRepository.Delete(entity);
        }

        public void Save()
        {
            entityRepository.Save();
        }

        #endregion

    }
}
