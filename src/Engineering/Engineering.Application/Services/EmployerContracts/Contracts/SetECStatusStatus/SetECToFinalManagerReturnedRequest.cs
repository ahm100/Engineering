namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToFinalManagerReturnedRequest(
    long Id,
    string? Description
) : IHttpRequest;
