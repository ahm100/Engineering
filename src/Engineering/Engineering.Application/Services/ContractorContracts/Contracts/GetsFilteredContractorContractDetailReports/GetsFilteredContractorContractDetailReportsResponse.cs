namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;

public record GetsFilteredContractorContractDetailReportsResponse(
    List<GetsFilteredContractorContractDetailReportsModel> Data,
    int RowCount
    );
