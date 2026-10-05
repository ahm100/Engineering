using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsContractorProjectOperationDetailDetailReports;

public record GetsContractorProjectOperationDetailDetailReportsQuery(
    List<long>? Ids,
    long? ContractorContractId,
    long? ContractorId,
    DateTime? FromDate,
    DateTime? ToDate,
    long? CompanyId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;