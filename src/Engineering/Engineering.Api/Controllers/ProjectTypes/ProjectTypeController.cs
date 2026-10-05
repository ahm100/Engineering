using Engineering.Application.Services.ProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.ActiveProjectType;
using Engineering.Application.Services.ProjectTypes.Models.CodeCreator;
using Engineering.Application.Services.ProjectTypes.Models.CreateProjectType;
using Engineering.Application.Services.ProjectTypes.Models.DisableProjectType;
using Engineering.Application.Services.ProjectTypes.Models.GetActiveProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByCode;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeById;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByName;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectType;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelEnum;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;
using Engineering.Application.Services.ProjectTypes.Models.InactiveProjectType;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeGroupDelete;
using Engineering.Application.Services.ProjectTypes.Models.StateChangerProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.UpdateProjectType;

namespace Engineering.Api.Controllers.ProjectTypes;

[ApiController]
[Route("api/engineering/v1/ProjectType")]
public class ProjectTypeController : ControllerBase
{
    private readonly IProjectTypeLogic _logic;

    public ProjectTypeController(IProjectTypeLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddProjectType")]
    [ResponseSchema<CreateProjectTypeResponse>]
    public async Task<IResult> AddProjectType([FromBody] CreateProjectTypeRequest request, CT ct)
    {
        var result = await _logic.CreateProjectType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectTypeCodeCreator")]
    [ResponseSchema<ProjectTypeCodeCreatorResponse>]
    public async Task<IResult> ProjectTypeCodeCreator([FromBody] ProjectTypeCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.ProjectTypeCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectTypeGroupDelete")]
    [ResponseSchema<ProjectTypeGroupDeleteResponse>]
    public async Task<IResult> ProjectTypeGroupDelete([FromBody] ProjectTypeGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.ProjectTypeGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateProjectTypes")]
    [ResponseSchema<StateChangerProjectTypesResponse>]
    public async Task<IResult> ActivateProjectTypes([FromBody] ActivateProjectTypesRequest request, CT ct)
    {
        var result = await _logic.StateChangerProjectTypes(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateProjectTypes")]
    [ResponseSchema<StateChangerProjectTypesResponse>]
    public async Task<IResult> InactivateProjectTypes([FromBody] InactivateProjectTypesRequest request, CT ct)
    {
        var result = await _logic.StateChangerProjectTypes(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectType")]
    [ResponseSchema<UpdateProjectTypeResponse>]
    public async Task<IResult> EditProjectType([FromBody] UpdateProjectTypeRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveProjectType")]
    [ResponseSchema<ActiveProjectTypeResponse>]
    public async Task<IResult> ActiveProjectType([FromBody] ActiveProjectTypeRequest request, CT ct)
    {
        var result = await _logic.ActiveProjectType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveProjectType")]
    [ResponseSchema<InactiveProjectTypeResponse>]
    public async Task<IResult> InactiveProjectType([FromBody] InactiveProjectTypeRequest request, CT ct)
    {
        var result = await _logic.InactiveProjectType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectTypeById")]
    [ResponseSchema<GetProjectTypeByIdResponse>]
    public async Task<IResult> GetProjectTypeById([FromQuery] GetProjectTypeByIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectTypeById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectTypeByCode")]
    [ResponseSchema<GetProjectTypeByCodeResponse>]
    public async Task<IResult> GetProjectTypeByCode([FromQuery] GetProjectTypeByCodeRequest request, CT ct)
    {
        var result = await _logic.GetProjectTypeByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectTypeByName")]
    [ResponseSchema<GetProjectTypeByNameResponse>]
    public async Task<IResult> GetProjectTypeByName([FromQuery] GetProjectTypeByNameRequest request, CT ct)
    {
        var result = await _logic.GetProjectTypeByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveProjectType")]
    [ResponseSchema<GetActiveProjectTypesResponse>]
    public async Task<IResult> GetsActiveProjectType([FromQuery] GetActiveProjectTypesRequest request, CT ct)
    {
        var result = await _logic.GetActiveProjectTypes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectType")]
    [ResponseSchema<GetsProjectTypeResponse>]
    public async Task<IResult> GetsProjectType([FromQuery] GetsProjectTypeRequest request, CT ct)
    {
        var result = await _logic.GetsProjectType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectTypeExcelEnum")]
    [ResponseSchema<GetsProjectTypeExcelEnumResponse>]
    public async Task<IResult> GetsProjectTypeExcelEnum([FromQuery] GetsProjectTypeExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsProjectTypeExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectTypeExcelExporter")]
    [ResponseSchema<GetsProjectTypeExcelExporterResponse>]
    public async Task<IResult> GetsProjectTypeExcelExporter([FromBody] GetsProjectTypeExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsProjectTypeExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableProjectType")]
    [ResponseSchema<DisableProjectTypeResponse>]
    public async Task<IResult> DisableProjectType([FromQuery] DisableProjectTypeRequest request, CT ct)
    {
        var result = await _logic.DisableProjectType(request, ct);
        return result.GetHttpResponse();
    }
}