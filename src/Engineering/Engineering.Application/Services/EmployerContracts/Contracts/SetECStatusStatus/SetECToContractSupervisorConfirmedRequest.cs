namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractSupervisorConfirmedRequest(
    long Id,
    string? Description
) : IHttpRequest;
