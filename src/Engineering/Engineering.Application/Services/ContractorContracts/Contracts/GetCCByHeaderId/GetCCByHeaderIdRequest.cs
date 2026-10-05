using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;

public record GetCCByHeaderIdRequest(
    long HeaderId,
    ContractorContractType Type
    ) : IHttpRequest;