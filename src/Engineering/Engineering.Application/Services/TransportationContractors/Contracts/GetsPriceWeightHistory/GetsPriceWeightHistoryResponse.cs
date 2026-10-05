namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;

public record GetsPriceWeightHistoryResponse(
    List<GetsPriceWeightHistoryResponseModel> Data,
    int RowCount
    );
