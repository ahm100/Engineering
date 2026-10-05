namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;

public record GetsFilteredContractorContractDetailReportsRequest(
    long? ContractorContractId,
    long? ContractorId,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
