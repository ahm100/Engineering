namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorByContractorContractType;

public record GetsContractorByContractorContractTypeResponse(
    List<GetsContractorByContractorContractTypeModel> Data,
    int RowCount
    );
