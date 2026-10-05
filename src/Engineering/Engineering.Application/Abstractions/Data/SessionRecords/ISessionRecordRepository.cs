using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;
using Engineering.Domain.Entities.SessionRecords;
using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Abstractions.Data.SessionRecords;

public interface ISessionRecordRepository : IBaseRepository<SessionRecord>
{
    Task<SessionRecord?> GetById(
        long id);

    Task<Result<(List<GetSessionRecordsResponseModel> Data, int RowCount)>> GetSessionRecords(
        string? titleFa, string? titleEn, string? code,
        SessionCategory? category, SessionType? type, long? projectId,
        int pageIndex, int pageSize, CT ct);

    Task<GetSessionRecordDetailResponse?> GetSessionRecordDetail(
        long id, CT ct);
}
