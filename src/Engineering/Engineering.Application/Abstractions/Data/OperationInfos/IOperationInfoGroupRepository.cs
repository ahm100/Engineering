using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoGroupRepository : IBaseRepository<OperationInfoGroup>
{
    Task<OperationInfoGroup?> GetById(long Id, CT ct);
    Task<OperationInfoGroup?> FindByName(string name, long? companyId, CT ct);
    Task<OperationInfoGroup?> FindByCode(string code, long? companyId, CT ct);
    Task<OperationInfoGroup?> FindForDelete(long id, CT ct);
    Task<bool> FindOperationInfoGroupByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct);
    Task<string> CodeCreator(long? companyId, CT ct);

    Task<(List<OperationInfoGroup> Data, int RowCount)> GetByOperationInfoGroupIds(List<long> groupIds, int pageIndex, int pageSize, CT ct);
    Task<(List<OperationInfoGroup> Data, int RowCount)> GetsOperationInfoGroup(List<long>? ids, string? filterData, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<OperationInfoGroup> Data, int RowCount)> GetActiveOperationInfoGroups(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct);

}