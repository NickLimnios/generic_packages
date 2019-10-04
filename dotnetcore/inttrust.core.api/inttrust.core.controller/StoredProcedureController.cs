using inttrust.core.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inttrust.core.controller
{
    public class StoredProcedureController<TQueryResult, TContext> : ControllerBase
        where TQueryResult : class
        where TContext : DbContext
    {
        protected readonly IStoredProcedureService<TQueryResult, TContext> storedProcedureService;

        public StoredProcedureController(IStoredProcedureService<TQueryResult, TContext> storedProcedureService)
        {
            this.storedProcedureService = storedProcedureService;
        }


        [HttpGet]
        public IActionResult GetStoredProcedureResult(string storedprocedureName)
        {
            return Ok(storedProcedureService.GetStoredProcedureResult(storedprocedureName));
        }
    }
}
