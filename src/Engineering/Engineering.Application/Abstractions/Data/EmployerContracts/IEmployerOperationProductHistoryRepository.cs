using Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerOperationProductHistoryRepository : IBaseRepository<EmployerOperationProductHistory>
{
    Task<List<GetEOProductHistoryModel>> GetEOProductHistory(
    long id,
    int pageIndex,
    int pageSize,
    CT ct);
}