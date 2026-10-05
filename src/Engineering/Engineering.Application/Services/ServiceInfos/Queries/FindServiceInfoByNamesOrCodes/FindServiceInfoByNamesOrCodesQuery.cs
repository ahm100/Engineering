
namespace Engineering.Application.Services.ServiceInfos.Queries.FindServiceInfoByNamesOrCodes;

public record FindServiceInfoByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
