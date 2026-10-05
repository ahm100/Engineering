namespace Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;

public record GetSessionRecordDetailRequest(
    long Id) : IHttpRequest;