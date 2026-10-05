namespace Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;

public record GetAvailableContractTypeDetailSourcesRequest(
    long ContractId,
    long ContractTypeId,
    long? ProjectOperationId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;