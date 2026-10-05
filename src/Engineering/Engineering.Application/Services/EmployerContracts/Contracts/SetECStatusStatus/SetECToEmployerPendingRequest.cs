namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToEmployerPendingRequest(
    long Id,
    string? Description
) : IHttpRequest;
