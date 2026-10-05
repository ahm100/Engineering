namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;

public record GetEOProductHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
) : IHttpRequest;