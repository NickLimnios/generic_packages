using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace inttrust.core.repository
{
    public class StoredProcedureRepository<TQueryResult, TContext> : IStoredProcedureRepository<TQueryResult, TContext>
        where TQueryResult : class
        where TContext : DbContext
    {
        protected TContext DbContext { get; }

        public StoredProcedureRepository(TContext dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<TQueryResult> GetStoredProcedureResult(string storedProcedureName)
        {
            return DbContext.Query<TQueryResult>().FromSql(storedProcedureName).ToList();
        }
    }
}
