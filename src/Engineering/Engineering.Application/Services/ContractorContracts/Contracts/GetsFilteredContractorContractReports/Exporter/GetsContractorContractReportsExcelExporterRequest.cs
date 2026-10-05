using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Enum;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Exporter;

public record GetsContractorContractReportsExcelExporterRequest(
    List<long>? Ids,
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ContractorContractIds,
    DateTime? FromDate,
    DateTime? ToDate,
    List<ContractorContractReportsExcelEnum>? ExcelFilters,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
