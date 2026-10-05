
namespace Engineering.Application.Services.MachineriesGroups.Models.StateChangerMachineriesGroups;

public record StateChangerMachineriesGroupsRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
