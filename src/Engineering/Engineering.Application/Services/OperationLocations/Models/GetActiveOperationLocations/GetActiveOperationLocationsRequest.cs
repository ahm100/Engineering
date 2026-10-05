namespace Engineering.Application.Services.OperationLocations.Models.GetActiveOperationLocations;

public record GetActiveOperationLocationsRequest(
    string? FilterData,
    long? CostCenterId,
    long? ProjectId,
    string? PrivateCode,
    string? PrivateName,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
