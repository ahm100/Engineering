using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelEnum;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelExporter;

public record GetsContractorDetailReportsExcelExporterRequest(
    List<long>? Ids,
    long? ContractorContractId,
    long? ContractorId,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    List<ContractorDetailReportsExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
