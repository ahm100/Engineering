
namespace Engineering.Application.Services.Machineries.Models.StateChangerMachineries;

public record InactivateMachineriesRequest(
    List<long> Ids
    ) : IHttpRequest;
