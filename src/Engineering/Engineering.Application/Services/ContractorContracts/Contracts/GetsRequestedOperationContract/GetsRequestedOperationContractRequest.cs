
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedOperationContract;

public record GetsRequestedOperationContractRequest(
    List<long> ProjectOperationDetailServiceIds
    ) : IHttpRequest;
