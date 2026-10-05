using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.HaveMachineriesGroupChild;

public record HaveMachineriesGroupChildQuery(
    long Id
    ) : IQuery<MachineriesGroup>;