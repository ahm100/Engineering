namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetContractorEmployeesByContractorId;

public record GetContractorEmployeesByContractorIdRequest(
    long Id,
    List<long>? EmployeeId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
