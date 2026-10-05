using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailDoneVolume;

public record GetProjectOperationDetailDoneVolumeQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;
