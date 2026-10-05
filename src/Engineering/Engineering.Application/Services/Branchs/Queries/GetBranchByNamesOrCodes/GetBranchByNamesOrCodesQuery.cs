namespace Engineering.Application.Services.Branchs.Queries.GetBranchByNamesOrCodes;

public record GetBranchByNamesOrCodesQuery(
    List<string> Names,
    long CategoryId,
    List<string> Codes,
    long? CompanyId)
    : IQuery<bool>;