namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;

public record GetEContractOperationHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
) : IHttpRequest;