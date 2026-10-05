using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerOperationHistoryRepository : BaseRepository<EngineeringDBContext, EmployerOperationHistory>, IEmployerOperationHistoryRepository
{
    public EmployerOperationHistoryRepository(EngineeringDBContext context) : base(context)
    { }
    public async Task<List<GetEContractOperationHistoryModel>> GetEContractOperationHistory(
    long id,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
       .Where(e => e.EmployerOperation.EmployerContractId == id)
       .Select(history => new GetEContractOperationHistoryModel()
       {
           Id = history.Id,
           Price = history.UnitPrice,
           Workload = history.Id,
           Description = history.Description
       });

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);
    }
}


