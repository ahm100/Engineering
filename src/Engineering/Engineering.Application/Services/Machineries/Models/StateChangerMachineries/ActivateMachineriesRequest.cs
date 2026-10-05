
namespace Engineering.Application.Services.Machineries.Models.StateChangerMachineries;

public record ActivateMachineriesRequest(
    List<long> Ids
    ) : IHttpRequest;
