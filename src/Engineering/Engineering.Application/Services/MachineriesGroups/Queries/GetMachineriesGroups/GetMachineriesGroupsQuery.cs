
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetMachineriesGroups;

public record GetMachineriesGroupsQuery(
    List<long>? Ids,
    string? FilterData,
    string? code,
    string? name,
    bool? isActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineriesGroup>>>;