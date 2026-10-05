using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperationDetails;

public record GetSchedulingOperationLocationsQuery(
    long CostCenterId,
    long ProjectId,
    string? FilterData,
    List<long>? OperationInfoIds,
    List<long>? OperationLocationIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationLocation>>>;
