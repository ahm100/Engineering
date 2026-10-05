namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToPrimaryManagerConfirmedRequest(
    long Id,
    string? Description
) : IHttpRequest;
