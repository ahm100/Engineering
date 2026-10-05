using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupById;

public record GetMachineriesGroupByIdQuery(
    long Id
    ) : IQuery<MachineriesGroup?>;