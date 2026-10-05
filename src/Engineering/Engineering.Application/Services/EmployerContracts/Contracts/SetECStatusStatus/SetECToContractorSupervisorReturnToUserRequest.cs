namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractorSupervisorReturnToUserRequest(
    long Id,
    string? Description
) : IHttpRequest;
