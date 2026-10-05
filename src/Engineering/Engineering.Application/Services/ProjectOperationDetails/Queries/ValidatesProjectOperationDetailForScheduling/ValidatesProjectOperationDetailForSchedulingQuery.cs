using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.ValidatesProjectOperationDetailForScheduling;

public record ValidatesProjectOperationDetailForSchedulingQuery(
    List<long> OperationInfoIds,
    List<long> OperationLocationIds
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;