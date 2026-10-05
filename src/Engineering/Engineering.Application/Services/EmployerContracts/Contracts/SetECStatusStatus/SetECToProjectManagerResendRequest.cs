namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToProjectManagerResendRequest(
    long Id,
    string? Description
) : IHttpRequest;
