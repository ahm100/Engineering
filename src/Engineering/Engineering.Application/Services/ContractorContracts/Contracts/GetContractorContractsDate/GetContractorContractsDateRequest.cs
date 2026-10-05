
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;

public record GetContractorContractsDateRequest(
    long ProjectId,
    long ContractorId
    ) : IHttpRequest;
