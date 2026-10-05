using Engineering.Domain.Entities.OperationInfos.Enums;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependencies;

public record GetOperationInfoDependenciesQuery(
    long OperationInfoId,
    OperationInfoDependencyType? DependencyType,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoDependency>>>;