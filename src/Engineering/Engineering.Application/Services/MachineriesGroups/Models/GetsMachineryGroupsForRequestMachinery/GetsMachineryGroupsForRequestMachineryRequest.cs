namespace Engineering.Application.Services.MachineriesGroups.Models.GetsMachineryGroupsForRequestMachinery;

public record GetsMachineryGroupsForRequestMachineryRequest(
    long? ProjectId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
