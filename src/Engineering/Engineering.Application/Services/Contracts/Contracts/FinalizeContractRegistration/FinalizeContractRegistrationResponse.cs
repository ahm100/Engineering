using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.FinalizeContractRegistration;

public record FinalizeContractRegistrationResponse(
    ContractStatus Status,
    bool IsDone);
