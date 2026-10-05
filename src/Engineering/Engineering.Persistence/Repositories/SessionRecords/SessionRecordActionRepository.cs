using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;
using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Persistence.Repositories.SessionRecords;

public class SessionRecordActionRepository : BaseRepository<EngineeringDBContext, SessionRecordAction>, ISessionRecordActionRepository
{
    public SessionRecordActionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetUserSessionRecordActionResponseModel> Data, int RowCount)> GetUserSessionRecordAction(
            long userId, string? description, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(e => e.UserId == userId && !e.IsDeleted);

        if (!string.IsNullOrWhiteSpace(description))
            query = query.Where(e => e.Description.Contains(description));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query
            .OrderBy(e => e.Deadline)
            .Select(e => new GetUserSessionRecordActionResponseModel
            {
                Id = e.Id,
                Description = e.Description,
                Status = e.Status,
                Deadline = e.Deadline
            })
            .ToListAsync(ct);

        return (data, count);
    }
}
