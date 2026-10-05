using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class MeasureUnitRepository : BaseRepository<EngineeringDBContext, MeasureUnit>, IMeasureUnitRepository
{
    public MeasureUnitRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task CreateOrUpdateAsync(MeasureUnit measureUnit, CT ct)
    {
        var existMeasureUnit = await DbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == measureUnit.Id && !e.IsDeleted, ct);
        if (existMeasureUnit is null)
            await base.Create(measureUnit, ct);
        else
        {
            DbContext.Entry(existMeasureUnit).CurrentValues.SetValues(measureUnit);
            await DbContext.SaveChangesAsync(ct);
        }
    }

    public async Task<List<MeasureUnit?>> GetByGroupIdAsync(long groupId, CT ct)
    {
        return await DbSet.IgnoreQueryFilters()
                    .Where(e => e.MeasureUnitGroup.Id == groupId && !e.IsDeleted)
                    .ToListAsync(ct);
    }

    public async Task<MeasureUnit?> GetByIdAsync(long id, CT ct)
    {
        return await DbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, ct);
    }

    public async Task<MeasureUnit?> GetByIdIgnoreAsync(long id, CT ct)
    {
        return await DbSet
            .Include(x => x.MeasureUnitGroup)
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<List<MeasureUnit?>> GetAllByIdAsync(long id, CT ct)
    {
        var current = await DbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, ct);
        return await DbSet.IgnoreQueryFilters()
                    .Where(e => !e.IsDeleted
                     && e.MeasureUnitGroup.Id == current.MeasureUnitGroup.Id)
                    .ToListAsync(ct);
    }

    public async Task<List<MeasureUnit>> GetMeasureUnitsByIds(List<long> ids,
                                                              CT ct)
    {
        var query = DbSet.IgnoreQueryFilters().Include(x => x.MeasureUnitGroup)
            .Where(x => !x.IsDeleted &&
                        ids.Contains(x.Id));

        var items = await query.ToListAsync(ct);

        return items;
    }

    public async Task<List<MeasureUnit>> GetByNames(
        List<string> names,
        long companyId,
        CT ct)
    {
        var query = DbSet.Where(x => x.CompanyId == companyId);
        var items = await query.ToListAsync(ct);

        items = items.Where(x => names.Contains(StringSeparator.Normalize(x.Name))).ToList();

        var c = StringSeparator.Normalize("هزار کیلو کالری در ساعت");
        return items;
    }

    public async Task<List<MeasureUnitDto>> GetByNamesForFehrestBaha(List<string> names, CT ct)
    {
        var query = DbSet;
        try
        {
            var projectedItems = await DbSet
              .Select(x => new { x.Id, x.Name })
              .ToListAsync(ct);

            // 2. Filter and map to your DTO
            return projectedItems
                .Where(x => names.Contains(StringSeparator.Normalize(x.Name)))
                .DistinctBy(x => StringSeparator.Normalize(x.Name))
                .Select(x => new MeasureUnitDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .DistinctBy(x => x.Name)
                .ToList();

        }
        catch (Exception)
        {

            throw;
        }

    }

 

    public async Task<MeasureUnit?> GetByName(
        string name,
        CT ct)
    {
        var query = DbSet.Where(x => name == x.Name);
        var items = await query.FirstOrDefaultAsync(ct);
        return items;
    }
}