namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;

public record GetContractAdjustmentReferencesRequest(
    bool? IsActive,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
