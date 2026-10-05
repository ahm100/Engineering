namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractExpertPendingRequest(
    long Id,
    string? Description
) : IHttpRequest;
