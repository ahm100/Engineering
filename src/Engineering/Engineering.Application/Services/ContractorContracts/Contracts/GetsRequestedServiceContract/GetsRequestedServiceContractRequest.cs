
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;

public record GetsRequestedServiceContractRequest(
    long ProjectId,
    List<long> ServiceIds,
    long ContractorId
    ) : IHttpRequest;
