using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelEnum;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelExporter;

public record GetsContractorReportsExcelExporterRequest(
    List<long>? Ids,
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ContractorContractIds,
    DateTime? FromDate,
    DateTime? ToDate,
    List<ContractorReportsExcelEnum>? ExcelFilters,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
