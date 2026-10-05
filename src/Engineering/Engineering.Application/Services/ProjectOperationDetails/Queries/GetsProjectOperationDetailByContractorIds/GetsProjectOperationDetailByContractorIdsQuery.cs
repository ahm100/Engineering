using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByContractorIds;

public record GetsProjectOperationDetailByContractorIdsQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    List<long>? ContractorIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;