using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsByProjectOperationIdForVolumes;

public record GetsByProjectOperationIdForVolumesQuery(
    long ProjectOperationId
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;