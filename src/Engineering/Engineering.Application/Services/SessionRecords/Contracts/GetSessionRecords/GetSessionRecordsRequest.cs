using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;

public record GetSessionRecordsRequest(
    string? TitleFa,
    string? TitleEn,
    string? Code,
    SessionCategory? Category,
    SessionType? Type,
    long? ProjectId,
    int PageIndex,
    int PageSize) : IHttpRequest;