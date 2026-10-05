using Engineering.Application.Abstractions.Data.Tasks;
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
using Engineering.Domain.Errors.Tasks;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.Tasks;

public partial class TaskLogic : ITaskLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaskLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoProvider _userInfoProv;
    private readonly IUserInfoService _userInfoService;
    private readonly ITaskGroupRepository _taskGroupRepository;
    private readonly IUserTaskRepository _userTaskRepository;

    public TaskLogic(IMediator mediator,
        ILogger<TaskLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoProvider userInfoProv,
        IUserInfoService userInfoService,
        ITaskGroupRepository taskGroupRepository,
        IUserTaskRepository userTaskRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoProv = userInfoProv;
        _userInfoService = userInfoService;
        _taskGroupRepository = taskGroupRepository;
        _userTaskRepository = userTaskRepository;
    }

    public async Task<Result<CreateTaskGroupResponse?>> CreateTaskGroup(
        CreateTaskGroupRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for CreateTaskGroup, Title:{Title}",
            request.Title);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateTaskGroupResponse>(GlobalErrors.InvalidCompany);
        var validation = await request.IsValidAsync<
            CreateTaskGroupValidator,
            CreateTaskGroupRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<CreateTaskGroupResponse>(
                validation.Error!);
        var result = await CreateTaskGroupCommand(request, ct);
        if (result.IsBad()) return result.Failure<CreateTaskGroupResponse>()!;
        return result;

    }

    public async Task<Result<UpdateTaskGroupResponse?>> UpdateTaskGroup(
      UpdateTaskGroupRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for UpdateUserTask, Id:{Id}",
            request.Id);
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateTaskGroupResponse>(GlobalErrors.InvalidCompany);

        var validation = await request.IsValidAsync<UpdateTaskGroupValidator, UpdateTaskGroupRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<UpdateTaskGroupResponse>(
                validation.Error!);
        var result = await UpdateTaskGroupCommand(request, ct);
        await _unitOfWork.CommitAsync(ct);
        if (result.IsBad()) return result.Failure<UpdateTaskGroupResponse>()!;
        return result;

    }


    public async Task<Result<DeleteTaskGroupResponse?>> DeleteTaskGroup(
        DeleteTaskGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for Delete Task Group, Id:{Id}", request.Id);
        var validation = await request.IsValidAsync<DeleteTaskGroupValidator, DeleteTaskGroupRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<DeleteTaskGroupResponse>(validation.Error!);
        var userId = _userInfoProv.UserId;
        var forDeleteGroup = await _taskGroupRepository.GetByIdForUpdate(request.Id, ct);
        if (forDeleteGroup?.CreatorId != userId)
            return Result.Failure<DeleteTaskGroupResponse>(TaskErrors.NotHaveAccess);
        if (forDeleteGroup?.UserTasks?.Count > 0)
            return Result.Failure<DeleteTaskGroupResponse>(TaskErrors.TaskGroupHasTasks);
        var result = await DeleteTaskGroupCommand(request, ct);
        if (result.IsBad()) return result.Failure<DeleteTaskGroupResponse>()!;
        return result;
    }

    public async Task<Result<GetTaskGroupResponse?>> GetTaskGroupById(
        GetTaskGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTaskGroup, Id:{Id}", request.Id);
        var validation = await request.IsValidAsync<GetTaskGroupValidator, GetTaskGroupRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetTaskGroupResponse>(
                validation.Error!);
       
        var result = await GetTaskGroupByIdCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetTaskGroupResponse>()!;
        return result;

    }

    public async Task<Result<GetTaskGroupsResponse?>> GetTaskGroups(
        GetTaskGroupsRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for GetTaskGroups, PageIndex:{PageIndex}, PageSize:{PageSize}",
            request.PageIndex,
            request.PageSize);

        var result = await GetTaskGroupCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetTaskGroupsResponse>()!;
        return result;

    }

    public async Task<Result<CreateUserTaskResponse?>> CreateUserTask(
        CreateUserTaskRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for CreateUserTask, Title:{Title}",
            request.Title);
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateUserTaskResponse>(GlobalErrors.InvalidCompany);
        var validation = await request.IsValidAsync<
            CreateUserTaskValidator,
            CreateUserTaskRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<CreateUserTaskResponse>(
                validation.Error!);
        var result = await CreateUserTaskCommand(request, ct);
        if (result.IsBad()) return result.Failure<CreateUserTaskResponse>()!;
        return new CreateUserTaskResponse(result.Value!.Id, true);

    }

    public async Task<Result<UpdateUserTaskResponse?>> UpdateUserTask(
        UpdateUserTaskRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for UpdateUserTask, Id:{Id}",
            request.Id);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateUserTaskResponse>(GlobalErrors.InvalidCompany);

        var validation = await request.IsValidAsync<
           UpdateUserTaskValidator,
           UpdateUserTaskRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<UpdateUserTaskResponse>(
                validation.Error!);

        var result = await UpdateUserTaskCommand(request, ct);
        if (result.IsBad()) return result.Failure<UpdateUserTaskResponse>()!;
        await _unitOfWork.CommitAsync(ct);
        return new UpdateUserTaskResponse(true);

    }

    public async Task<Result<DeleteUserTaskResponse?>> DeleteUserTask(
        DeleteUserTaskRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteUserTask, Id:{Id}", request.Id);

        var validation = await request.IsValidAsync<DeleteUserTaskValidator, DeleteUserTaskRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<DeleteUserTaskResponse>(
                validation.Error!);
        var userId = _userInfoProv.UserId;
        var forDeleteUser = await _userTaskRepository.GetById(request.Id, ct);
        if (forDeleteUser?.CreatorId != userId)
            return Result.Failure<DeleteUserTaskResponse>(TaskErrors.NotHaveAccess);
        var result = await DeleteUserTaskCommand(request, ct);
        if (result.IsBad()) return result.Failure<DeleteUserTaskResponse>()!;
        return result;
    }

    public async Task<Result<GetUserTaskResponse?>> GetUserTaskById(
    GetUserTaskRequest request, CT ct)
    {
        _logger.LogInformation("GetUserTaskById");
        var result = await GetUserTaskByIdCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetUserTaskResponse>()!;
        return result;
    }

    public async Task<Result<GetUserTasksResponse?>> GetUserTasks(
        GetUserTasksRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetUserTasks, PageIndex:{PageIndex}, PageSize:{PageSize}",
            request.PageIndex,
            request.PageSize);

        var result = await GetUserTasksCommand(request, ct);
        if (result.IsBad()) 
            return result.Failure<GetUserTasksResponse>()!;
        return result;
    }


}