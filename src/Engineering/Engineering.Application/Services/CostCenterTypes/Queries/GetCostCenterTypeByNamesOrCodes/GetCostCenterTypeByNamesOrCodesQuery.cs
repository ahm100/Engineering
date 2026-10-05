namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByNamesOrCodes;

public record GetCostCenterTypeByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId)
    : IQuery<bool>;