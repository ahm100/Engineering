using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Enum;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Exporter;

public record GetsContractorContractDetailReportsExcelExporterRequest(
    List<long>? Ids,
    long? ContractorContractId,
    long? ContractorId,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    List<ContractorContractDetailReportsExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
