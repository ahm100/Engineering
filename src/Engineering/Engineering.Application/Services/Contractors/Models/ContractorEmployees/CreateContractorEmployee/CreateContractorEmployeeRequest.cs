namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.CreateContractorEmployee;

public record CreateContractorEmployeeRequest(long ContractorId,
                                              List<CreateContractorEmployeeRequestModel> Employees) : IHttpRequest;
