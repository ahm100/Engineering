using Engineering.Application.Services.ProjectOperationDetailContractorServices
    .Models.OpAssign.GetAssignable;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices
    .Queries.OpAssign.GetAssignable;

public record GetAssignablePODsQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<AssignablePODModel>>>;