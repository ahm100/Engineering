namespace Engineering.Application.Services.SessionRecords.Contracts.RemoveSessionRecord;

public record RemoveSessionRecordRequest(
    long Id) : IHttpRequest;