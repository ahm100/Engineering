namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractsByContractor;

public record GetFilteredContractorContractsByContractorResponse(
    List<GetFilteredContractorContractsByContractorModel> Data,
    int RowCount
    );
