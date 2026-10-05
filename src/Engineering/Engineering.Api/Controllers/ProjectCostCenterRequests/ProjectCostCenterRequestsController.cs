using Engineering.Application.Services.ProjectCostCenterRequests;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.DeleteProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequestById;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequests;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.RejectProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.SubmitCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.UpdateProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Models;
using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;

namespace Engineering.Api.Controllers.ProjectCostCenterRequests;

[ApiController]
[Route("api/engineering/v1/ProjectCostCenterRequests")]
public class ProjectCostCenterRequestsController : ControllerBase
{
    private readonly IProjectCostCenterRequestLogic _logic;

    public ProjectCostCenterRequestsController(IProjectCostCenterRequestLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("SubmitCostCenterRequest")]
    [ResponseSchema<SubmitCostCenterRequestResponse>]
    public async Task<IResult> SubmitCostCenterRequest([FromBody] SubmitCostCenterRequestRequest request, CT ct)
    {
        var result = await _logic.SubmitCostCenterRequest(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectCostCenterRequests")]
    [ResponseSchema<GetProjectCostCenterRequestsResponse>]
    public async Task<IResult> GetProjectCostCenterRequests([FromBody] GetProjectCostCenterRequestsRequest request, CT ct)
    {
        var result = await _logic.GetProjectCostCenterRequests(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectCostCenterRequestById")]
    [ResponseSchema<ProjectCostCenterRequestModel>]
    public async Task<IResult> GetProjectCostCenterRequestById([FromBody] GetProjectCostCenterRequestByIdRequest request, CT ct)
    => (await _logic.GetProjectCostCenterRequestById(request, ct)).GetHttpResponse();

    [HttpPut("UpdateProjectCostCenterRequest")]
    [ResponseSchema<UpdateProjectCostCenterRequestResponse>]
    public async Task<IResult> UpdateProjectCostCenterRequest([FromBody] UpdateProjectCostCenterRequestRequest request, CT ct)
        => (await _logic.UpdateProjectCostCenterRequest(request, ct)).GetHttpResponse();

    [HttpDelete("DeleteProjectCostCenterRequest")]
    [ResponseSchema<DeleteProjectCostCenterRequestResponse>]
    public async Task<IResult> DeleteProjectCostCenterRequest([FromQuery] DeleteProjectCostCenterRequestRequest request, CT ct)
        => (await _logic.DeleteProjectCostCenterRequest(request, ct)).GetHttpResponse();

    [HttpPost("RejectProjectCostCenterRequest")]
    [ResponseSchema<RejectProjectCostCenterRequestResponse>]
    public async Task<IResult> RejectProjectCostCenterRequest([FromBody] RejectProjectCostCenterRequestRequest request, CT ct)
        => (await _logic.RejectProjectCostCenterRequest(request, ct)).GetHttpResponse();

    [HttpGet("GetProjectCostCenterRequestStatuses")]
    [ResponseSchema<GetsProjectStatusResponse>]
    public async Task<IResult> GetProjectCostCenterRequestStatuses(CT ct)
        => (await _logic.GetProjectCostCenterRequestStatuses(ct)).GetHttpResponse();
}