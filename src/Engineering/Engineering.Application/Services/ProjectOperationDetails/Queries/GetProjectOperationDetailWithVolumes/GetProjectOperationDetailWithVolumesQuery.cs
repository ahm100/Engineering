using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithVolumes;

public record GetProjectOperationDetailWithVolumesQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;