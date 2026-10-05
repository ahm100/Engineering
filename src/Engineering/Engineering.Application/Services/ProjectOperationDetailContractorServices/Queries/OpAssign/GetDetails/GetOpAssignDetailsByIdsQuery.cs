using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetDetails;

public record GetOpAssignDetailsByIdsQuery(
    long ProjectOperationId,
    List<long> ProjectOperationDetailIds)
    : IQuery<List<ProjectOperationDetail>>;