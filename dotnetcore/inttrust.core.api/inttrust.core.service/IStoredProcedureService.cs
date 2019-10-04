using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace inttrust.core.service
{
    public interface IStoredProcedureService<TQueryResult, TContext>
        where TQueryResult : class
        where TContext : DbContext
    {
        IEnumerable<TQueryResult> GetStoredProcedureResult(string storedProcedureName);
    }
}
