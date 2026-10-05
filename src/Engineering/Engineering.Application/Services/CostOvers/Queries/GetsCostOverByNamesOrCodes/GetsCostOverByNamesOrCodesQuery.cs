namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNamesOrCodes;

public record GetsCostOverByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId)
    : IQuery<bool>;