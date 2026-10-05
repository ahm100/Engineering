namespace Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;

public record GetContractChangeAvailableItemsRequest(
    long ContractId,
    long ContractTypeId,
    long? ProjectOperationId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
