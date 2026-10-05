namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToEmployerReturnedRequest(
    long Id,
    string? Description
) : IHttpRequest;
