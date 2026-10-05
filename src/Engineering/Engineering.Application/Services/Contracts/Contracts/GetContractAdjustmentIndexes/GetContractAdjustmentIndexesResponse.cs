namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;

public record GetContractAdjustmentIndexesResponse(
    List<GetContractAdjustmentIndexesModel> Data,
    int RowCount);
