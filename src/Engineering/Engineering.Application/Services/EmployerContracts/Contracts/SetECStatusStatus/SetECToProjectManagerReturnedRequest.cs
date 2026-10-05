namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToProjectManagerReturnedRequest(
    long Id,
    string? Description
) : IHttpRequest;
