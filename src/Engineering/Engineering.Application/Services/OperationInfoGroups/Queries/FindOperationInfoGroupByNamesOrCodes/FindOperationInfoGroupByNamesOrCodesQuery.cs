
namespace Engineering.Application.Services.OperationInfoGroups.Queries.FindOperationInfoGroupByNamesOrCodes;

public record FindOperationInfoGroupByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
