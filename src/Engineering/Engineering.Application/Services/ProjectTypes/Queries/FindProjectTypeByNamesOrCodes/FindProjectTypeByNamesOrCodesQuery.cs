
namespace Engineering.Application.Services.ProjectTypes.Queries.FindProjectTypeByNamesOrCodes;

public record FindProjectTypeByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
