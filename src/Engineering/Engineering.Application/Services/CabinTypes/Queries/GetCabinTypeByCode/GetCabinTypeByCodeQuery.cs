using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCode;

public record GetCabinTypeByCodeQuery(
    int CabinTypeCode,
    long? CompanyId)
    : IQuery<GetCabinTypeByCodeResponse?>;