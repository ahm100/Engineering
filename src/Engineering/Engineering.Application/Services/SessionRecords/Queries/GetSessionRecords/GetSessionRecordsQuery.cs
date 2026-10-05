using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;
using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Services.SessionRecords.Queries.GetSessionRecords;

public record GetSessionRecordsQuery(
    string? TitleFa,
    string? TitleEn,
    string? Code,
    SessionCategory? Category,
    SessionType? Type,
    long? ProjectId,
    int PageIndex,
    int PageSize) : IQuery<GetSessionRecordsResponse>;