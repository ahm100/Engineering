using Engineering.Application.Abstractions.Data.EngineeringDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;
using Engineering.Domain.Entities.EngineeringDocs;
using Engineering.Persistence.Repositories.EngineeringDocs.Seeders;

namespace Engineering.Persistence.Repositories.EngineeringDocs;

public class DisciplineDocRepository : BaseRepository<EngineeringDBContext, DisciplineDoc>, IDisciplineDocRepository
{
    public DisciplineDocRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task Seeder(CancellationToken cancellationToken)
    {

        foreach (var item in SeedDisciplineDocs.All)
        {
            if (await DbSet.AnyAsync(x => x.Code == item.Code, cancellationToken))
                continue;

            await DbSet.AddAsync(
                 new DisciplineDoc(
                     item.Code,
                     item.Title,
                     "",
                     item.Description));
        }
    }



    public async Task<(List<GetDisciplineDocModel> Data, int RowCount)> GetDisciplineDoc(
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => !x.IsDeleted);

        query = query.OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = query.Select(item => new GetDisciplineDocModel()
        {
            Id = item.Id,
            Code = item.Code,
            Title = item.Title,
            Description = item.Description,
            CreatorId = item.CreatorId,
            UpdaterId = item.UpdaterId,
            IsActive = item.IsActive

        }).OrderBy(x => x.Id);

        var entities = await newQuery.ToListAsync(ct);

        return (entities, count);
    }

    public async Task<bool> IsDisciplineDocValid(
     long disciplineDocId,
     CT ct)
    {
        return await DbContext.Set<DisciplineDoc>()
            .AnyAsync(
                x => !x.IsDeleted &&
                     x.Id == disciplineDocId,
                ct);
    }


}
