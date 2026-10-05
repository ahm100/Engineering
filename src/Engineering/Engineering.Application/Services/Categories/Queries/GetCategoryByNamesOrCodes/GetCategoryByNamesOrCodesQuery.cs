namespace Engineering.Application.Services.Categories.Queries.GetCategoryByNamesOrCodes;

public record GetCategoryByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId)
    : IQuery<bool>;