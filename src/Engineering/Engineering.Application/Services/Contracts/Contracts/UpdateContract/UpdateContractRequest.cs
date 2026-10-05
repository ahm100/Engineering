using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContract;

public record UpdateContractRequest(
    long Id,
    string FaTitle,
    string? EnTitle,
    string? Description,
    long ProjectId,
    long ContractPartyId,
    DateTime StartDate,
    int Duration,
    ContractDurationUnit DurationUnit,
    List<string>? Urls = null) : IHttpRequest;
