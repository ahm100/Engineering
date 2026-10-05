using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.HaveOperationLocationChild;

public record HaveOperationLocationChildQuery(
    long Id
    ) : IQuery<OperationLocation?>;