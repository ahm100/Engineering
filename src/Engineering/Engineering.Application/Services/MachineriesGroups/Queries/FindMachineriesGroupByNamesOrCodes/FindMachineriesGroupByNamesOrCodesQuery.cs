
namespace Engineering.Application.Services.MachineriesGroups.Queries.FindMachineriesGroupByNamesOrCodes;

public record FindMachineriesGroupByNamesOrCodesQuery(
    List<string> Names,
    List<string> Codes,
    long? CompanyId
    ) : IQuery<bool>;
