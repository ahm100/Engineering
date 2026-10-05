using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;
using Engineering.Domain.Entities.DailyProjectOperations;
namespace Engineering.Application.Abstractions.Data.DailyProjectOperations;

public interface IDailyProjectOperationHistoryRepository : IBaseRepository<DailyProjectOperationHistory>
{
    Task<List<GetDailyProjectOperationHistoryByIdModel>?> GetDailyProjectOperationHistoryById(long id, CT ct);
}