namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractSupervisorPendingRequest(
    long Id,
    string? Description
) : IHttpRequest;
