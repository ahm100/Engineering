namespace Engineering.Application.Services.ProjectOperations.Models.GetsByEmployerContract;

public record GetsByEmployerContractRequest(
    long EmployerContractId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
