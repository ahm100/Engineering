using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Queries.GetsMachineryGroupsForRequestMachinery;

public record GetsMachineryGroupsForRequestMachineryQuery(
    long? ProjectId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineriesGroup>>>;