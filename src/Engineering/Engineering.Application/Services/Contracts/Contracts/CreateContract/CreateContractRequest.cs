using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContract;

public record CreateContractRequest(
    string FaTitle,
    string? EnTitle,
    string? Description,
    long ProjectId,
    long ContractPartyId,
    DateTime StartDate,
    int Duration,
    ContractDurationUnit DurationUnit,
    List<string>? Urls = null) : IHttpRequest;
