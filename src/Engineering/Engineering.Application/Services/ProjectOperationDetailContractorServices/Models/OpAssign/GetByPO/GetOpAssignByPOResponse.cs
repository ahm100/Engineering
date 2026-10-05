namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;

public record GetOpAssignByPOResponse(
    List<OpAssignModel> Data,
    int RowCount);