namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Delete;

public record DeleteOpAssignRequest(
    long Id
) : IHttpRequest;