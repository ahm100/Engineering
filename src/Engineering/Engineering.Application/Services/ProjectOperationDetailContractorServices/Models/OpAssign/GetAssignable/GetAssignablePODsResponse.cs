namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;

public record GetAssignablePODsResponse(
    List<AssignablePODModel> Data,
    int RowCount);