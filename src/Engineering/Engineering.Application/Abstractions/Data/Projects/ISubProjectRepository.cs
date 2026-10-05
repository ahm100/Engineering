using Engineering.Application.Services.SubProjects.Models;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.Histories;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface ISubProjectRepository : IBaseRepository<SubProject>
{
    Task<SubProject?> GetSubProjectById(
        long id, CT ct);

    Task<SubProject?> GetSubProjectByCode(
        string code, CT ct);

    Task<SubProjectDetails?> GetSubProjectDetailsById(
        long id, CT ct);

    Task<SubProjectDetails?> GetSubProjectDetailsByCode(
        string code, CT ct);

    Task<(List<SubProjectDetails> Data, int RowCount)> GetSubProjects(
        long projectId,
        string? filterData,
        SubProjectStatus? status,
        SubProjectType? type,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<long> AllocateSequence(
        long projectId, CT ct);

    Task<bool> HasAccess(
        long projectId,
        long userId,
        List<long> thirdPartyIds,
        CT ct);

    Task<bool> HasDependencies(
        long id, CT ct);

    Task<bool> ProjectHasSubProjects(
        long projectId, CT ct);

    Task<List<long>> GetAllowedManagerIds(
        long projectId, CT ct);

    Task AddHistory(
        SubProjectHistory history, CT ct);
}
