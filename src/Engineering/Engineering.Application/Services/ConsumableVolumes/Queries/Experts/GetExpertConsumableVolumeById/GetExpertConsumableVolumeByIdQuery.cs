using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertConsumableVolumeById;

public record GetConsumableVolumeExpertByIdQuery(
    long Id
    ) : IQuery<ConsumableVolumeExpert>;