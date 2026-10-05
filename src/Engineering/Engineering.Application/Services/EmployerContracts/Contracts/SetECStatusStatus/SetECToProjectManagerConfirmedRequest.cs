namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToProjectManagerConfirmedRequest(
    long Id,
    string? Description
) : IHttpRequest;
