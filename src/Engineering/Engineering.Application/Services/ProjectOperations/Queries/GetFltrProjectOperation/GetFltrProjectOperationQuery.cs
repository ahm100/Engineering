using Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetFltrProjectOperation;

public record GetFltrProjectOperationQuery(
    long CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? MeasureUnitIds,
    decimal? MinPrice,
    decimal? MaxPrice,
    string? FilterData,
    int PageIndex,
    int PageSize
) : IQuery<List<GetFltrProjectOperationModel>>;