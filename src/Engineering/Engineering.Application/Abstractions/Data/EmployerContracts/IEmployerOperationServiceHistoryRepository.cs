using Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerOperationServiceHistoryRepository : IBaseRepository<EmployerOperationServiceHistory>
{
    Task<List<GetEOServiceHistoryModel>> GetEOServiceHistory(
    long id,
    int pageIndex,
    int pageSize,
    CT ct);
}