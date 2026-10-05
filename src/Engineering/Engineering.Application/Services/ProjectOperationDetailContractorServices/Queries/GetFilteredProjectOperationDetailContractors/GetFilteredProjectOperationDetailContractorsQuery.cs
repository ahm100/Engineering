namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetFilteredProjectOperationDetailContractors;

public record GetFilteredProjectOperationDetailContractorsQuery(
    List<long> CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<long?>>>;