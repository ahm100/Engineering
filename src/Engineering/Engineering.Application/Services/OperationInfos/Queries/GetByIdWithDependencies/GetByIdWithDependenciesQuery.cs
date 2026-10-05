using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetByIdWithDependencies;

public record GetByIdWithDependenciesQuery(
    long Id
    ) : IQuery<OperationInfo>;