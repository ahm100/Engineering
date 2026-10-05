using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;

namespace Engineering.Application.Services.EmployerEmployees.Queries.GetECThirdParties;

public record GetECThirdPartiesQuery(
    long ProjectId,
    List<long>? EmployerIds,
    int PageIndex,
    int PageSize
    ) : IQuery<GetECThirdPartiesResponse?>;