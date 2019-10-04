using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace inttrust.core.repository
{
    public class Repository<TEnt, TContext> : IRepository<TEnt, TContext>
      where TEnt : class
      where TContext : DbContext
    {
        private readonly DbSet<TEnt> dbSet;

        protected TContext DbContext { get; }

        public Repository(TContext dbContext)
        {
            DbContext = dbContext;

            dbSet = DbContext.Set<TEnt>();
        }

        #region Implementation

        public void Add(TEnt entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEnt entity)
        {
            dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEnt entity)
        {
            dbSet.Remove(entity);
        }

        public TEnt GetById(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEnt> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<TEnt> GetMany(Expression<Func<TEnt, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }
        #endregion
    }
}
