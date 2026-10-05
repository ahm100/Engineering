using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByCodes;

public record GetsMachineriesGroupByCodesQuery(
    List<string> Codes,
    long? CompanyId
    ) : IQuery<DataResult<List<MachineriesGroup>>>;