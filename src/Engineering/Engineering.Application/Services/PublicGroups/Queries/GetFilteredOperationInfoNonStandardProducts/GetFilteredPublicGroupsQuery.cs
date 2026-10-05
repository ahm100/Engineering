using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.PublicGroups.Queries.GetFilteredPublicGroups;

public record GetFilteredPublicGroupsQuery(
    List<long>? ProductGroupIds
    ) : IQuery<DataResult<List<PublicGroup>>>;
