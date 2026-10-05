using Engineering.Application.Services.CostOvers.Models.GetsCostOverByNameOrCode;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNameOrCode;

public record GetsCostOverByNameOrCodeQuery(
    string FilterData,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsCostOverByNameOrCodeModel>>>;