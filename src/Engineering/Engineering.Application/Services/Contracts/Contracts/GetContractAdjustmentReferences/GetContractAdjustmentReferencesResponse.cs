namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;

public record GetContractAdjustmentReferencesResponse(
    List<GetContractAdjustmentReferencesModel> Data,
    int RowCount);
