
namespace Engineering.Application.Services.ContractorMachineries.Models.StateChangerContractorMachineries;

public record StateChangerContractorMachineriesRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
