namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorProjectOperationDetailReports;

public record GetsContractorProjectOperationDetailReportsRequest(
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ContractorContractIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
