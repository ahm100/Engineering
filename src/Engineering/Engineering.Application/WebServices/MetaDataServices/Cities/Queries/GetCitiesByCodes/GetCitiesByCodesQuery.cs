using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCitiesByCodes;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCitiesByCodes;

public record GetCitiesByCodesQuery(List<string> Codes) : IQuery<DataResult<List<GetsCityByCodesModel>>>;
