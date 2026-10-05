using Engineering.Application.ContractorServices.Models.CreateContractors;
using Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetContractorEmployeesByContractorId;
using Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetsContractorEmployeeBySkill;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetFilteredContractors;

namespace Engineering.Application.ContractorServices;

public interface IContractorLogic
{
    Task<Result<CreateContractorsResponse?>> CreateContractors(
        CreateContractorsRequest request, CT ct);

    Task<Result<GetContractorServicesByContractorIdResponse?>> GetContractorServicesByContractorId(
        GetContractorServicesByContractorIdRequest request, CT ct);

    Task<Result<GetContractorsByServiceIdsResponse?>> GetContractorsByServiceIds(
        GetContractorsByServiceIdsRequest request, CT ct);

    Task<Result<GetContractorEmployeesByContractorIdResponse?>> GetContractorEmployeesByContractorId(
        GetContractorEmployeesByContractorIdRequest request, CT ct);

    Task<Result<GetsContractorEmployeeBySkillResponse?>> GetsContractorEmployeeBySkill(
        GetsContractorEmployeeBySkillRequest request, CT ct);

    Task<Result<GetFilteredContractorsResponse?>> GetFilteredContractors(
        GetFilteredContractorsRequest request, CT ct);
}
