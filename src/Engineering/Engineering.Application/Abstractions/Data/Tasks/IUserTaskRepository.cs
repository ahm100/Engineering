using Engineering.Application.Services.Tasks.Contracts.GetUserTaskById;
using Engineering.Application.Services.Tasks.Contracts.GetUserTasks;
using global::Engineering.Domain.Entities.Tasks;
using global::Engineering.Domain.Entities.Tasks.Enums;
namespace Engineering.Application.Abstractions.Data.Tasks;

public interface IUserTaskRepository : IBaseRepository<UserTask>
{
    Task<UserTask?> GetById(
        long id, CT ct);

    Task<GetUserTaskResponse?> GetByIdForApi(
    long id, CT ct);

    Task<UserTask?> GetByOwnerAndId(
    long id,
    long ownerUserId,
    CT ct);

    Task<GetUserTasksResponse> GetUserTasks(
       GetUserTasksRequest request, CT ct);
}