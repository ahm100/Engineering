using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroupByCode;

public record GetMachineriesGroupByCodeQuery(
    string GroupCode,
    long? CompanyId
    ) : IQuery<MachineriesGroup>;