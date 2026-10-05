namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToFinalManagerConfirmedRequest(
    long Id,
    string? Description
) : IHttpRequest;
