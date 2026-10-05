using Engineering.Application.Services.ProjectOperationDetailSchedulings;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperations;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDate;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDay;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDetailScheduling")]
public class ProjectOperationDetailSchedulingController : ControllerBase
{
    private readonly IProjectOperationDetailSchedulingLogic _logic;

    public ProjectOperationDetailSchedulingController(IProjectOperationDetailSchedulingLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("GetFilteredOperationLocations")]
    [ResponseSchema<GetSchedulingOperationLocationsResponse>]
    public async Task<IResult> GetFilteredOperationLocations(
    [FromBody] GetSchedulingOperationLocationsRequest request,
    CT ct)
    {
        var result = await _logic.GetSchedulingOperationLocationsAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredOperationInfos")]
    [ResponseSchema<GetSchedulingOperationInfosResponse>]
    public async Task<IResult> GetFilteredOperationInfos(
        [FromBody] GetSchedulingOperationInfosRequest request,
        CT ct)
    {
        var result = await _logic.GetSchedulingOperationInfosAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetByDay")]
    [ResponseSchema<UpdateProjectOperationDetailDatesByDayResponse>]
    public async Task<IResult> SetByDay(
        [FromBody] UpdateProjectOperationDetailDatesByDayRequest request,
        CT ct)
    {
        var result = await _logic.UpdateProjectOperationDetailDatesByDayAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetByDate")]
    [ResponseSchema<UpdateProjectOperationDetailDatesByDateResponse>]
    public async Task<IResult> SetByDate(
        [FromBody] UpdateProjectOperationDetailDatesByDateRequest request,
        CT ct)
    {
        var result = await _logic.UpdateProjectOperationDetailDatesByDateAsync(request, ct);
        return result.GetHttpResponse();
    }
}