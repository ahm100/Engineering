using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.GetProjectOperationDetailVolumes;

public record GetProjectOperationDetailVolumesQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;