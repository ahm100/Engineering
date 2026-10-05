using Engineering.Api.Extensions.Enums;
using Engineering.Application.Services.SubProjects;
using Engineering.Application.Services.SubProjects.Contracts.ChangeSubProjectStatus;
using Engineering.Application.Services.SubProjects.Contracts.CreateSubProject;
using Engineering.Application.Services.SubProjects.Contracts.DeleteSubProject;
using Engineering.Application.Services.SubProjects.Contracts.GetAllowedSubProjectManagers;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjectByCode;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjectById;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjects;
using Engineering.Application.Services.SubProjects.Contracts.UpdateSubProject;
using Engineering.Domain.Entities.Projects.Enums;
using System.ComponentModel;

namespace Engineering.Api.Controllers.SubProjects;

[ApiController]
[Route("api/engineering/v1/Project/SubProject")]
public class SubProjectController : ControllerBase
{
    private readonly ISubProjectLogic _subProjectLogic;
    private readonly ILogger<SubProjectController> _logger;

    public SubProjectController(
        ISubProjectLogic subProjectLogic, ILogger<SubProjectController> logger)
    {
        _subProjectLogic = subProjectLogic;
        _logger = logger;
    }

    [HttpPost("CreateSubProject")]
    [Description("Create SubProject")]
    [ResponseSchema<CreateSubProjectResponse>]
    public async Task<IResult> CreateSubProject(
        [FromBody] CreateSubProjectRequest request, CT ct)
    {
        _logger.LogInformation("CreateSubProject");
        var result = await _subProjectLogic.CreateSubProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateSubProject")]
    [Description("Update SubProject")]
    [ResponseSchema<UpdateSubProjectResponse>]
    public async Task<IResult> UpdateSubProject(
        [FromBody] UpdateSubProjectRequest request, CT ct)
    {
        _logger.LogInformation("UpdateSubProject");
        var result = await _subProjectLogic.UpdateSubProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteSubProject")]
    [Description("Delete SubProject")]
    [ResponseSchema<DeleteSubProjectResponse>]
    public async Task<IResult> DeleteSubProject(
        [FromBody] DeleteSubProjectRequest request, CT ct)
    {
        _logger.LogInformation("DeleteSubProject");
        var result = await _subProjectLogic.DeleteSubProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeSubProjectStatus")]
    [Description("Change SubProject Status")]
    [ResponseSchema<ChangeSubProjectStatusResponse>]
    public async Task<IResult> ChangeSubProjectStatus(
        [FromBody] ChangeSubProjectStatusRequest request, CT ct)
    {
        _logger.LogInformation("ChangeSubProjectStatus");
        var result = await _subProjectLogic.ChangeSubProjectStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSubProjectById")]
    [Description("Get SubProject by Id")]
    [ResponseSchema<GetSubProjectByIdResponse>]
    public async Task<IResult> GetSubProjectById(
        [FromQuery] GetSubProjectByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjectById");
        var result = await _subProjectLogic.GetSubProjectById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSubProjectByCode")]
    [Description("Get SubProject by Code")]
    [ResponseSchema<GetSubProjectByCodeResponse>]
    public async Task<IResult> GetSubProjectByCode(
        [FromQuery] GetSubProjectByCodeRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjectByCode");
        var result = await _subProjectLogic.GetSubProjectByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSubProjects")]
    [Description("Get Filtered SubProjects")]
    [ResponseSchema<GetSubProjectsResponse>]
    public async Task<IResult> GetSubProjects(
        [FromBody] GetSubProjectsRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjects");
        var result = await _subProjectLogic.GetSubProjects(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetAllowedSubProjectManagers")]
    [Description("Get Allowed SubProject Managers")]
    [ResponseSchema<GetAllowedSubProjectManagersResponse>]
    public async Task<IResult> GetAllowedSubProjectManagers(
        [FromBody] GetAllowedSubProjectManagersRequest request, CT ct)
    {
        _logger.LogInformation("GetAllowedSubProjectManagers");
        var result = await _subProjectLogic.GetAllowedSubProjectManagers(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSubProjectTypes")]
    [Description("Get SubProject types")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetSubProjectTypes(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjectTypes");
        var result = EnumExtensions.GetEnums<SubProjectType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetSubProjectStatuses")]
    [Description("Get SubProject statuses")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetSubProjectStatuses(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation("GetSubProjectStatuses");
        var result = EnumExtensions.GetEnums<SubProjectStatus>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }
}
