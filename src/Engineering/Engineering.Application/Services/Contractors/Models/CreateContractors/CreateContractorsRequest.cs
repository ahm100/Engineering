using Engineering.Application.Services.Contractors.Models.ContractorEmployees.CreateContractorEmployee;
using Engineering.Application.Services.Contractors.Models.ContractorServices.CreateContractorsService;

namespace Engineering.Application.ContractorServices.Models.CreateContractors;

public record CreateContractorsRequest : IHttpRequest
{
    public long ContractorId { get; set; }
    public List<CreateContractorServicesRequestModel>? ServiceInfoIds { get; set; }
    public List<CreateContractorEmployeeRequestModel>? Employees { get; set; }
}
