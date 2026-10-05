namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorProjectOperationDetailReports;

public record GetsContractorProjectOperationDetailReportsResponse(
    List<GetsContractorProjectOperationDetailReportsModel> Data,
    int RowCount
    );
