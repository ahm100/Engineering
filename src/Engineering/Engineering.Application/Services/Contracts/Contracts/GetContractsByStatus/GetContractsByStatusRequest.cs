using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;

public record GetContractsByStatusRequest(
    ContractStatus Status,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
) : IHttpRequest;