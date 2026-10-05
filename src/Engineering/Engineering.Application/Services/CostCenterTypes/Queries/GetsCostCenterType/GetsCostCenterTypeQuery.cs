using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetsCostCenterType;

public record GetsCostCenterTypeQuery(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsCostCenterTypeModel>>>;