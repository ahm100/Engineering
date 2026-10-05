
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetActiveMachineriesGroups;

public record GetActiveMachineriesGroupsQuery(
    string? FilterData,
    string? code,
    string? name,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineriesGroup>>>;