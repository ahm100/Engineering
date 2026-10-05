namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorProjectOperationDetailDetailReports;

public record GetsContractorProjectOperationDetailDetailReportsRequest(
    long? ContractorContractId,
    long? ContractorId,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
