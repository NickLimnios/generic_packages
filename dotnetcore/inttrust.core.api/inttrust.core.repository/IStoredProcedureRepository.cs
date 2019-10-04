using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace inttrust.core.repository
{
    public interface IStoredProcedureRepository<TQueryResult, TContext> 
        where TQueryResult : class
        where TContext : DbContext
    {
        IEnumerable<TQueryResult> GetStoredProcedureResult(string storedProcedureName);
    }
}
