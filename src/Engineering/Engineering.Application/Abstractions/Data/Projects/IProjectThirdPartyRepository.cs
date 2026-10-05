using Engineering.Application.Services.Projects.Models.GetProjectThirdParties;
using Engineering.Domain.Entities.Projects.ProjectUsers;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectThirdPartyRepository : IBaseRepository<ProjectThirdParty>
{
    Task<ProjectThirdParty?> GetProjectThirdPartyById(
        long id,
        CT ct);

    Task<List<ProjectThirdParty>?> GetProjectThirdPartyByIds(
        List<long> ids,
        CT ct);

    Task<ProjectThirdParty?> GetByProjectAndThirdPartyId(
        long projectId,
        long thirdPartyId,
        CT ct);

    Task<bool> HasThirdParty(
        long projectId,
        CT ct);

    Task<(List<GetFltrProjectThirdPartyModel>? Data, int RowCount)> GetFltrProjectThirdParty(
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct);
}