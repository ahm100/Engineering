
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsWithContractProjectOperation;

public record GetsWithContractProjectOperationRequest(
    long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
