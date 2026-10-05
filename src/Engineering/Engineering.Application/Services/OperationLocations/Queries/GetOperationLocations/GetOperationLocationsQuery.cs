using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocations;

public record GetOperationLocationsQuery(
    List<long>? Ids,
    long? CostCenterId,
    long? ProjectId,
    long? ParentId,
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationLocation>>>;