using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsContractorProjectOperationDetailReports;

public record GetsContractorProjectOperationDetailReportsQuery(
    List<long>? Ids,
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ContractorContractIds,
    DateTime? FromDate,
    DateTime? ToDate,
    long? CompanyId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;