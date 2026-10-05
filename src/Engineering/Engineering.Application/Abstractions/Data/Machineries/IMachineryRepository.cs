using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Abstractions.Data.Machineries;

public interface IMachineryRepository : IBaseRepository<Machinery>
{
    Task<Machinery?> FindByName(string name, long? companyId, CT ct);
    Task<Machinery?> FindByCode(string code, long? companyId, CT ct);
    Task<bool> FindMachineryByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct);
    Task<Machinery?> FindByIdWithMachineriesGroup(long id, CT ct);
    Task<Machinery?> GetMachineryWithoutInclude(long id, CT ct);
    Task<Machinery?> GetMachineryForDelete(long id, CT ct);
    Task<Machinery?> HaveMachineryChild(long id, CT ct);

    Task<string> CodeCreator(long? companyId, CT ct);
    Task<(List<Machinery> Data, int RowCount)> GetsMachineryByIds(List<long> ids, int pageIndex, int pageSize, CT ct);
    Task<(List<Machinery> Data, int RowCount)> GetMachineries(List<long>? ids, string? filterData, long? groupId, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<Machinery> Data, int RowCount)> GetActiveMachineries(string? filterData, long? groupId, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct);
    Task<(List<Machinery> Data, int RowCount)> GetsMachineryForRequestMachinery(long? projectId, long? projectOperationId, long? projectOperationDetailId, long? machineriesGroupId,
        string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct);
    Task<(List<Machinery> Data, int RowCount)> GetByMachineriesGroupId(long groupId, int pageIndex, int pageSize, CT ct);
}