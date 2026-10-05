
namespace Engineering.Application.Services.MachineTypes.Models.StateChangerMachineTypes;

public record ActivateMachineTypesRequest(
    List<long> Ids
    ) : IHttpRequest;
