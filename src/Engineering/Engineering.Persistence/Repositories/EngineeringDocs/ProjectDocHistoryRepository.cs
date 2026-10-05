using Engineering.Application.Abstractions.Data.EngineeringDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;
using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Persistence.Repositories.EngineeringDocs;

public class ProjectDocHistoryRepository : BaseRepository<EngineeringDBContext, ProjectDocHistory>, IProjectDocHistoryRepository
{
    public ProjectDocHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetProjectDocHistoriesModel> Data, int RowCount)> GetProjectDocHistories(
    long projectDocId, int pageIndex, int pageSize, CT ct)
    {
        IQueryable<ProjectDocHistory> query = DbSet
            .Where(x =>
                !x.IsDeleted &&
                x.ProjectDocId == projectDocId)
            .OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query
            .Select(x => new GetProjectDocHistoriesModel
            {
                Id = x.Id,
                ProjectDocId = x.ProjectDocId,
                ProjectId = x.ProjectId,
                DisciplineId = x.DisciplineId,
                DisciplineDocId = x.DisciplineDocId,
                ThirdPartyId = x.ThirdPartyId,
                Url = x.Url,
                Description = x.Description,
                Code = x.Code,
                Revision = x.Revision,
                Sequence = x.Sequence,
                Status = x.Status,
                CreatorId = x.CreatorId,
                Created = x.Created
            })
            .ToListAsync(ct);

        return (data, count);
    }
}
