using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractGuaranteeStatus;

public record ChangeContractGuaranteeStatusRequest(
    long ContractId,
    long Id,
    ContractGuaranteeStatus Status) : IHttpRequest;
