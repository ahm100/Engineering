
namespace Engineering.Application.Services.ContractorMachineries.Models.StateChangerContractorMachineries;

public record InactivateContractorMachineriesRequest(
    List<long> Ids
    ) : IHttpRequest;
