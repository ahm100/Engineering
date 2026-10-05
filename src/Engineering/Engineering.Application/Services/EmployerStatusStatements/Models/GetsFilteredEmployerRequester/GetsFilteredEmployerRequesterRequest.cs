namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerRequester;

public record GetsFilteredEmployerRequesterRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
