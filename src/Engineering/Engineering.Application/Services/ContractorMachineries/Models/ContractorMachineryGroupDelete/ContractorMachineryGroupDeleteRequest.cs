
namespace Engineering.Application.Services.ContractorMachineries.Models.ContractorMachineryGroupDelete;

public record ContractorMachineryGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
