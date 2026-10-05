using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;

namespace Engineering.Application.Services.SessionRecords.Queries.GetSessionRecordDetail;

public record GetSessionRecordDetailQuery(
    long Id) : IQuery<GetSessionRecordDetailResponse?>;