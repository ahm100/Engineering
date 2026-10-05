using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Abstractions.Data.Machineries;

public interface IMachineriesGroupRepository : IBaseRepository<MachineriesGroup>
{
    Task<MachineriesGroup?> FindByName(string name, long? companyId, CT ct);
    Task<MachineriesGroup?> FindByCode(string code, long? companyId, CT ct);
    Task<MachineriesGroup?> HaveMachineriesGroupChild(long id, CT ct);
    Task<MachineriesGroup?> GetMachineriesGroupForDelete(long id, CT ct);
    Task<bool> FindMachineriesGroupByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct);

    Task<List<MachineriesGroup>> GetsMachineriesGroupByIds(List<long> ids, CT ct);
    Task<string> CodeCreator(long? companyId, CT ct);
    Task<(List<MachineriesGroup> Data, int RowCount)> GetsMachineriesGroupByCodes(List<string> codes, long? companyId, CT ct);
    Task<(List<MachineriesGroup> Data, int RowCount)> GetMachineriesGroups(List<long>? ids, string? filterData, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<MachineriesGroup> Data, int RowCount)> GetsMachineryGroupsForRequestMachinery(long? projectId, List<long>? projectOperationIds, List<long>? projectOperationDetailIds,
        string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct);
    Task<(List<MachineriesGroup> Data, int RowCount)> GetActiveMachineriesGroups(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct);
}
