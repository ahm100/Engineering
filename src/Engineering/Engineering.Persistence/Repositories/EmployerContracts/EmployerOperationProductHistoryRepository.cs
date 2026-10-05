using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerOperationProductHistoryRepository : BaseRepository<EngineeringDBContext, EmployerOperationProductHistory>, IEmployerOperationProductHistoryRepository
{
    public EmployerOperationProductHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<GetEOProductHistoryModel>> GetEOProductHistory(
    long id,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
       .Where(e => e.EmployerOperationProductId == id)
       .Select(x => new GetEOProductHistoryModel()
       {
           Id = x.Id,
           EOProductId = x.EmployerOperationProductId,
           ProductGroupId = x.ProductGroupId,
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
           IsStandard = x.IsStandard,
           Description = x.Description,
           Created = x.Created,
           CreatorId = x.CreatorId,
       });

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);
    }
}