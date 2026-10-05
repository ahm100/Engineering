using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigByEngConfigId;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigById;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrCodingConfigs;
using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Persistence.Repositories.EngineeringConfigs;

public class EngineeringCodingConfigRepository : BaseRepository<EngineeringDBContext, EngineeringCodingConfig>, IEngineeringCodingConfigRepository
{
    public EngineeringCodingConfigRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetCodingConfigByIdResponse?> GetCodingConfigById(
            long id,
            CT ct)
    {
        return await DbSet
            .Where(x => id == x.Id)
            .Select(x => new GetCodingConfigByIdResponse
            {
                Id = x.Id,
                Type = x.Type,
                Prefix = x.Prefix,
                IsActive = x.IsActive,
                CreatorId = x.CreatorId,
                Created = x.Created,
                Updated = x.Updated
            }).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetFltrCodingConfigsModel> Data, int RowCount)> GetFltrCodingConfigs(
            bool? isActive,
            int pageIndex,
            int pageSize,
            CT ct)
    {
        var query = DbSet
            .Where(x => (isActive == null || x.IsActive == isActive))
            .Select(x => new GetFltrCodingConfigsModel
            {
                Id = x.Id,
                Type = x.Type,
                Prefix = x.Prefix,
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

    public async Task<(List<GetCodingConfigByEngConfigModel> Data, int RowCount)> GetCodingConfigByEngConfigModel(
        long configId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => (x.EngineeringConfigId == configId))
            .Select(x => new GetCodingConfigByEngConfigModel
            {
                Id = x.Id,
                Type = x.Type,
                Prefix = x.Prefix,
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