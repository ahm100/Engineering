
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;

public record GetContractorContractHeaderByIdRequest(
    long Id
    ) : IHttpRequest;
