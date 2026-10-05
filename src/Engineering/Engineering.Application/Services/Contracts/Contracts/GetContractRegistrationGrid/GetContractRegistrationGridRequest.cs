using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;

public record GetContractRegistrationGridRequest(
    long? ContractNumber,
    long? ContractNumberFrom,
    long? ContractNumberTo,
    ContractStatus? Status,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
