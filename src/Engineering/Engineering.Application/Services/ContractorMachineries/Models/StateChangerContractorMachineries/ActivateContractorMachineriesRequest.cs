
namespace Engineering.Application.Services.ContractorMachineries.Models.StateChangerContractorMachineries;

public record ActivateContractorMachineriesRequest(
    List<long> Ids
    ) : IHttpRequest;
