using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithProductVolumes;

public record GetProjectOperationDetailWithProductVolumesQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;