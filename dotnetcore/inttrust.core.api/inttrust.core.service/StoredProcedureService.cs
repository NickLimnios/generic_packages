using inttrust.core.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace inttrust.core.service
{
    public class StoredProcedureService<TQueryResult, TContext> : IStoredProcedureService<TQueryResult, TContext>
        where TQueryResult : class
        where TContext : DbContext
    {
        private readonly IStoredProcedureRepository<TQueryResult, TContext> storedProcedureRepository;
        public StoredProcedureService(IStoredProcedureRepository<TQueryResult, TContext> storedProcedureRepository)
        {
            this.storedProcedureRepository = storedProcedureRepository;
        }

        public IEnumerable<TQueryResult> GetStoredProcedureResult(string storedProcedureName)
        {
            return storedProcedureRepository.GetStoredProcedureResult(storedProcedureName);
        }
    }
}
