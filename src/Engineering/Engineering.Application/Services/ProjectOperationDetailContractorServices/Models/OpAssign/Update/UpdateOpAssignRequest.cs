namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Update;

public record UpdateOpAssignRequest(
    long Id,
    long ContractorId,
    decimal Volume
) : IHttpRequest;