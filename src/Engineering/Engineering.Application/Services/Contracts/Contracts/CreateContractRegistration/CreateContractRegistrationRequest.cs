using Engineering.Application.Services.Contracts.Contracts.ContractRegistration;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractRegistration;

public record CreateContractRegistrationRequest(
    long ContractNumber,
    string FaTitle,
    string? EnTitle,
    string? Description,
    long ProjectId,
    long ContractPartyId,
    DateTime StartDate,
    int Duration,
    ContractDurationUnit DurationUnit,
    DateTime? EndDate,
    ContractStatus Status,
    string ContractTypeCode,
    PricingMethod PricingMethod,
    List<string>? Urls,
    ContractRegistrationFinancialRequest Financial) : IHttpRequest;
