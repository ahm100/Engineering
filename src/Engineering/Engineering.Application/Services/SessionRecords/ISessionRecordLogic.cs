using Engineering.Application.Services.SessionRecords.Contracts.CreateSessionRecord;
using Engineering.Application.Services.SessionRecords.Contracts.EditSessionRecord;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;
using Engineering.Application.Services.SessionRecords.Contracts.RemoveSessionRecord;

namespace Engineering.Application.Services.SessionRecords;

public interface ISessionRecordLogic
{
    Task<Result<CreateSessionRecordResponse>> CreateSessionRecord(
        CreateSessionRecordRequest request, CT ct);

    Task<Result<GetUserSessionRecordActionResponse>> GetUserSessionRecordAction(
        GetUserSessionRecordActionRequest request, CT ct);

    Task<Result<RemoveSessionRecordResponse>> RemoveSessionRecord(
        RemoveSessionRecordRequest request, CT ct);

    Task<Result<GetSessionRecordsResponse>> GetSessionRecords(
        GetSessionRecordsRequest request, CT ct);

    Task<Result<EditSessionRecordResponse>> EditSessionRecord(
        EditSessionRecordRequest request, CT ct);

    Task<Result<GetSessionRecordDetailResponse>> GetSessionRecordDetail(
        GetSessionRecordDetailRequest request, CT ct);
}
