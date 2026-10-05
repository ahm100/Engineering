using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByName;

public record GetOperationLocationByNameQuery(
    string PrivateName,
    long? CompanyId
    ) : IQuery<OperationLocation?>;