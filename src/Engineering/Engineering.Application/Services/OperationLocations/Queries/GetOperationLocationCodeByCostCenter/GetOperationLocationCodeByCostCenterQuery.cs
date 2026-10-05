using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByCostCenter;

public record GetOperationLocationCodeByCostCenterQuery(
    string PrivateCode,
    long? CostCenterId,
    long? ProjectId
    ) : IQuery<OperationLocation?>;