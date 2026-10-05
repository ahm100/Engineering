
namespace Engineering.Application.Services.Trips.Queries.FindTripByNamesOrCodes;

public record FindTripByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
