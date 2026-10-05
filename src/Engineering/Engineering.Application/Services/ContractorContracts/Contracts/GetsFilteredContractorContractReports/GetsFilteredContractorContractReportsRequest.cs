namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractReports;

public record GetsFilteredContractorContractReportsRequest(
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ContractorContractIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
