using Engineering.Application.Services.ProjectOperationTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.CreateProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.DeleteProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetCurrentUserTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetFilteredProjectOperationTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetProjectOperationTemporaryDailyById;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetProjectOperationTemporaryDailyStatus;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GroupProjectOperationTemporaryDailyStatusChanger;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyGroupDelete;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyStatusChanger;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.UpdateProjectOperationTemporaryDaily;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/ProjectOperationTemporaryDaily")]
public class ProjectOperationTemporaryDailyController : ControllerBase
{
    private readonly IProjectOperationTemporaryDailyLogic _logic;

    public ProjectOperationTemporaryDailyController(IProjectOperationTemporaryDailyLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateTemporaryDaily")]
    [ResponseSchema<CreateProjectOperationTemporaryDailyResponse>]
    public async Task<IResult> CreateTemporaryDaily(
    [FromBody] CreateProjectOperationTemporaryDailyRequest request,
    CT ct)
    {
        var result = await _logic.CreateProjectOperationTemporaryDaily(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TemporaryDailyGroupDelete")]
    [ResponseSchema<ProjectOperationTemporaryDailyGroupDeleteResponse>]
    public async Task<IResult> TemporaryDailyGroupDelete(
        [FromBody] ProjectOperationTemporaryDailyGroupDeleteRequest request,
        CT ct)
    {
        var result = await _logic.ProjectOperationTemporaryDailyGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateTemporaryDaily")]
    [ResponseSchema<UpdateProjectOperationTemporaryDailyResponse>]
    public async Task<IResult> UpdateTemporaryDaily(
        [FromBody] UpdateProjectOperationTemporaryDailyRequest request,
        CT ct)
    {
        var result = await _logic.UpdateProjectOperationTemporaryDaily(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("GroupTemporaryDailyStatusChanger")]
    [ResponseSchema<GroupProjectOperationTemporaryDailyStatusChangerResponse>]
    public async Task<IResult> GroupTemporaryDailyStatusChanger(
        [FromBody] GroupProjectOperationTemporaryDailyStatusChangerRequest request,
        CT ct)
    {
        var result = await _logic.GroupProjectOperationTemporaryDailyStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("TemporaryDailyStatusChanger")]
    [ResponseSchema<ProjectOperationTemporaryDailyStatusChangerResponse>]
    public async Task<IResult> TemporaryDailyStatusChanger(
        [FromBody] ProjectOperationTemporaryDailyStatusChangerRequest request,
        CT ct)
    {
        var result = await _logic.ProjectOperationTemporaryDailyStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredTemporaryDailies")]
    [ResponseSchema<GetFilteredProjectOperationTemporaryDailiesResponse>]
    public async Task<IResult> GetFilteredTemporaryDailies(
        [FromBody] GetFilteredProjectOperationTemporaryDailiesRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredProjectOperationTemporaryDailies(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetCurrentUserTemporaryDailies")]
    [ResponseSchema<GetCurrentUserTemporaryDailiesResponse>]
    public async Task<IResult> GetCurrentUserTemporaryDailies(
        [FromBody] GetCurrentUserTemporaryDailiesRequest request,
        CT ct)
    {
        var result = await _logic.GetCurrentUserTemporaryDailies(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTemporaryDailyById")]
    [ResponseSchema<GetProjectOperationTemporaryDailyByIdResponse>]
    public async Task<IResult> GetTemporaryDailyById(
        [FromQuery] GetProjectOperationTemporaryDailyByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectOperationTemporaryDailyById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTemporaryDailyStatus")]
    [ResponseSchema<GetProjectOperationTemporaryDailyStatusResponse>]
    public async Task<IResult> GetTemporaryDailyStatus(
        [FromQuery] GetProjectOperationTemporaryDailyStatusRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectOperationTemporaryDailyStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteTemporaryDaily")]
    [ResponseSchema<DeleteProjectOperationTemporaryDailyResponse>]
    public async Task<IResult> DeleteTemporaryDaily(
        [FromQuery] DeleteProjectOperationTemporaryDailyRequest request,
        CT ct)
    {
        var result = await _logic.DeleteProjectOperationTemporaryDaily(request, ct);
        return result.GetHttpResponse();
    }
}