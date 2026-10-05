using Engineering.Application.Services.ProjectServices;
using Engineering.Application.Services.ProjectServices.Models.ActiveProjectService;
using Engineering.Application.Services.ProjectServices.Models.CreateProjectService;
using Engineering.Application.Services.ProjectServices.Models.DeleteProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetActiveProjectServices;
using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;
using Engineering.Application.Services.ProjectServices.Models.GetsContractorProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;
using Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;
using Engineering.Application.Services.ProjectServices.Models.InactiveProjectService;
using Engineering.Application.Services.ProjectServices.Models.StateChangerProjectServices;
using Engineering.Application.Services.ProjectServices.Models.UpdateProjectService;

namespace Engineering.Api.Controllers.ProjectServices;

[ApiController]
[Route("api/engineering/v1/ProjectService")]
public class ProjectServiceController : ControllerBase
{
    private readonly IProjectServiceLogic _logic;

    public ProjectServiceController(IProjectServiceLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateProjectService")]
    [ResponseSchema<CreateProjectServiceResponse>]
    public async Task<IResult> CreateProjectService([FromBody] CreateProjectServiceRequest request, CT ct)
    {
        var result = await _logic.CreateProjectService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateProjectService")]
    [ResponseSchema<UpdateProjectServiceResponse>]
    public async Task<IResult> UpdateProjectService([FromBody] UpdateProjectServiceRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateProjectServices")]
    [ResponseSchema<StateChangerProjectServicesResponse>]
    public async Task<IResult> ActivateProjectServices([FromBody] ActivateProjectServicesRequest request, CT ct)
    {
        var result = await _logic.StateChangerProjectServices(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateProjectServices")]
    [ResponseSchema<StateChangerProjectServicesResponse>]
    public async Task<IResult> InactivateProjectServices([FromBody] InactivateProjectServicesRequest request, CT ct)
    {
        var result = await _logic.StateChangerProjectServices(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveProjectService")]
    [ResponseSchema<ActiveProjectServiceResponse>]
    public async Task<IResult> ActiveProjectService([FromBody] ActiveProjectServiceRequest request, CT ct)
    {
        var result = await _logic.ActiveProjectService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveProjectService")]
    [ResponseSchema<InactiveProjectServiceResponse>]
    public async Task<IResult> InactiveProjectService([FromBody] InactiveProjectServiceRequest request, CT ct)
    {
        var result = await _logic.InactiveProjectService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectServiceById")]
    [ResponseSchema<GetProjectServiceByIdResponse>]
    public async Task<IResult> GetProjectServiceById([FromQuery] GetProjectServiceByIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectServiceById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorProjectService")]
    [ResponseSchema<GetsContractorProjectServiceResponse>]
    public async Task<IResult> GetsContractorProjectService([FromQuery] GetsContractorProjectServiceRequest request, CT ct)
    {
        var result = await _logic.GetsContractorProjectService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveProjectService")]
    [ResponseSchema<GetActiveProjectServicesResponse>]
    public async Task<IResult> GetsActiveProjectService([FromBody] GetActiveProjectServicesRequest request, CT ct)
    {
        var result = await _logic.GetActiveProjectServices(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectService")]
    [ResponseSchema<GetsProjectServiceResponse>]
    public async Task<IResult> GetsProjectService([FromBody] GetsProjectServiceRequest request, CT ct)
    {
        var result = await _logic.GetsProjectService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsServiceInfoByProjectId")]
    [ResponseSchema<GetsServiceInfoByProjectIdResponse>]
    public async Task<IResult> GetsServiceInfoByProjectId([FromBody] GetsServiceInfoByProjectIdRequest request, CT ct)
    {
        var result = await _logic.GetsServiceInfoByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectServiceDetail")]
    [ResponseSchema<GetsProjectServiceDetailResponse>]
    public async Task<IResult> GetsProjectServiceDetail([FromBody] GetsProjectServiceDetailRequest request, CT ct)
    {
        var result = await _logic.GetsProjectServiceDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProjectService")]
    [ResponseSchema<DeleteProjectServiceResponse>]
    public async Task<IResult> DeleteProjectService([FromBody] DeleteProjectServiceRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectService(request, ct);
        return result.GetHttpResponse();
    }
}