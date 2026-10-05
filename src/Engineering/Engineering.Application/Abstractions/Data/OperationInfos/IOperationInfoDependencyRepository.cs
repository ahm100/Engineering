using Engineering.Domain.Entities.OperationInfos.Enums;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoDependencyRepository : IBaseRepository<OperationInfoDependency>
{
    Task<OperationInfoDependency?> GetOperationInfoDependence(long operationInfoId, CT ct);
    Task<OperationInfoDependency?> FindForDelete(long id, CT ct);
    Task<(List<OperationInfoDependency> Data, int RowCount)> GetsDependentOnOperationInfo(long operationInfoId, CT ct);
    Task<(List<OperationInfoDependency> Data, int RowCount)> FindOperationInfoDependencies(long operationInfoId, CT ct);
    Task<(List<OperationInfoDependency> Data, int RowCount)> GetOperationInfoDependencies(long operationInfoId, OperationInfoDependencyType? dependencyType, string[]? orderBy, int pageIndex, int pageSize, CT ct);
}