using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerOperationServiceHistoryRepository : BaseRepository<EngineeringDBContext, EmployerOperationServiceHistory>, IEmployerOperationServiceHistoryRepository
{
    public EmployerOperationServiceHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<GetEOServiceHistoryModel>> GetEOServiceHistory(
    long id,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
       .Where(e => e.EmployerOperationServiceId == id)
       .Select(x => new GetEOServiceHistoryModel()
       {
           Id = x.Id,
           EOServiceId = x.EmployerOperationServiceId,
           MinPrice = x.MinPrice,
           MaxPrice = x.MaxPrice,
           Tax = x.Tax,
           TaxPercent = x.TaxPercent,
           TransportationCost = x.TransportationCost,
           TransportationCostPercent = x.TransportationCostPercent,
           ProfitCost = x.ProfitCost,
           ProfitCostPercent = x.ProfitCostPercent,
           OtherCost = x.OtherCost,
           OtherCostPercent = x.OtherCostPercent,
           Description = x.Description,
           Created = x.Created,
           CreatorId = x.CreatorId,
       });

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);
    }
}