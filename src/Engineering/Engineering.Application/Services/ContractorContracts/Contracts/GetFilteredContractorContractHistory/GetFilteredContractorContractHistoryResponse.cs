namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHistory;

public record GetContractorContractHistoryResponse(
    List<GetContractorContractHistoryModel> Data,
    int RowCount
    );

