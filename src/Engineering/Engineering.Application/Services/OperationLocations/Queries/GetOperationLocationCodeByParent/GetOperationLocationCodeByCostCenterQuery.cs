using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByParent;

public record GetOperationLocationCodeByParentQuery(
    string PrivateCode,
    long ParentId,
    long? CompanyId
    ) : IQuery<OperationLocation?>;