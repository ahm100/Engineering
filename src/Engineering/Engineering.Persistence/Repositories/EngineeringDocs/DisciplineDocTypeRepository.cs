using Engineering.Application.Abstractions.Data.EngineeringDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;
using Engineering.Domain.Entities.EngineeringDocs;
using Engineering.Persistence.Repositories.EngineeringDocs.Seeders;

namespace Engineering.Persistence.Repositories.EngineeringDocs
{
    public class DisciplineDocTypeRepository : BaseRepository<EngineeringDBContext, DisciplineDocType>, IDisciplineDocTypeRepository

    {
        public DisciplineDocTypeRepository(EngineeringDBContext context) : base(context)
        {
        }


        public async Task SeedEngineeringDocs(CancellationToken cancellationToken)
        {

            var DisciplinesDataSet = DbContext.Set<Discipline>();
            var DisciplineDocsDataSet = DbContext.Set<DisciplineDoc>();

            var disciplines = await DisciplinesDataSet
                .ToDictionaryAsync(x => x.Code, cancellationToken);

            var disciplineDocs = await DisciplineDocsDataSet
                .ToDictionaryAsync(x => x.Code, cancellationToken);

            foreach (var item in SeedDisciplineDocTypes.All)
            {
                if (!disciplines.TryGetValue(item.DisciplineCode, out var discipline))
                    continue;

                if (!disciplineDocs.TryGetValue(item.DisciplineDocCode, out var disciplineDoc))
                    continue;

                var exists = await DbSet.AnyAsync(
                    x => x.DisciplineId == discipline.Id &&
                         x.DisciplineDocId == disciplineDoc.Id &&
                         x.Code == item.Code,
                    cancellationToken);

                if (exists)
                    continue;

                await DbSet.AddAsync(
                    new DisciplineDocType(
                        discipline.Id,
                        disciplineDoc.Id,
                        item.Code,
                        item.Description));
            }

        }


        public async Task<(List<GetDisciplineDocTypeModel> Data, int RowCount)> GetDisciplineDocType(
        long? disciplineId,
        int pageIndex,
        int pageSize,
        CT ct)
        {
            var query = DbSet.Where(x => !x.IsDeleted);

            if (disciplineId is not null)
                query = query.Where(x => x.DisciplineId == disciplineId);

            query = query.OrderByDescending(x => x.Created);

            var count = await query.CountAsync(ct);

            if (pageIndex > 0 && pageSize > 0)
                query = query.Page(pageIndex, pageSize);

            var newQuery = query.Select(item => new GetDisciplineDocTypeModel()
            {
                Id = item.Id,
                DisciplineId = item.DisciplineId,
                DisciplineDocId = item.DisciplineDocId,
                Code = item.Discipline.Code,
                Description = item.Description,
                CreatorId = item.CreatorId,
                UpdaterId = item.UpdaterId,
                IsActive = item.IsActive

            }).OrderBy(x => x.Id);

            var entities = await newQuery.ToListAsync(ct);

            return (entities, count);
        }

        public async Task<bool> IsDisciplineDocTypeValid(
        long disciplineId, long disciplineDocId, CT ct)
        {
            //var DisciplineDocTypeSet = DbContext.Set<DisciplineDocType>();
            return await DbSet.AnyAsync(
                x =>
                    !x.IsDeleted &&
                    x.DisciplineId == disciplineId &&
                    x.DisciplineDocId == disciplineDocId,
                ct);
        }


    }
}
