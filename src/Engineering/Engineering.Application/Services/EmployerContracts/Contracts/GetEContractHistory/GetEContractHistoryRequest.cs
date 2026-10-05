namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;

public record GetEContractHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
) : IHttpRequest;