using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByCode;

public record GetOperationLocationByCodeQuery(
    string PrivateCode,
    long? CompanyId
    ) : IQuery<OperationLocation?>;