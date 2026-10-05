using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Persistence.Repositories.TelegramChats;

public class TelegramMessageHistoryHistoryRepository : BaseRepository<EngineeringDBContext, TelegramMessageHistory>,
    ITelegramMessageHistoryRepository
{
    public TelegramMessageHistoryHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<TelegramMessageHistory> Data, int RowCount)> GetsFilteredTelegramMessageHistory(
        List<long>? ids,
        string? filterData,
        TelegramMessageType? messageType,
        bool? isSend,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Include(x => x.TelegramChat)
            .Where(oo => ids.Contains(oo.Id)
                        && (messageType == null || oo.TelegramMessageType == messageType)
                        && (isSend == null || oo.IsSend == isSend)
            );
        query = query.OrderBy(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}