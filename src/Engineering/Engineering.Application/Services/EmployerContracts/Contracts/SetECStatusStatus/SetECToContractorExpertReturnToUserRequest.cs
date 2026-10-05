namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public record SetECToContractorExpertReturnToUserRequest(
    long Id,
    string? Description
) : IHttpRequest;
