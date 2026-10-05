using Engineering.Domain.Entities.Synonyms.MetaData.Organizations;

namespace Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;

public interface IViewOrganizationRepository
{
    Task<ViewOrganization?> GetById(
        long id, CT ct);

    Task<List<ViewOrganization>?> GetByIds(
        List<long> ids, CT ct);

    Task<List<long>?> GetByManagerId(
        long managerId, CT ct);
}
