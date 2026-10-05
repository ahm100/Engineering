using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetByPO;

public record GetOpAssignByPOQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<OpAssignModel>>>;