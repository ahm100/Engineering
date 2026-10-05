namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationContractors;

public record GetDailyProjectOperationContractorsQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds
    ) : IQuery<DataResult<List<long>>>;
