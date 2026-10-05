
namespace Engineering.Application.Services.MachineTypes.Models.StateChangerMachineTypes;

public record InactivateMachineTypesRequest(
    List<long> Ids
    ) : IHttpRequest;
