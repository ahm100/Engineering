using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByCostCenter;

public record GetOperationLocationNameByCostCenterQuery(
    string PrivateName,
    long? CostCenterId,
    long? ProjectId
    ) : IQuery<OperationLocation?>;