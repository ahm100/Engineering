
namespace Engineering.Application.Services.Seasons.Queries.FindSeasonByNamesOrCodes;

public record FindSeasonByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long BranchId,
    long? CompanyId
    ) : IQuery<bool>;
