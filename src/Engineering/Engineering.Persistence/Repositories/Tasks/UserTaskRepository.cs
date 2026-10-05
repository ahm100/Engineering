using Engineering.Application.Services.Tasks.Contracts.GetUserTaskById;
using Engineering.Application.Services.Tasks.Contracts.GetUserTasks;
using Engineering.Application.Abstractions.Data.Tasks;
using UserTaskEntity = Engineering.Domain.Entities.Tasks.UserTask;
namespace Engineering.Persistence.Repositories.Tasks;


public class UserTaskRepository
    : BaseRepository<EngineeringDBContext, UserTaskEntity>,
      IUserTaskRepository
{
    public UserTaskRepository(EngineeringDBContext context)
        : base(context)
    {
    }

    public async Task<UserTaskEntity?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.TaskGroup)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted,
                ct);
    }

    public async Task<GetUserTaskResponse?> GetByIdForApi(
        long id, CT ct)
    {
        return await DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetUserTaskResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                TaskGroupId = x.TaskGroupId,
                TaskGroupTitle = x.TaskGroup.Title,
                Status = x.Status,
                OwnerUserId = x.OwnerUserId,
                CreatorId = x.CreatorId
            }).FirstOrDefaultAsync(ct);

    }

    public async Task<UserTaskEntity?> GetByOwnerAndId(
        long id, long ownerUserId, CT ct)
    {
        return await DbSet
            .Include(x => x.TaskGroup)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.OwnerUserId == ownerUserId &&
                !x.IsDeleted,
                ct);
    }

    public async Task<GetUserTasksResponse> GetUserTasks(
       GetUserTasksRequest request, CT ct)
    {
        var (ownerUserId, taskGroupId, status, pageIndex, pageSize) = request;
        var query = DbSet
            .Include(x => x.TaskGroup)
            .Where(x =>
                 (ownerUserId == null || x.OwnerUserId == ownerUserId) &&
                (taskGroupId == null || x.TaskGroupId == taskGroupId) &&
                (status == null || x.Status == status))
            .OrderByDescending(x => x.Created)
            .Select(x => new GetUserTasksModel(x.Id, x.Title, x.Description, x.TaskGroupId, x.TaskGroup.Title, x.Status, x.OwnerUserId));

        var count = await query.CountAsync(ct);      
        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex!.Value, pageSize!.Value);
        var data = await query.ToListAsync(ct);
        GetUserTasksResponse result = new GetUserTasksResponse(data, count);
        return result;
    }
}