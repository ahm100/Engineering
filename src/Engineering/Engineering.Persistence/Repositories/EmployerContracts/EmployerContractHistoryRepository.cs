using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public partial class EmployerContractHistoryRepository : BaseRepository<EngineeringDBContext, EmployerContractHistory>, IEmployerContractHistoryRepository
{
    public EmployerContractHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
    public async Task<List<GetEContractHistoryModel>> GetEContractHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
       .Where(e => e.EmployerContractId == id)
       .Select(contract => new GetEContractHistoryModel()
       {
           Id = contract.Id,
           IsFirst = contract.IsFirst,
           Status = contract.Status,
           Code = contract.Code,
           HeadCode = contract.EmployerContract.EmployerContractHead.Code,
           CurrencyRate = contract.CurrencyRate,
           StartDate = contract.StartDate,
           EndDate = contract.EndDate,
           TotalAmount = contract.TotalAmount,
           AdvancePayment = contract.AdvancePayment,
           Description = contract.Description
       });

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);
    }
}