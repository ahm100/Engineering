namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocations;

public record GetOperationLocationsRequest(
    long? CostCenterId,
    long? ProjectId,
    long? ParentId,
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
