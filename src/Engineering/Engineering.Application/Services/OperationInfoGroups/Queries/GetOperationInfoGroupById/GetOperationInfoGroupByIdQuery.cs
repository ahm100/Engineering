using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetOperationInfoGroupById;

public record GetOperationInfoGroupByIdQuery(
    long Id
    ) : IQuery<OperationInfoGroup>;