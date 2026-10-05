using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetsDependentOnOperationInfo;

public record GetsDependentOnOperationInfoQuery(
    long OperationInfoId
    ) : IQuery<DataResult<List<OperationInfoDependency>>>;