using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsByCostCenterId;

public record GetsByCostCenterIdQuery(
    long? CostCenterId,
    long? ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationLocation>>>;