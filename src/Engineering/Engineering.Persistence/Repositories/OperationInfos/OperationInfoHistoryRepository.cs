using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;
using Engineering.Domain.Entities.OperationInfoHistorys;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class OperationInfoHistoryRepository : BaseRepository<EngineeringDBContext, OperationInfoHistory>, IOperationInfoHistoryRepository
{
    public OperationInfoHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetsOperationInfoHistoryByIdModel> Data, int RowCount)> GetsOperationInfoHistoryById(
        long oprationInfoId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfo.Id == oprationInfoId)

            .Select(item => new GetsOperationInfoHistoryByIdModel()
            {
                Id = item.Id,
                OperationInfoName = item.OperationInfoName,
                OperationInfoCode = item.OperationInfoCode,
                OperationLatinName = item.OperationLatinName,
                Priority = item.Priority,
                UnitOfMeasurementId = item.UnitOfMeasurementId,
                IsActive = item.IsActive,
                IsPriceList = item.IsPriceList,
                BasePrice = item.BasePrice,
                HaveStandard = item.HaveStandard,
                Created = item.Created,
                CreatorId = item.CreatorId,
            });

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var experts = await query
            .Page(pageIndex, pageSize)
            .ToListAsync(ct);

        return (experts, count);
    }

}