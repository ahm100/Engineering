using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerContractHistoryRepository : IBaseRepository<EmployerContractHistory>
{
    Task<List<GetEContractHistoryModel>> GetEContractHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);
}