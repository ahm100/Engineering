using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetsByProjectOperationDetailId;

public record GetExpertsByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId
    ) : IQuery<DataResult<List<ConsumableVolumeExpert>>>;