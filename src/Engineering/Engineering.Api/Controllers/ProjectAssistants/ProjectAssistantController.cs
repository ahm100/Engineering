using Engineering.Application.Services.ProjectAssistants;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.CreateImplementationAssistants;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.DeletemplementationAssistants;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsGetsByProjectId;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.CreateTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.DeleteTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansGetsByProjectId;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/ProjectAssistants")]
public class ProjectAssistantsController : ControllerBase
{
    private readonly IProjectAssistantsLogic _logic;

    public ProjectAssistantsController(IProjectAssistantsLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddImplementationAssistans")]
    [ResponseSchema<CreateImplementationAssistansResponse>]
    public async Task<IResult> AddImplementationAssistans([FromBody] CreateImplementationAssistansRequest request, CT ct)
    {
        var result = await _logic.CreateImplementationAssistant(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddTechnicalAssistans")]
    [ResponseSchema<CreateTechnicalAssistansResponse>]
    public async Task<IResult> AddTechnicalAssistans([FromBody] CreateTechnicalAssistansRequest request, CT ct)
    {
        var result = await _logic.CreateTechnicalAssistant(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("ImplementationAssistansGetsByProjectId")]
    [ResponseSchema<ImplementationAssistansGetsByProjectIdResponse>]
    public async Task<IResult> ImplementationAssistansGetsByProjectId([FromQuery] ImplementationAssistansGetsByProjectIdRequest request, CT ct)
    {
        var result = await _logic.ImplementationAssistantGetsByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("TechnicalAssistansGetsByProjectId")]
    [ResponseSchema<TechnicalAssistansGetsByProjectIdResponse>]
    public async Task<IResult> TechnicalAssistansGetsByProjectId([FromQuery] TechnicalAssistansGetsByProjectIdRequest request, CT ct)
    {
        var result = await _logic.TechnicalAssistantGetsByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteImplementationAssistans")]
    [ResponseSchema<DeleteImplementationAssistansResponse>]
    public async Task<IResult> DeleteImplementationAssistans([FromQuery] DeleteImplementationAssistansRequest request, CT ct)
    {
        var result = await _logic.DeleteImplementationAssistant(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteTechnicalAssistans")]
    [ResponseSchema<DeleteTechnicalAssistansResponse>]
    public async Task<IResult> DeleteTechnicalAssistans([FromQuery] DeleteTechnicalAssistansRequest request, CT ct)
    {
        var result = await _logic.DeleteTechnicalAssistant(request, ct);
        return result.GetHttpResponse();
    }
}