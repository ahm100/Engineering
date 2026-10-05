using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationById;

public record GetOperationLocationByIdQuery(
    long Id
    ) : IQuery<OperationLocation?>;