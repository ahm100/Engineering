using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECStatusRequest(
    long Id,
    EContractStatus Status,
    string? Description
) : IHttpRequest;
