using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Application.Services.TelegramChats.Models.ResponseModels;
using Engineering.Domain.Entities.TelegramChats.Enums;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Persistence.Repositories.TelegramChats;

public class TelegramChatRepository : BaseRepository<EngineeringDBContext, TelegramChat>, ITelegramChatRepository
{
    public TelegramChatRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<TelegramChat?> FindByName(string name, CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
             .Include(oo => oo.TelegramChatTypes)
            .Where(oo => oo.ChatName == name &&

            !oo.CostCenter.IsDeleted);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<TelegramChat?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
            .Include(oo => oo.TelegramChatTypes)
            .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public Task<bool> FindTelegramChatByNames(List<string> names, CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var query = DbSet
            .AnyAsync(oo =>
            names.Contains(oo.ChatName),
            ct);
#pragma warning restore CS8604 // Possible null reference argument.

        return query;
    }

    public async Task<TelegramChat?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
             .Include(oo => oo.TelegramChatTypes)

             .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }


    public async Task<List<TelegramChat>> GetsTelegramChatByIds(List<long> ids, CT ct)
    {
        var query = DbSet
             .Include(oo => oo.TelegramChatTypes)
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<TelegramChat> Data, int RowCount)> GetTelegramChats(
        List<long>? ids,
        TelegramMessageType? telegramMessageType,
        string? filterData,
        long? costCenterId,
        long? projectId,
        string? name,
        bool? isActive,
        string[]? orderBy,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
             .Include(oo => oo.TelegramChatTypes)
            .Where(oo =>
            //(name == null || oo.ChatName.Contains(name)) &&
            (ids == null || ids.Count == 0 || ids.Contains(oo.Id))
            && (telegramMessageType == null || oo.TelegramChatTypes.Any(x => x.TelegramMessageType == telegramMessageType))
            //&& (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ChatName, filterData.MakeLikePattern())) 
            && (costCenterId == null || oo.CostCenter.Id == costCenterId) && !oo.CostCenter.IsDeleted);

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        if (getOther == true)
        {
            var otherGroupsQuery = query.Where(oo => oo.TelegramChatTypes.Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
            if (await otherGroupsQuery.AnyAsync(ct))
                query = otherGroupsQuery;
        }

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<TelegramChat> Data, int RowCount)> GetActiveTelegramChats(
        string? filterData,
        long? costCenterId,
        long? projectId,
        string? name,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
             .Include(oo => oo.TelegramChatTypes)
            .Where(oo =>
            //(string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ChatName, filterData.MakeLikePattern())) &&
            //(name == null || oo.ChatName.Contains(name)) &&
            (costCenterId == null || oo.CostCenter.Id == costCenterId) &&
            oo.IsActive &&
            !oo.CostCenter.IsDeleted &&
            oo.CostCenter.IsActive);

        if (getOther == true)
        {
            var otherGroupsQuery = query.Where(oo => oo.TelegramChatTypes.Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
            if (await otherGroupsQuery.AnyAsync(ct))
                query = otherGroupsQuery;
        }

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<TelegramChat> Data, int RowCount)> GetByCostCenterId(
        long costCenterId,
        long? projectId,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
             .Include(oo => oo.TelegramChatTypes)
            .Where(oo => oo.CostCenter.Id == costCenterId);

        if (getOther == true)
        {
            var otherGroupsQuery = query.Where(oo => oo.TelegramChatTypes.Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
            if (await otherGroupsQuery.AnyAsync(ct))
                query = otherGroupsQuery;
        }

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
    public async Task<(List<TelegramChat> Data, int RowCount)> GetByProjectOperationDetailId(
        long projectOperationDetailId,
        bool? getOther,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
            .Include(oo => oo.TelegramChatTypes)
            .Where(oo => oo.CostCenter.Projects
                .Any(x => x.ProjectOperations
                    .Any(y => y.ProjectOperationDetails
                        .Any(p => p.Id.Equals(projectOperationDetailId))))
                        && oo.TelegramChatTypes.Any(z => z.TelegramMessageType == TelegramMessageType.DailyOperation));

        if (getOther == true)
        {
            var otherGroupsQuery = query.Where(oo => oo.TelegramChatTypes.Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
            if (await otherGroupsQuery.AnyAsync(ct))
                query = otherGroupsQuery;
        }

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
    public async Task<(List<TelegramChat> Data, int RowCount)> GetByRequestGoodsSupplyId(
        long requestGoodsSupplyId,
        bool? getOther,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
            .Include(oo => oo.TelegramChatTypes)
            .Where(oo => oo.CostCenter.Projects
                            .Any(x => x.ProjectOperations
                                .Any(y => y.RequestGoodsSupplies.Any(z => z.RequestGoodsSupplyProducts.Any(p => p.Id.Equals(requestGoodsSupplyId)))))
                        && oo.TelegramChatTypes.Any(z =>
                            z.TelegramMessageType == TelegramMessageType.SupplyRequestRejection
                            || z.TelegramMessageType == TelegramMessageType.SupplyRequestReturn));

        if (getOther == true)
        {
            var otherGroupsQuery = query.Where(oo => oo.TelegramChatTypes.Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
            if (await otherGroupsQuery.AnyAsync(ct))
                query = otherGroupsQuery;
        }
        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
    public async Task<(List<TelegramMessageResponseModel> Data, int RowCount)> GetByTransportationRequestId(
        long transportationRequestId,
        bool? getOther,
        TelegramMessageType telegramMessageType, CT ct)
    {
        var query = DbSet
                .Where(oo =>
                    (oo.CostCenter.Projects.Any(x => x.TransportationRequestProjects.Any(p => p.TransportationRequest.Id.Equals(transportationRequestId))) ||
                    oo.CostCenter.TransportationRequestCostCenters.Any(p => p.TransportationRequest.Id.Equals(transportationRequestId))) &&
                    oo.TelegramChatTypes.Any(z => z.TelegramMessageType == telegramMessageType))
                .Select(x => new TelegramMessageResponseModel()
                {
                    Id = x.Id,
                    ChatId = x.ChatId,
                    IsActive = x.IsActive,
                    Created = x.Created,
                    TelegramChatTypes = x.TelegramChatTypes.Select(z => new TelegramChatTypeResponseModel()
                    {
                        ChatId = z.ChatId,
                        TelegramMessageType = z.TelegramMessageType
                    }).ToList()
                });

        //if (getOther == true)
        //{
        //    var otherGroupsQuery = query
        //        .Where(oo => oo.TelegramChatTypes != null &&
        //                    oo.TelegramChatTypes.Count > 0 &&
        //                    oo.TelegramChatTypes.AsQueryable().Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
        //    if (await otherGroupsQuery.AnyAsync(ct))
        //        query = otherGroupsQuery;
        //}

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<TelegramChat> Data, int RowCount)> GetBySnapIds(
        List<long> ids,
        bool? getOther,
        TelegramMessageType telegramMessageType,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
            .Include(oo => oo.TelegramChatTypes)
            .Where(oo => (oo.CostCenter.Projects.Any(x => x.TransportationRequestProjects.Any(p => ids.Contains(p.TransportationRequest.Id))) ||
                         oo.CostCenter.TransportationRequestCostCenters.Any(p => ids.Contains(p.TransportationRequest.Id))) &&
                         oo.TelegramChatTypes.Any(z => z.TelegramMessageType == telegramMessageType));

        if (getOther == true)
        {
            var otherGroupsQuery = query.Where(oo => oo.TelegramChatTypes.Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
            if (await otherGroupsQuery.AnyAsync(ct))
                query = otherGroupsQuery;
        }

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<TelegramChat> Data, int RowCount)> GetsTelegramChatByCostCenterIds(
        List<long>? costCenterIds,
        TelegramMessageType? telegramMessageType,
        string? filterData,
        bool? isActive,
        bool? getOther,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
            .Include(oo => oo.TelegramChatTypes)
            .Where(oo => (costCenterIds == null || costCenterIds.Contains(oo.CostCenter.Id))
                        && (telegramMessageType == null || oo.TelegramChatTypes.Any(x => x.TelegramMessageType == telegramMessageType))
            //&& (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ChatName, filterData.MakeLikePattern()))
            );

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        if (getOther == true)
        {
            var otherGroupsQuery = query.Where(oo => oo.TelegramChatTypes.Any(x => x.TelegramMessageType == TelegramMessageType.OtherGroups));
            if (await otherGroupsQuery.AnyAsync(ct))
                query = otherGroupsQuery;
        }
        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}