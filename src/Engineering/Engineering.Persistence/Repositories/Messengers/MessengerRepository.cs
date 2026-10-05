using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Persistence.Repositories.Messengers;

public class MessengerRepository : BaseRepository<EngineeringDBContext, Messenger>, IMessengerRepository
{
    public MessengerRepository(EngineeringDBContext context) : base(context)
    {
    }



    public async Task<Messenger?> GetMessenger(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }
    public async Task<List<Messenger>> GetMessengers(
        List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(w =>
                ids.Contains(w.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<GetMessengerByIdResponse?> GetMessengerById(long id, CT ct)
    {
        return await DbSet
            .Where(oo => oo.Id == id)
            .Select(item => new GetMessengerByIdResponse()
            {
                Id = item.Id,
                IsActive = item.IsActive,
                Description = item.Description,
                MessengerTargetType = item.MessengerTargetType,
                MessengerType = item.MessengerType,
                TargetId = item.TargetId,
            }).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetMessengersModel> Data, int RowCount)> GetMessengers(
        long? targetId,
        MessengerTargetType? messengerTargetType,
        MessengerType? messengerType,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => !x.IsDeleted);

        if (messengerTargetType is not null)
            query = query.Where(x => x.MessengerTargetType == messengerTargetType);

        if (messengerType is not null)
            query = query.Where(x => x.MessengerType == messengerType);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = query.Select(item => new GetMessengersModel()
        {
            Id = item.Id,
            MessengerType = item.MessengerType,
            MessengerTargetType = item.MessengerTargetType,
            TargetId = item.TargetId,
            Description = item.Description,
            IsActive = item.IsActive,
            CreatorId = item.CreatorId,
            Created = item.Created,
            UpdaterId = item.UpdaterId,
            Updated = item.Updated
        });

        var entities = await newQuery.ToListAsync(ct);
        return (entities, count);
    }

}