namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;

public record GetCCHByIdRequest(
    long Id
    ) : IHttpRequest;