namespace Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;

public record GetUserSessionRecordActionRequest(
    string? Description,
    int PageIndex,
    int PageSize) : IHttpRequest;