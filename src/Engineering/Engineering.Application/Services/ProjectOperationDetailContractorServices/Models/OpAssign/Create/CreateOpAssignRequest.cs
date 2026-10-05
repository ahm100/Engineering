namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Create;

public record CreateOpAssignRequest(
    long ProjectOperationId,
    long ContractorId,
    List<CreateOpAssignItem> Items
) : IHttpRequest;