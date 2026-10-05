using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetsActiveCostCenterTypes;

public record GetsActiveCostCenterTypesQuery(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsActiveCostCenterTypesModel>>>;