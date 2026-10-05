using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectTypeRepository : IBaseRepository<ProjectType>
{
    Task<ProjectType?> FindByName(string name, long? companyId, CT ct);
    Task<ProjectType?> FindByCode(string code, long? companyId, CT ct);
    Task<ProjectType?> FindForDelete(long id, CT ct);
    Task<bool> FindProjectTypeByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct);
    Task<string> CodeCreator(long? companyId, CT ct);

    Task<List<ProjectType>> GetsProjectTypeByIds(List<long> ids, CT ct);
    Task<(List<ProjectType> Data, int RowCount)> GetsProjectType(List<long>? ids, string? filterData, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<ProjectType> Data, int RowCount)> GetActiveProjectTypes(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct);

}