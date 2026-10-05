using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationIds;

public record GetExpertsByProjectOperationIdsQuery(
    List<long> ProjectOperationIds
    ) : IQuery<DataResult<List<ConsumableVolumeExpert>>>;
