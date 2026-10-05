namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;

public record GetECThirdPartiesRequest(
    long ProjectId,
    List<long>? EmployerIds,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;