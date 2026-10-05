namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToPrimaryManagerReturnedRequest(
    long Id,
    string? Description
) : IHttpRequest;
