using Engineering.Application.Abstractions.Data.EngineeringDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDiscipline;
using Engineering.Domain.Entities.EngineeringDocs;
using Engineering.Persistence.Repositories.EngineeringDocs.Seeders;

namespace Engineering.Persistence.Repositories.EngineeringDocs;

public class DisciplineRepository : BaseRepository<EngineeringDBContext, Discipline>, IDisciplineRepository
{
    public DisciplineRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task Seeder(CancellationToken cancellationToken)
    {

        foreach (var item in SeedDisciplines.All)
        {
            if (await DbSet.AnyAsync(x => x.Code == item.Code, cancellationToken))
                continue;

            await DbSet.AddAsync(
               new Discipline(
                   item.Code,
                   item.Name,
                   item.EnglishName,
                   item.Description));
        }
    }

    public async Task<(List<GetDisciplineModel> Data, int RowCount)> GetDiscipline(
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet.Where(x => !x.IsDeleted);

        query = query.OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = query.Select(item => new GetDisciplineModel()
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            EnglishName = item.EnglishName,
            Description = item.Description,
            CreatorId = item.CreatorId,
            UpdaterId = item.UpdaterId,
            IsActive = item.IsActive

        }).OrderBy(x => x.Id);

        var entities = await newQuery.ToListAsync(ct);

        return (entities, count);
    }


    public async Task<bool> IsDisciplineValid(
    long disciplineId, CT ct)
    {
        return await DbContext.Set<Discipline>()
            .AnyAsync(
                x => !x.IsDeleted &&
                     x.Id == disciplineId,
                ct);
    }

}
