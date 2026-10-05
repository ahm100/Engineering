using Engineering.Application.Services.Categories.Models.GetsByFilterData;

namespace Engineering.Application.Services.Categories.Queries.GetsByFilterData;

public record GetsByFilterDataQuery(
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsByFilterDataResponseModel>>>;