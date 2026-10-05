using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependencyById;

public record GetOperationInfoDependencyByIdQuery(
    long Id
    ) : IQuery<OperationInfoDependency?>;