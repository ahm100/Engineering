using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependence;

public record GetOperationInfoDependenceQuery(
    long OperationInfoId
    ) : IQuery<OperationInfoDependency?>;