using Engineering.Application.Abstractions.Data.EngineeringDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;
using Engineering.Domain.Entities.EngineeringDocs;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Repositories.EngineeringDocs
{
    public class ProjectDocRepository : BaseRepository<EngineeringDBContext, ProjectDoc>, IProjectDocRepository
    {
        public ProjectDocRepository(EngineeringDBContext context) : base(context)
        {
        }


        public async Task<(string ProjectCode, string DisciplineCode, string DisciplineDocCode)?>
        GetProjectDocCodeData(
        long projectId,
        long disciplineId,
        long disciplineDocId,
        CT ct)
        {
            var projectCode = await DbContext.Set<Project>()
                .Where(x => x.Id == projectId)
                .Select(x => x.ProjectCode)
                .FirstOrDefaultAsync(ct);

            var disciplineCode = await DbContext.Set<Discipline>()
                .Where(x => x.Id == disciplineId)
                .Select(x => x.Code)
                .FirstOrDefaultAsync(ct);

            var disciplineDocCode = await DbContext.Set<DisciplineDoc>()
                .Where(x => x.Id == disciplineDocId)
                .Select(x => x.Code)
                .FirstOrDefaultAsync(ct);

            if (projectCode is null ||
                disciplineCode is null ||
                disciplineDocCode is null)
                return null;

            return (
                projectCode,
                disciplineCode,
                disciplineDocCode);
        }


        public async Task<int> GetNextSequence(
            long projectId,
            long disciplineId,
            long disciplineDocId,
            CT ct)
        {
            var lastSequence = await DbSet
                .Where(x =>
                    !x.IsDeleted &&
                    x.ProjectId == projectId &&
                    x.DisciplineId == disciplineId &&
                    x.DisciplineDocId == disciplineDocId)
                .MaxAsync(x => (int?)x.Sequence, ct);

            //var parts = lastProjectDoc.Code.Split('-');  // OrderByDescending(x => x.Code) chon reshte hst momkene kharab she order
            //if (parts.Length < 5 || !int.TryParse(parts[3], out var lastSequence))
            //    return 1;

            return (lastSequence ?? 0) + 1;
        }






        public async Task<GetProjectDocByIdResponse?> GetProjectDocById(
            long id,
            CT ct)
        {
            return await DbSet
                .Where(x => x.Id == id && !x.IsDeleted)
                .Select(item => new GetProjectDocByIdResponse
                {
                    Id = item.Id,
                    ProjectId = item.ProjectId,
                    DisciplineId = item.DisciplineId,
                    DisciplineDocId = item.DisciplineDocId,
                    DisciplineDocTitle = item.DisciplineDoc.Title,
                    DisciplineDocEnTitle = item.DisciplineDoc.EnTitle,
                    ThirdPartyId = item.ThirdPartyId,
                    Url = item.Url,
                    Description = item.Description,
                    Code = item.Code,
                    Revision = item.Revision,
                    Sequence = item.Sequence,
                    Status = item.Status
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<(List<GetProjectDocsModel> Data, int RowCount)> GetProjectDocs(
            long? projectId,
            long? disciplineId,
            long? disciplineDocId,
            int pageIndex,
            int pageSize,
            CT ct)
        {
            var query = DbSet
                .Where(x => !x.IsDeleted);

            if (projectId.HasValue)
                query = query.Where(x => x.ProjectId == projectId);

            if (disciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == disciplineId);

            if (disciplineDocId.HasValue)
                query = query.Where(x => x.DisciplineDocId == disciplineDocId);

            query = query.OrderByDescending(x => x.Created);

            var count = await query.CountAsync(ct);

            if (pageIndex > 0 && pageSize > 0)
                query = query.Page(pageIndex, pageSize);

            var newQuery = query.Select(item => new GetProjectDocsModel
            {
                Id = item.Id,
                ProjectId = item.ProjectId,
                DisciplineId = item.DisciplineId,
                DisciplineDocId = item.DisciplineDocId,
                DisciplineDocTitle = item.DisciplineDoc.Title,
                DisciplineDocEnTitle = item.DisciplineDoc.EnTitle,
                ThirdPartyId = item.ThirdPartyId,
                Url = item.Url,
                Description = item.Description,
                Code = item.Code,
                Revision = item.Revision,
                Sequence = item.Sequence,
                Status = item.Status,
                CreatorId = item.CreatorId,
                Created = item.Created,
                UpdaterId = item.UpdaterId,
                Updated = item.Updated
            });

            var entities = await newQuery.ToListAsync(ct);

            return (entities, count);
        }

        public async Task<ProjectDoc?> GetProjectDocEntityById(
        long id, CT ct)
        {
            return await DbSet
                .FirstOrDefaultAsync(
                    x => x.Id == id && !x.IsDeleted,
                    ct);
        }

    }
}
