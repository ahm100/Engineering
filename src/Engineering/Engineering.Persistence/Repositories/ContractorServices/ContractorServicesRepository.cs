using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Persistence.Repositories;

public class ContractorServicesRepository : BaseRepository<EngineeringDBContext, ContractorService>, IContractorServicesRepository
{
    public ContractorServicesRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<bool> ExistAsync(long serviceInfoId, long contractorId, CT ct)
    {
        return await DbSet.AnyAsync(c => c.ServiceInfoId.Equals(serviceInfoId) &&
                                         c.ContractorId.Equals(contractorId) &&
                                         !c.IsDeleted, ct);
    }

    public async Task<(List<ContractorService> Data, int RowCount)> GetContractorServicesByContractorId(long contractorId, string? filterData, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ServiceInfo)

            .Where(c => c.ContractorId.Equals(contractorId) &&
                (companyId == null || c.CompanyId == companyId) &&
                !c.IsDeleted &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);

    }

    public async Task<List<long>> GetContractorServicesByServiceIds(List<long> serviceInfoIds, CT ct)
    {
        var query = DbSet.GroupBy(x => x.ContractorId)
            .Where(contractorService =>
                serviceInfoIds.All(serviceInfoId =>
                    contractorService.Any(c => c.ServiceInfoId == serviceInfoId))).Select(contractor => contractor.Key);

        return await query.ToListAsync(ct);
    }
}