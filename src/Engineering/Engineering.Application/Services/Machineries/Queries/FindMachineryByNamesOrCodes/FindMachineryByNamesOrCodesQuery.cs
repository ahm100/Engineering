
namespace Engineering.Application.Services.Machineries.Queries.FindMachineryByNamesOrCodes;

public record FindMachineryByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
