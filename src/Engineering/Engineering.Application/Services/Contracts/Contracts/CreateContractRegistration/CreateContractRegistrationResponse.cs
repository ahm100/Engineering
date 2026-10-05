using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractRegistration;

public record CreateContractRegistrationResponse(
    long Id,
    long ContractNumber,
    ContractStatus Status,
    DateTime EndDate,
    decimal InitialAmount,
    decimal FinalContractAmount,
    bool IsRegistrationPending,
    bool IsDone);
