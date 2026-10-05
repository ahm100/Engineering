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
using Engineering.Domain.Entities.Tasks.Enums;
using Engineering.Domain.Errors.Tasks;
using TaskGroup = Engineering.Domain.Entities.Tasks.TaskGroup;
using UserTaskEntity = Engineering.Domain.Entities.Tasks.UserTask;


namespace Engineering.Application.Services.Tasks;

public partial class TaskLogic
{

    public async Task<Result<UserTaskEntity?>> CreateUserTaskCommand(
        CreateUserTaskRequest request,
        CT ct)
    {
        try
        {
            var group = await _taskGroupRepository.GetById(
                request.TaskGroupId, ct);
            if (group is null)
                return Result.Failure<UserTaskEntity>(
                    TaskErrors.TaskGroupNotFound);
            var entity = new UserTaskEntity(
                request.Title,
                request.Description,
                request.TaskGroupId,
                TaskStatusEnum.ToDo,
                request.OwnerId);
            await _userTaskRepository.Create(entity, ct);
            await _unitOfWork.CommitAsync(ct);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Result.Failure<UserTaskEntity>(
                SharedErrors.UnknownError);
        }
    }

    public async Task<Result<CreateTaskGroupResponse?>> CreateTaskGroupCommand(
        CreateTaskGroupRequest request, CT ct)
    {
        try
        {
            var entity = new TaskGroup(request.Title, request.Description);
            await _taskGroupRepository.Create(entity, ct);
            await _unitOfWork.CommitAsync(ct);
            return new CreateTaskGroupResponse(entity.Id, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Result.Failure<CreateTaskGroupResponse>(
                SharedErrors.UnknownError);
        }
    }


    public async Task<Result<UpdateTaskGroupResponse?>> UpdateTaskGroupCommand(
      UpdateTaskGroupRequest request, CT ct)
    {
        try
        {
            var group = await _taskGroupRepository.GetByIdForUpdate(request.Id, ct);
            if (group is null)
                return Result.Failure<UpdateTaskGroupResponse>(TaskErrors.TaskGroupNotFound);
            group.SetTitle(request.Title);
            group.SetDescription(request.Description);
            await _taskGroupRepository.Update(group);
            await _unitOfWork.CommitAsync(ct);
            return new UpdateTaskGroupResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateTaskGroupResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetTaskGroupResponse?>> GetTaskGroupByIdCommand(
     GetTaskGroupRequest request, CT ct)
    {
        try
        {
            var result = await _taskGroupRepository.GetById(request.Id, ct);
            if (result is null)
                return Result.Failure<GetTaskGroupResponse>(TaskErrors.TaskNotFound);
          
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetTaskGroupResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<UpdateUserTaskResponse?>> UpdateUserTaskCommand(
     UpdateUserTaskRequest request, CT ct)
    {
        try
        {
            var entity = await _userTaskRepository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<UpdateUserTaskResponse?>(TaskErrors.TaskNotFound);
            var group = await _taskGroupRepository.GetById(request.TaskGroupId, ct);
            if (group is null)
                return Result.Failure<UpdateUserTaskResponse?>(TaskErrors.TaskGroupNotFound);

            entity.Update(request.Title, request.Description, request.TaskGroupId);

            if (request.Status.HasValue)
            {
                entity.SetStatus(request.Status.Value);
            }

            await _userTaskRepository.Update(entity);
            await _unitOfWork.CommitAsync(ct);

            return new UpdateUserTaskResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateUserTaskResponse?>(SharedErrors.UnknownError);
        }
    }


    public async Task<Result<DeleteUserTaskResponse?>> DeleteUserTaskCommand(
        DeleteUserTaskRequest request, CT ct)
    {

        try
        {
            var entity = await _userTaskRepository.GetById(
                request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteUserTaskResponse>(
                    TaskErrors.TaskNotFound);
            entity.SoftDelete();
            await _userTaskRepository.Update(entity);
            await _unitOfWork.CommitAsync(ct);
            return new DeleteUserTaskResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Result.Failure<DeleteUserTaskResponse>(
                SharedErrors.UnknownError);
        }
    }


    public async Task<Result<DeleteTaskGroupResponse?>> DeleteTaskGroupCommand(DeleteTaskGroupRequest request, CT ct)
    {
        try
        {
            var entity = await _taskGroupRepository.GetByIdForUpdate(request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteTaskGroupResponse>(TaskErrors.TaskNotFound);
            entity.SoftDelete();
            await _taskGroupRepository.Update(entity);
            await _unitOfWork.CommitAsync(ct);
            return new DeleteTaskGroupResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteTaskGroupResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetUserTaskResponse?>> GetUserTaskByIdCommand(
     GetUserTaskRequest request, CT ct)
    {
        try
        {
            var result = await _userTaskRepository.GetByIdForApi(request.Id, ct);
            var userId = _userInfoProv.UserId;
            if (result is null)
                return Result.Failure<GetUserTaskResponse>(TaskErrors.TaskNotFound);
            if (result?.CreatorId != userId)
                return Result.Failure<GetUserTaskResponse>(TaskErrors.NotHaveAccess);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetUserTaskResponse>(SharedErrors.UnknownError);
        }
    }



    public async Task<Result<GetUserTasksResponse?>> GetUserTasksCommand(
     GetUserTasksRequest request, CT ct)
    {
        try
        {
            var userId = _userInfoProv.UserId;
            request = request with
            {
                OwnerUserId = request.OwnerUserId ?? userId,
                PageIndex = request.PageIndex ?? 1,
                PageSize = request.PageSize ?? 20
            };
            var result = await _userTaskRepository.GetUserTasks(request, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetUserTasksResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetTaskGroupsResponse?>> GetTaskGroupCommand(
     GetTaskGroupsRequest request, CT ct)
    {
        try
        {
            var pageIndex = request.PageIndex ?? 1;
            var pageSize = request.PageSize ?? 20;
            
            var result = await _taskGroupRepository.GetTaskGroups(pageIndex, pageSize, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetTaskGroupsResponse>(SharedErrors.UnknownError);
        }
    }



}

