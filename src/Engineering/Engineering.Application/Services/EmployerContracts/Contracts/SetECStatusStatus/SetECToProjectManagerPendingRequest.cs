namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToProjectManagerPendingRequest(
    long Id,
    string? Description
) : IHttpRequest;
