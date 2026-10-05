using CityModel = Engineering.Application.WebServices.MetaDataServices.Cities.Models.City;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetsCityById;

public record GetsCityByIdQuery(
    int PageIndex,
    int PageSize,
    List<long>? Ids,
    string? FilterData,
    bool IgnoreQuery
    ) : IQuery<DataResult<List<CityModel>>>;
