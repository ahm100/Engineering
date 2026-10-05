using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;

namespace Engineering.Application.Services.SessionRecords.Queries.GetUserSessionRecordAction;

public record GetUserSessionRecordActionQuery(
    long UserId,
    string? Description,
    int PageIndex,
    int PageSize) : IQuery<GetUserSessionRecordActionResponse?>;