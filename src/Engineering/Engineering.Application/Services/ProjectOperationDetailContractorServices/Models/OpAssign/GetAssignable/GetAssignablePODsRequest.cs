namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;

public record GetAssignablePODsRequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize) : IHttpRequest;