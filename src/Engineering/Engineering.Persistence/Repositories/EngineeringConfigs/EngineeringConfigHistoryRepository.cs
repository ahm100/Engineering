using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;
using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Persistence.Repositories.EngineeringConfigs;

public class EngineeringConfigHistoryRepository : BaseRepository<EngineeringDBContext, EngineeringConfigHistory>, IEngineeringConfigHistoryRepository
{
    public EngineeringConfigHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
    public async Task<EngineeringConfigHistory?> GetConfigById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }
    public async Task<EngineeringConfigHistory?> GetByConfigId(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.EngineeringConfigId == id, ct);
    }
    public async Task<(List<GetConfigHistoryByConfigIdModel>? Data, int Count)> GetConfigHistoryByConfigId(
        long id,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.EngineeringConfigId == id)
            .Select(x => new GetConfigHistoryByConfigIdModel
            {
                Id = x.Id,
                EngineeringConfigId = x.EngineeringConfigId,
                SendTelegramMessage = x.SendTelegramMessage,
                ProjectThirdParties = x.ProjectThirdParties,
                IsActive = x.IsActive,
                CreatorId = x.CreatorId,
                Created = x.Created,
                Updated = x.Updated,
            });

        var count = await query.CountAsync();

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);

        return (newQuery, count);
    }
}
