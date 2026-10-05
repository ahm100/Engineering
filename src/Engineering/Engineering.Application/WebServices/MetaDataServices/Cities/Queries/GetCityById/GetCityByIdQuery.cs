using CityModel = Engineering.Application.WebServices.MetaDataServices.Cities.Models.City;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCityById;

public record GetCityByIdQuery(
    long Id
    ) : IQuery<CityModel?>;
