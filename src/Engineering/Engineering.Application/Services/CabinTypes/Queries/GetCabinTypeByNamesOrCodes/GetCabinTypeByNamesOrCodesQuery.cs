namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByNamesOrCodes;

public record GetCabinTypeByNamesOrCodesQuery(
    List<string> Names,
    List<int> Codes,
    long? CompanyId)
    : IQuery<bool>;