namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractSupervisorReturnedRequest(
    long Id,
    string? Description
) : IHttpRequest;
