using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByName;

public record GetCabinTypeByNameQuery(
    string CabinTypeName,
    long? CompanyId)
    : IQuery<GetCabinTypeByNameResponse?>;