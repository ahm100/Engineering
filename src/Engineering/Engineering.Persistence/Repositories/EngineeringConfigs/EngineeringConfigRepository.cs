using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrConfigs;
using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Persistence.Repositories.EngineeringConfigs;

public class EngineeringConfigRepository : BaseRepository<EngineeringDBContext, EngineeringConfig>, IEngineeringConfigRepository
{
    public EngineeringConfigRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<EngineeringConfig>> GetActiveEngineeringConfigs(
            long companyId,
            CT ct)
    {
        return await DbSet
            .Where(x => x.CompanyId == companyId).ToListAsync();
    }

    public async Task<EngineeringConfig?> GetConfigById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<EngineeringConfig?> GetActiveConfig(
        long companyId, CT ct)
    {
        return await DbSet
            .Include(x => x.EngineeringCodingConfigs)
            .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.IsActive, ct);
    }

    public async Task<(List<GetFltrConfigsModel> Data, int RowCount)> GetFltrConfigs(
            long companyId,
            bool? sendTelegramMessage,
            bool? isActive,
            int pageIndex,
            int pageSize,
            CT ct)
    {
        var query = DbSet
            .Where(x => x.CompanyId == companyId &&
            (sendTelegramMessage == null || x.SendTelegramMessage == sendTelegramMessage) &&
            (isActive == null || x.IsActive == isActive))
            .Select(x => new GetFltrConfigsModel
            {
                Id = x.Id,
                CompanyId = x.CompanyId,
                SendTelegramMessage = x.SendTelegramMessage,
                ProjectThirdParties = x.ProjectThirdParties,
                IsActive = x.IsActive,
                CreatorId = x.CreatorId,
                Created = x.Created,
                Updated = x.Updated
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var configs = await query.ToListAsync(ct);
        return (configs, count);
    }
}