using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Abstractions.Data;

public interface IContractorEmployeeRepository : IBaseRepository<ContractorEmployee>
{
    Task<(List<ContractorEmployee> Data, int RowCount)> GetContractorEmployeesByContractorId(
        List<long> contractorIds, List<long>? employeeId, CT ct);

    Task<ContractorEmployee?> GetActiveContractorEmployeesByEmployeeId(
        long employeeId, CT ct);

    Task<bool> ExistAsync(
        long thirdPartySkillId, long contractorId, CT ct);

    Task<(List<ContractorEmployee> Data, int RowCount)> GetsContractorEmployeeBySkill(
        long contractorId, long? companyId, CT ct);

    Task<List<ContractorEmployee>> GetThirdPartiesByContractorId(
        List<long> contractorIds, CT ct);

}