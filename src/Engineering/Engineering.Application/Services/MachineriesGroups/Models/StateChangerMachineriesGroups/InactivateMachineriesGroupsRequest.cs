
namespace Engineering.Application.Services.MachineriesGroups.Models.StateChangerMachineriesGroups;

public record InactivateMachineriesGroupsRequest(
    List<long> Ids
    ) : IHttpRequest;
