using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;

public record GetFilteredContractsRequest(
    long? ContractNumber,
    string? FaTitle,
    string? EnTitle,
    long? ProjectId,
    long? ContractPartyId,
    ContractStatus? Status,
    ContractDurationUnit? DurationUnit,
    DateTime? StartDateFrom,
    DateTime? StartDateTo,
    DateTime? EndDateFrom,
    DateTime? EndDateTo,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
) : IHttpRequest;