using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Persistence.Repositories.ContractorEmployees;

public class ContractorEmployeeRepository : BaseRepository<EngineeringDBContext, ContractorEmployee>, IContractorEmployeeRepository
{
    public ContractorEmployeeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<bool> ExistAsync(long thirdPartySkillId, long contractorId, CT ct)
    {
        return await DbSet.AnyAsync(c => c.EmployeeId.Equals(thirdPartySkillId) &&
                                         c.ContractorId.Equals(contractorId) &&
                                         !c.IsDeleted, ct);
    }

    public async Task<ContractorEmployee?> GetActiveContractorEmployeesByEmployeeId(long employeeId, CT ct)
    {
        return await DbSet.Where(oo => oo.IsActive && oo.EmployeeId.Equals(employeeId)).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<ContractorEmployee> Data, int RowCount)> GetContractorEmployeesByContractorId(List<long> contractorIds, List<long>? employeeId, CT ct)
    {
        var query = DbSet.Where(c =>
        contractorIds.Contains(c.ContractorId) &&
        !c.IsDeleted &&
        (employeeId == null || employeeId.Contains(c.EmployeeId)))
            .OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ContractorEmployee> Data, int RowCount)> GetsContractorEmployeeBySkill(
        long contractorId, long? companyId, CT ct)
    {
        var query = DbSet.Where(c => (companyId == null || c.CompanyId == companyId) &&
                                      c.ContractorId == contractorId && !c.IsDeleted)
                                     .OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ContractorEmployee>> GetThirdPartiesByContractorId(
        List<long> contractorIds, CT ct)
    {
        return await DbSet
            .Where(c => contractorIds.Contains(c.ContractorId) && !c.IsDeleted).ToListAsync(ct);
    }
}