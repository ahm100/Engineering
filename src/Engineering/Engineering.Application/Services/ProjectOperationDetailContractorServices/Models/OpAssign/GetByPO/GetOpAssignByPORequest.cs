namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;

public record GetOpAssignByPORequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize) : IHttpRequest;