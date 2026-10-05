using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetActiveOperationLocations;

public record GetActiveOperationLocationsQuery(
    string? FilterData,
    long? CostCenterId,
    long? ProjectId,
    string? PrivateName,
    string? PrivateCode,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationLocation>>>;