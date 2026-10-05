using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;
public class MeasureUnitGroupRepository : BaseRepository<EngineeringDBContext, MeasureUnitGroup>, IMeasureUnitGroupRepository
{
    public MeasureUnitGroupRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task CreateOrUpdateAsync(MeasureUnitGroup measureUnitGroup, CT ct)
    {
        var existMeasureUnitGroup = await DbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == measureUnitGroup.Id && !e.IsDeleted, ct);
        if (existMeasureUnitGroup is null)
            await base.Create(measureUnitGroup, ct);
        else
        {
            DbContext.Entry(existMeasureUnitGroup).CurrentValues.SetValues(measureUnitGroup);
            await DbContext.SaveChangesAsync(ct);
        }
    }

    public async Task<MeasureUnitGroup?> GetByIdAsync(long id, CT ct)
    {
        return await DbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id & !e.IsDeleted, ct);
    }

    public async Task<List<MeasureUnitGroup>?> GetByGroupIdAsync(long groupId, CT ct)
    {
        var result = await DbSet.IgnoreQueryFilters()
                        .Where(e => !e.IsDeleted && e.MeasureUnits
                        /*.Select(e => e.Groups.Any(x => x.Id == groupId))*/.Any())
                        .ToListAsync(ct);
        return result;
    }
}