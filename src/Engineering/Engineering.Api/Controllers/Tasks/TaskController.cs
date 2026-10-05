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
using Engineering.Application.Services.Tasks;

namespace Engineering.Api.Controllers.Tasks;

[ApiController]
[Route("api/engineering/v1/Task")]
public class TaskController : ControllerBase
{
    private readonly ITaskLogic _logic;

    public TaskController(ITaskLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateTaskGroup")]
    [ResponseSchema<CreateTaskGroupResponse>]
    public async Task<IResult> CreateTaskGroup([FromBody] CreateTaskGroupRequest request, CT ct)
    {
        var result = await _logic.CreateTaskGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateTaskGroup")]
    [ResponseSchema<UpdateTaskGroupResponse>]
    public async Task<IResult> UpdateTaskGroup([FromBody] UpdateTaskGroupRequest request, CT ct)
    {
        var result = await _logic.UpdateTaskGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteTaskGroup")]
    [ResponseSchema<DeleteTaskGroupResponse>]
    public async Task<IResult> DeleteTaskGroup([FromQuery] DeleteTaskGroupRequest request, CT ct)
    {
        var result = await _logic.DeleteTaskGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTaskGroupById")]
    [ResponseSchema<GetTaskGroupResponse>]
    public async Task<IResult> GetTaskGroup([FromQuery] GetTaskGroupRequest request, CT ct)
    {
        var result = await _logic.GetTaskGroupById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTaskGroups")]
    [ResponseSchema<GetTaskGroupsResponse>]
    public async Task<IResult> GetTaskGroups([FromQuery] GetTaskGroupsRequest request, CT ct)
    {
        var result = await _logic.GetTaskGroups(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateUserTask")]
    [ResponseSchema<CreateUserTaskResponse>]
    public async Task<IResult> CreateUserTask([FromBody] CreateUserTaskRequest request, CT ct)
    {
        var result = await _logic.CreateUserTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateUserTask")]
    [ResponseSchema<UpdateUserTaskResponse>]
    public async Task<IResult> UpdateUserTask([FromBody] UpdateUserTaskRequest request, CT ct)
    {
        var result = await _logic.UpdateUserTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteUserTask")]
    [ResponseSchema<DeleteUserTaskResponse>]
    public async Task<IResult> DeleteUserTask([FromQuery] DeleteUserTaskRequest request, CT ct)
    {
        var result = await _logic.DeleteUserTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetUserTask")]
    [ResponseSchema<GetUserTaskResponse>]
    public async Task<IResult> GetUserTask([FromQuery] GetUserTaskRequest request, CT ct)
    {
        var result = await _logic.GetUserTaskById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetUserTasks")]
    [ResponseSchema<GetUserTasksResponse>]
    public async Task<IResult> GetUserTasks([FromQuery] GetUserTasksRequest request, CT ct)
    {
        var result = await _logic.GetUserTasks(request, ct);
        return result.GetHttpResponse();
    }
}