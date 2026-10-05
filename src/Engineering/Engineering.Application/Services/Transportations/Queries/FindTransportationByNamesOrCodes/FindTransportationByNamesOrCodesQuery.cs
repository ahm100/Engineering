
namespace Engineering.Application.Services.Transportations.Queries.FindTransportationByNamesOrCodes;

public record FindTransportationByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
