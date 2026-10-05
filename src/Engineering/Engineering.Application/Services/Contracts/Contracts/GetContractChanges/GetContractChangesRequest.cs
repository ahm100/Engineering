using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractChanges;

public record GetContractChangesRequest(
    long ContractId,
    ContractChangeType? Type,
    DateTime? DateFrom,
    DateTime? DateTo,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
