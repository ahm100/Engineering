namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContracts;

public record GetFilteredContractorContractsResponse(
    List<GetFilteredContractorContractsModel> Data,
    int RowCount
    );
