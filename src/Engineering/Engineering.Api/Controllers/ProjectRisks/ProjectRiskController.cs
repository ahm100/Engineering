using Engineering.Api.Extensions.Enums;
using Engineering.Application.Services.ProjectRisks;
using Engineering.Application.Services.ProjectRisks.Contracts.CreateProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.DeleteProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;
using Engineering.Application.Services.ProjectRisks.Contracts.UpdateProjectRisk;
using Engineering.Domain.Entities.Projects.Enums;
using System.ComponentModel;

namespace Engineering.Api.Controllers.ProjectRisks;

[ApiController]
[Route("api/engineering/v1/ProjectRisk")]
public class ProjectRiskController : ControllerBase
{
    private readonly IProjectRiskLogic _logic;
    private readonly ILogger<ProjectRiskController> _logger;
    public ProjectRiskController(IProjectRiskLogic logic,
        ILogger<ProjectRiskController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpPost("CreateProjectRisk")]
    [ResponseSchema<CreateProjectRiskResponse>]
    public async Task<IResult> CreateProjectRisk(
        [FromBody] CreateProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("CreateProjectRisk");
        var result = await _logic.CreateProjectRisk(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateProjectRisk")]
    [ResponseSchema<UpdateProjectRiskResponse>]
    public async Task<IResult> UpdateProjectRisk(
        [FromBody] UpdateProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("UpdateProjectRisk");
        var result = await _logic.UpdateProjectRisk(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProjectRisk")]
    [ResponseSchema<DeleteProjectRiskResponse>]
    public async Task<IResult> DeleteProjectRisk(
        [FromQuery] DeleteProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("DeleteProjectRisk");
        var result = await _logic.DeleteProjectRisk(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectRiskById")]
    [ResponseSchema<GetProjectRiskByIdResponse>]
    public async Task<IResult> GetProjectRiskById(
        [FromQuery] GetProjectRiskByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectRiskById");
        var result = await _logic.GetProjectRiskById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectRiskByProjectId")]
    [ResponseSchema<GetProjectRiskByProjectIdResponse>]
    public async Task<IResult> GetProjectRiskByProjectId(
        [FromQuery] GetProjectRiskByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectRiskByProjectId");
        var result = await _logic.GetProjectRiskByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrProjectRisk")]
    [ResponseSchema<GetFltrProjectRiskResponse>]
    public async Task<IResult> GetFltrProjectRisk(
        [FromBody] GetFltrProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrProjectRisk");
        var result = await _logic.GetFltrProjectRisk(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("risk-probability")]
    [Description("Get Risk Probability.")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetRiskProbability(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetRiskProbability");
        var result = EnumExtensions.GetEnums<RiskProbability>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("risk-impact")]
    [Description("Get Risk Impact.")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetRiskImpact(
        [FromBody] GetEnumsRequest request, CT ct)
    {

        _logger.LogInformation($"GetRiskImpact");
        var result = EnumExtensions.GetEnums<RiskImpact>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("risk-status")]
    [Description("Get Risk Status.")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetRiskStatus(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetRiskStatus");
        var result = EnumExtensions.GetEnums<RiskStatus>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }
}