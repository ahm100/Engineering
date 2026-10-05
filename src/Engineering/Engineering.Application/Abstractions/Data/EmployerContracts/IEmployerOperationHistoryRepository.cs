using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerOperationHistoryRepository : IBaseRepository<EmployerOperationHistory>
{
    Task<List<GetEContractOperationHistoryModel>> GetEContractOperationHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct);
}