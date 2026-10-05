using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.FindOperationInfoDependencies;

public record FindOperationInfoDependenciesQuery(
    long OperationInfoId
    ) : IQuery<DataResult<List<OperationInfoDependency>>>;