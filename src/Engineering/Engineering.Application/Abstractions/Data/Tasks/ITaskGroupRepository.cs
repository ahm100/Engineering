using Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;
using Engineering.Application.Services.Tasks.Contracts.GetTaskGroups;
using Engineering.Domain.Entities.Tasks;


namespace Engineering.Application.Abstractions.Data.Tasks;



public interface ITaskGroupRepository : IBaseRepository<TaskGroup>
{
    Task<GetTaskGroupResponse?> GetById(long id, CT ct);

    Task<bool> HasTasks(long id, CT ct);

    Task<GetTaskGroupsResponse> GetTaskGroups(int pageIndex, int pageSize, CT ct);
    Task<TaskGroup?> GetByIdForUpdate(long id, CT ct);

}