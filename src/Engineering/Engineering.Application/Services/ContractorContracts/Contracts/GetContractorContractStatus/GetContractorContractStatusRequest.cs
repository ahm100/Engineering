using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractRequestStatus;

public record GetContractorContractStatusRequest(
    List<ContractorContractStatus>? RemoveStatuses
    ) : IHttpRequest;
