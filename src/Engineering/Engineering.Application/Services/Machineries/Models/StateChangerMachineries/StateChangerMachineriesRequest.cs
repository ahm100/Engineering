
namespace Engineering.Application.Services.Machineries.Models.StateChangerMachineries;

public record StateChangerMachineriesRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
