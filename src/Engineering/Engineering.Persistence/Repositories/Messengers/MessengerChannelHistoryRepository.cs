using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Persistence.Repositories.Messengers;

public class MessengerChannelHistoryRepository : BaseRepository<EngineeringDBContext, MessengerChannelHistory>,
    IMessengerChannelHistoryRepository
{
    public MessengerChannelHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<MessengerChannelHistory>?> GetFltrChannelHistory(
        List<long> ids,
        MessengerMessageType? messageType,
        bool? isSend, CT ct)
    {
        var items = await DbSet
            .Include(x => x.MessengerChannel)
            .Where(x =>
            ids.Contains(x.Id) &&
            (messageType == null || x.MessengerChannel.MessengerMessageType == messageType) &&
           (isSend == null || x.IsSend == isSend)).ToListAsync(ct);


        return items;
    }

    public async Task<(List<GetMessengerChannelHistoriesModel> Data, int RowCount)> GetMessengerChannelHistories(
        List<long>? ids,
        long? targetId,
        MessengerMessageType? messageType,
        bool? isSend,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => !x.IsDeleted);

        if (ids is not null && ids.Count > 0)
            query = query.Where(x => ids.Contains(x.Id));

        if (filterData is not null)
            query = query.Where(x =>
                x.Message!.Contains(filterData));

        if (messageType is not null)
            query = query.Where(x => x.MessengerChannel.MessengerMessageType == messageType);

        if (isSend is not null)
            query = query.Where(x => x.IsSend == isSend);

        if (targetId.HasValue)
            query.Where(x => x.MessengerChannel.Messenger.TargetId == targetId);

        var baseQuery = query
            .Select(x => new GetMessengerChannelHistoriesModel()
            {
                Id = x.Id,
                ChatId = x.MessengerChannel.ChatId,
                Message = x.Message,
                ErrorMessage = x.ErrorMessage,
                FileUrls = x.FileUrls,
                IsSend = x.IsSend,
                MessengerChannelId = x.MessengerChannelId,
                MessengerMessageType = x.MessengerChannel.MessengerMessageType,

                CreatorId = x.CreatorId,
                Created = x.Created,
                UpdaterId = x.UpdaterId,
                Updated = x.Updated
            });

        baseQuery = baseQuery.OrderBy(oo => oo.Created);
        var count = await baseQuery.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            baseQuery = baseQuery.Page(pageIndex, pageSize);

        var items = await baseQuery.ToListAsync(ct);
        return (items, count);
    }



}