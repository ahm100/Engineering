namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractExpertReturnedRequest(
    long Id,
    string? Description
) : IHttpRequest;
