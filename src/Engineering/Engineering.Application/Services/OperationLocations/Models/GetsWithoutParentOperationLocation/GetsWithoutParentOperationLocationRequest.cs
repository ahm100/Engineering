
namespace Engineering.Application.Services.OperationLocations.Models.GetsWithoutParentOperationLocation;

public record GetsWithoutParentOperationLocationRequest(
    long? CostCenterId,
    long? ProjectId,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
