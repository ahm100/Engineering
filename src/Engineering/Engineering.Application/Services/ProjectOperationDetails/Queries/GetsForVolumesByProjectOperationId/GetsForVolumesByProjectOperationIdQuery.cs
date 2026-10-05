using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsForVolumesByProjectOperationId;

public record GetsForVolumesByProjectOperationIdQuery(
    long? Id
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;