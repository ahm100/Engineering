
namespace Engineering.Application.Services.MachineTypes.Models.StateChangerMachineTypes;

public record StateChangerMachineTypesRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
