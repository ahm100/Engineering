using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithMachineryVolumes;

public record GetProjectOperationDetailWithMachineryVolumesQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;
