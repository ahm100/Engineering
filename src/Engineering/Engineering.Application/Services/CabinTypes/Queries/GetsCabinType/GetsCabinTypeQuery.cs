using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsCabinType;

public record GetsCabinTypeQuery(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsCabinTypeResponseModel>>>;