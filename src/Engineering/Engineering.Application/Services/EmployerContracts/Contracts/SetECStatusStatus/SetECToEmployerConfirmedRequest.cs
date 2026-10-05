namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToEmployerConfirmedRequest(
    long Id,
    string? Description
) : IHttpRequest;
