using Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetProvinceById;

namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetProvinceById;

public record GetProvinceByIdQuery(
    long Id
    ) : IQuery<GetProvinceByIdResponse?>;