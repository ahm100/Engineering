namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractExpertConfirmedRequest(
    long Id,
    string? Description
) : IHttpRequest;
