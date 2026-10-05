namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;

public record GetContractAdjustmentIndexesRequest(
    long ReferenceId,
    bool? IsActive,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
