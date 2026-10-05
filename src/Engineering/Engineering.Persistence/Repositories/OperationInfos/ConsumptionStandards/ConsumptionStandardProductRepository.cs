using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using Engineering.Domain.Entities.OperationInfos.Enums;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;

namespace Engineering.Persistence.Repositories.OperationInfos.ConsumptionStandards;

public class ConsumptionStandardProductRepository : BaseRepository<EngineeringDBContext, ConsumptionStandardProduct>, IConsumptionStandardProductRepository
{
    public ConsumptionStandardProductRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ConsumptionStandardProduct?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<ConsumptionStandardProduct> Data, int RowCount)> ProductGetByOprationInfoId(
        long oprationInfoId,
        StandardProductType? standardProductType,
        ProductAllowedType? productAllowedType,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(o => o.OperationInfo)
            .Where(oo => oo.OperationInfo.Id == oprationInfoId &&
                (standardProductType == null || oo.StandardProductType.Equals(standardProductType)) &&
                (productAllowedType == null || oo.ProductAllowedType.Equals(productAllowedType)));

        var count = await query.CountAsync(ct);
        var products = await query
            .Page(pageIndex, pageSize)
            .ToListAsync(ct);

        return (products, count);
    }

    public async Task<(List<ConsumptionStandardProduct> Data, int RowCount)> NonStandardProductGetByOprationInfoId(
        long oprationInfoId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Where(oo => oo.OperationInfo.Id == oprationInfoId);

        query = query.OrderByDescending(x => x.StandardProductType == StandardProductType.Category);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var products = await query.ToListAsync(ct);

        return (products, count);
    }

    public async Task<(List<ConsumptionStandardProduct> Data, int RowCount)> ProductByOprationInfoId(long oprationInfoId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfo.Id == oprationInfoId);

        var count = await query.CountAsync(ct);
        var experts = await query
            .ToListAsync(ct);

        return (experts, count);
    }

    public async Task<List<ConsumptionStandardProduct>> GetProductByOprationInfoId(
        long oprationInfoId,
        CT ct)
    {
        return await DbSet
            .Include(oo => oo.OperationInfo)
            .Where(oo => oo.OperationInfo.Id == oprationInfoId).ToListAsync(ct);
    }
}