namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;

public record GetFltrProjectProductsRequest(
    List<long>? ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
