using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsWithoutParentOperationLocation;

public record GetsWithoutParentOperationLocationQuery(
    long? CostCenterId,
    long? ProjectId,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationLocation>>>;
