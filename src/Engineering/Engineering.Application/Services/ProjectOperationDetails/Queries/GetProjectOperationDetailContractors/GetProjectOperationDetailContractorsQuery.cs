namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailContractors;

public record GetProjectOperationDetailContractorsQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds
    ) : IQuery<DataResult<List<long>>>;
