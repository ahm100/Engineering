using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractStatus;

public record ChangeContractStatusRequest(
    long Id,
    ContractStatusTransitionType Operation,
    DateTime? EffectiveDate,
    int? SuspensionDurationMonths,
    string? Reason,
    string? Description,
    List<string>? Urls) : IHttpRequest;
