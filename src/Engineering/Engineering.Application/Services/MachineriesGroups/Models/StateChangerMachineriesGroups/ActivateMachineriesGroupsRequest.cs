
namespace Engineering.Application.Services.MachineriesGroups.Models.StateChangerMachineriesGroups;

public record ActivateMachineriesGroupsRequest(
    List<long> Ids
    ) : IHttpRequest;
