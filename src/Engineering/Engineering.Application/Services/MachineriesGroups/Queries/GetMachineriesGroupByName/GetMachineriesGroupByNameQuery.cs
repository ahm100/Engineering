using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByName;

public record GetMachineriesGroupByNameQuery(
    string GroupName,
    long? CompanyId
    ) : IQuery<MachineriesGroup?>;