using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;
using Engineering.Domain.Entities.GoodsManager;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public class GoodsManagerAssignmentRepository :
    BaseRepository<EngineeringDBContext, GoodsManagerAssignment>,
    IGoodsManagerAssignmentRepository
{
    public GoodsManagerAssignmentRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GoodsManagerAssignment?> GetById(
        long id,
        CT ct)
    {
        return await DbSet
            .Include(x => x.Histories)
            .FirstOrDefaultAsync(
                x => x.Id == id && !x.IsDeleted,
                ct);
    }

    public async Task<List<GetFilteredGoodsManagerAssignmentModel>?> GetManagerById(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(x => x.OrganizationId == id && !x.IsDeleted)
            .Select(item => new GetFilteredGoodsManagerAssignmentModel()
            {
                OrganizationId = item.OrganizationId,
                ProductId = item.ProductId,
            });

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<bool> FindHaveCategory(
        long productId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Histories)
            .Where(x => x.ProductId == productId && !x.IsDeleted);

        return await query.AnyAsync(ct);
    }

    public async Task<List<GetFilteredGoodsManagerAssignmentModel>> GetFiltered(
        List<long>? ids,
        long? organizationId,
        long? productId,
        CT ct)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .Where(x =>
                (ids == null || ids.Contains(x.Id)) &&
                (productId == null ||
                    x.ProductId == productId) &&
                (organizationId == null ||
                    x.OrganizationId == organizationId))
            .Select(item => new GetFilteredGoodsManagerAssignmentModel()
            {
                OrganizationId = item.OrganizationId,
                ProductId = item.ProductId,
            });

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<bool> ExistsDuplicate(
        long organizationId,
        long productId,
        long? excludeId,
        CT ct)
    {
        var query = DbSet
            .Where(x => !x.IsDeleted)
            .Where(x =>
                x.OrganizationId == organizationId &&
                x.ProductId == productId &&
                (excludeId == null || x.Id != excludeId));

        return await query.AnyAsync(ct);
    }

    public async Task<List<GoodsManagerAssignment>> GetActiveByProductId(
        long productId,
        CT ct)
    {
        return await DbSet
            .AsNoTracking()
            .Where(x =>
                !x.IsDeleted &&
                x.ProductId == productId)
            .ToListAsync(ct);
    }

    public async Task<List<GoodsManagerAssignment>> GetByOrganizationId(
        long organizationId,
        CT ct)
    {
        return await DbSet
            .Where(x => !x.IsDeleted && x.OrganizationId == organizationId)
            .ToListAsync(ct);
    }

    public async Task<List<long>?> GetAllManagerGoods(
        List<long> organizationIds,
        CT ct)
    {
        return await DbSet
            .AsNoTracking()
            .Where(x =>
                !x.IsDeleted &&
                organizationIds.Contains(x.OrganizationId)
            ).Select(oo => oo.ProductId).ToListAsync(ct);
    }
}