using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineriesGroupByIds;

public record GetsMachineriesGroupByIdsQuery(
    List<long> Items
    ) : IQuery<List<MachineriesGroup>>;
