
namespace Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContract;

public record DeleteContractorContractRequest(
    long Id
    ) : IHttpRequest;
