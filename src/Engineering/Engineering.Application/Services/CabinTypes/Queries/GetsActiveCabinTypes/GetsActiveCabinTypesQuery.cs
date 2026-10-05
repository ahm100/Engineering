using Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsActiveCabinTypes;

public record GetsActiveCabinTypesQuery(
    string? FilterData,
    int? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsActiveCabinTypeModel>>>;