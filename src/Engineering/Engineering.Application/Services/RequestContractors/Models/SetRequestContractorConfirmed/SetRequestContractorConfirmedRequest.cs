namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorConfirmed;

public record SetRequestContractorConfirmedRequest(
    long RequestContractorId,
    string? Description) : IHttpRequest;
