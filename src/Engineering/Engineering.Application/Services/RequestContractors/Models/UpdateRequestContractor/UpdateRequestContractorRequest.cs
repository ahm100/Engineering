namespace Engineering.Application.Services.RequestContractors.Models.UpdateRequestContractor;
public record UpdateRequestContractorRequest(
    long Id,
    decimal Volume,
    string? Description) : IHttpRequest;
