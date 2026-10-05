using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerDocRepository : BaseRepository<EngineeringDBContext, EmployerDoc>, IEmployerDocRepository
{
    public EmployerDocRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<EmployerDoc?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.EmployerDocUrls)
            .Where(oo => oo.IsDeleted != true && oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<EmployerDoc> Data, int RowCount)> GetActiveEmployerDocsByEmployerContractId(
        long employerContractId, EDocumentType? documentTypes, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.EmployerDocUrls)
            .Where(oo => oo.IsActive && oo.IsDeleted != true && oo.EmployerContract.Id == employerContractId);

        if (documentTypes != null)
            query.Where(oo => oo.Type == documentTypes);

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var considerations = await query.ToListAsync(ct);

        return (considerations, count);
    }

    public async Task<(List<EmployerDoc> Data, int RowCount)> GetEmployerDocsByEmployerContractId(
        long employerContractId, EDocumentType? documentTypes, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.EmployerDocUrls)
            .Where(oo => oo.EmployerContract.Id == employerContractId);

        if (documentTypes != null)
            query.Where(oo => oo.Type == documentTypes);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var considerations = await query.ToListAsync(ct);

        return (considerations, count);
    }

}