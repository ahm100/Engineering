namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorRejected;

public record SetRequestContractorRejectedRequest(
    long RequestContractorId,
    string? Description) : IHttpRequest;
