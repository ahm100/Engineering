namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;

public record GetEOServiceHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
) : IHttpRequest;