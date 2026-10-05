using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsForVolumesByIds;

public record GetsForVolumesByIdsQuery(
    List<long>? Ids
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;