using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;
using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Application.Abstractions.Data.SessionRecords;

public interface ISessionRecordActionRepository : IBaseRepository<SessionRecordAction>
{
    Task<(List<GetUserSessionRecordActionResponseModel> Data, int RowCount)> GetUserSessionRecordAction(
            long userId, string? description, int pageIndex, int pageSize, CT ct);
}
