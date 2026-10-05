
using Engineering.Application.Abstractions.Data.Tasks;
using Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;
using Engineering.Application.Services.Tasks.Contracts.GetTaskGroups;
using Engineering.Domain.Entities.Tasks;

namespace Engineering.Persistence.Repositories.Tasks;


public class TaskGroupRepository
    : BaseRepository<EngineeringDBContext, TaskGroup>,
      ITaskGroupRepository
{
    public TaskGroupRepository(EngineeringDBContext context)
        : base(context)
    {
    }

    public async Task<GetTaskGroupResponse?> GetById(long id, CT ct)
    {

        return await DbSet
          .Where(x => x.Id == id)
          .Select(x => new GetTaskGroupResponse()
          {
              Id = x.Id,
              Title = x.Title,
              Description = x.Description,
              CreatorId = x.CreatorId
          }).FirstOrDefaultAsync(ct);
    }

    public async Task<TaskGroup?> GetByIdForUpdate(long id, CT ct)
    {
        return await DbSet.Include(us => us.UserTasks)
          .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> HasTasks(long id, CT ct)
    {
        return await DbSet
            .Where(x => x.Id == id)
            .SelectMany(x => x.UserTasks)
            .AnyAsync(ct);
    }
    public async Task<GetTaskGroupsResponse> GetTaskGroups(int pageIndex, int pageSize, CT ct)
    {
        IQueryable<GetTaskGroupsModel> query = DbSet
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.Created)
             .Select(x => new GetTaskGroupsModel(x.Id, x.Title, x.Description));
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);
        var data = await query.ToListAsync(ct);
        GetTaskGroupsResponse result = new GetTaskGroupsResponse(data, count);
        return result;
    }

}