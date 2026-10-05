using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByParent;

public record GetOperationLocationNameByParentQuery(
    string PrivateName,
    long ParentId,
    long? CompanyId
    ) : IQuery<OperationLocation?>;
