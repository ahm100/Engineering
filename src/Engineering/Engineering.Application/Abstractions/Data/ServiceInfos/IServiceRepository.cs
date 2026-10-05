using Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Abstractions.Data.ServiceInfos;

public interface IServiceInfoRepository : IBaseRepository<ServiceInfo>
{
    Task<ServiceInfo?> GetById(long id, CT ct);
    Task<bool> FindServiceInfoByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct);
    Task<ServiceInfo?> FindByName(string name, long? companyId, CT ct);
    Task<ServiceInfo?> FindByCode(string code, long? companyId, CT ct);
    Task<string> CodeCreator(long? companyId, CT ct);
    Task<ServiceInfo?> ValidateServiceByName(string name, long measurementId, long? companyId, CT ct);

    Task<(List<ServiceInfo> Data, int RowCount)> GetServiceInfos(List<long>? ids, long? operationInfoId, long? categoryId, long? branchId, long? seasonId, string? filterData, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<ServiceInfo> Data, int RowCount)> GetsServiceInfoByOperationInfo(List<long>? operationInfoIds, string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct);
    Task<(List<ServiceInfo> Data, int RowCount)> GetsServiceInfoByProjectOperationIds(List<long>? projectOperationIds, string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct);
    Task<(List<ServiceInfo> Data, int RowCount)> GetsServiceInfoByIds(List<long> ids, int pageIndex, int pageSize, CT ct);
    Task<(List<ServiceInfo> Data, int RowCount)> GetActiveServiceInfos(string? filterData, string? code, string? name, long? projectId, long? companyId, int pageIndex, int pageSize, CT ct);

    Task<(List<GetsServiceInfoByProjectIdModel> Data, int RowCount)> GetsServiceInfoByProjectId(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);
}