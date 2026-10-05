using Engineering.Application.Services.Tasks.Contracts.CreateTaskGroup;
using Engineering.Application.Services.Tasks.Contracts.CreateUserTask;
using Engineering.Application.Services.Tasks.Contracts.DeleteTaskGroup;
using Engineering.Application.Services.Tasks.Contracts.DeleteUserTask;
using Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;
using Engineering.Application.Services.Tasks.Contracts.GetTaskGroups;
using Engineering.Application.Services.Tasks.Contracts.GetUserTaskById;
using Engineering.Application.Services.Tasks.Contracts.GetUserTasks;
using Engineering.Application.Services.Tasks.Contracts.UpdateTaskGroup;
using Engineering.Application.Services.Tasks.Contracts.UpdateUserTask;

namespace Engineering.Application.Services.Tasks;

public interface ITaskLogic
{
    Task<Result<CreateTaskGroupResponse?>> CreateTaskGroup(
        CreateTaskGroupRequest request, CT ct);
    Task<Result<UpdateTaskGroupResponse?>> UpdateTaskGroup(
       UpdateTaskGroupRequest request, CT ct);
    Task<Result<DeleteTaskGroupResponse?>> DeleteTaskGroup(
    DeleteTaskGroupRequest request, CT ct);

    Task<Result<GetTaskGroupResponse?>> GetTaskGroupById(
       GetTaskGroupRequest request, CT ct);
  
    Task<Result<GetTaskGroupsResponse?>> GetTaskGroups(
       GetTaskGroupsRequest request, CT ct);
    Task<Result<CreateUserTaskResponse?>> CreateUserTask(
     CreateUserTaskRequest request, CT ct);
    Task<Result<UpdateUserTaskResponse?>> UpdateUserTask(
       UpdateUserTaskRequest request, CT ct);
    Task<Result<DeleteUserTaskResponse?>> DeleteUserTask(
        DeleteUserTaskRequest request, CT ct);
    Task<Result<GetUserTaskResponse?>> GetUserTaskById(
    GetUserTaskRequest request, CT ct);
    Task<Result<GetUserTasksResponse?>> GetUserTasks(
       GetUserTasksRequest request, CT ct);

}