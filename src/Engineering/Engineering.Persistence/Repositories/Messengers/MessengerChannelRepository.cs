using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Persistence.Repositories.Messengers;

public class MessengerChannelRepository : BaseRepository<EngineeringDBContext, MessengerChannel>, IMessengerChannelRepository
{
    public MessengerChannelRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<MessengerChannel?> GetById(
        long id, CT ct)
    {
        var query = DbSet.Where(m =>
        id == m.Id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<MessengerChannel?> GetBestChannel(
        long targetId,
        MessengerTargetType messengerTargetType,
        MessengerType messengerType,
        MessengerMessageType messengerMessageType, CT ct)
    {
        var query = DbSet.Where(m =>
            m.Messenger.TargetId == targetId &&
            m.Messenger.MessengerTargetType == messengerTargetType &&
            m.Messenger.MessengerType == messengerType &&
            m.MessengerMessageType == messengerMessageType);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<List<MessengerChannel>?> GetChannelByProjectIdAndCostCenterId(
        List<long> projectIds,
        List<long> costCenterIds,
        MessengerMessageType messengerMessageType,
        string chatId, CT ct)
    {
        var query = DbSet.Where(x =>
        (chatId == x.ChatId) &&
        (x.MessengerMessageType == messengerMessageType) &&
        ((x.Messenger.MessengerTargetType == MessengerTargetType.CostCenter && costCenterIds.Contains(x.Messenger.TargetId)) ||
        x.Messenger.MessengerTargetType == MessengerTargetType.Project && projectIds.Contains(x.Messenger.TargetId)));

        var item = await query.ToListAsync(ct);
        return item;
    }

    public async Task<(List<GetMessengerChannelsModel> Data, int RowCount)> GetMessengerChannels(
        List<long>? projectIds,
        List<long>? costCenterIds,
        MessengerMessageType? messengerMessageType,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x => !x.IsDeleted);

        if (projectIds is not null && projectIds.Count > 0)
            query = query.Where(x => (x.Messenger.MessengerTargetType == MessengerTargetType.Project &&
                projectIds.Contains(x.Messenger.TargetId)));

        if (costCenterIds is not null && costCenterIds.Count > 0)
            query = query.Where(x => (x.Messenger.MessengerTargetType == MessengerTargetType.CostCenter &&
                costCenterIds.Contains(x.Messenger.TargetId)));

        if (messengerMessageType is not null)
            query = query.Where(x => x.MessengerMessageType == messengerMessageType);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = query.Select(item => new GetMessengerChannelsModel()
        {
            Id = item.Id,
            ChatId = item.ChatId,
            ChatUrl = item.ChatUrl,
            ChatName = item.ChatName,
            MessengerMessageType = item.MessengerMessageType,
            MessengerId = item.MessengerId,
            MessengerType = item.Messenger.MessengerType,
            MessengerTargetType = item.Messenger.MessengerTargetType,
            CreatorId = item.CreatorId,
            Created = item.Created,
            UpdaterId = item.UpdaterId,
            Updated = item.Updated
        });

        var entities = await newQuery.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<List<MessengerChannel>?> GetFltrChannel(
        List<long>? projectIds,
        List<long>? costCenterIds,
        MessengerMessageType messengerMessageType, CT ct)
    {
        var query = DbSet
            .Where(x =>
                (x.MessengerMessageType == messengerMessageType || x.MessengerMessageType == MessengerMessageType.Other) &&
            (
                (costCenterIds == null ||
                (x.Messenger.MessengerTargetType == MessengerTargetType.CostCenter &&
                costCenterIds.Contains(x.Messenger.TargetId)))
                ||
                (projectIds == null ||
                (x.Messenger.MessengerTargetType == MessengerTargetType.Project &&
                projectIds.Contains(x.Messenger.TargetId)))
            ))
        .GroupBy(x => x.ChatId)
        .Select(g => g.First());

        var item = await query.ToListAsync(ct);
        return item;
    }

    public async Task<GetMessengerChannelByIdResponse?> GetMessengerChannelById(long id, CT ct)
    {
        return await DbSet
         .Where(oo => oo.Id == id)
         .Select(item => new GetMessengerChannelByIdResponse()
         {
             Id = item.Id,
             ChatId = item.ChatId,
             ChatName = item.ChatName,
             ChatUrl = item.ChatUrl,
             MessengerId = item.MessengerId,
             MessengerMessageType = item.MessengerMessageType,
         }).FirstOrDefaultAsync(ct);
    }
}