using Engineering.Application.Services.EngineeringDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;
using Engineering.Application.Services.EngineeringDocs.Contracts.DeleteProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDiscipline;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.ProjectDocCodeGenerator;
using Engineering.Application.Services.EngineeringDocs.Contracts.SeedeDocTypes;
using Engineering.Application.Services.EngineeringDocs.Contracts.UpdateProjectDoc;
using System.ComponentModel;

namespace Engineering.Api.Controllers.EngineeringDocs;

[ApiController]
[Route("api/engineering/v1/EngineeringDocs")]
public class EngineeringDocsController : ControllerBase
{
    private readonly IEngineeringDocLogic _logic;
    private readonly ILogger<EngineeringDocsController> _logger;
    public EngineeringDocsController(
        IEngineeringDocLogic logic,
        ILogger<EngineeringDocsController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpPost("SeedDocTypes")]
    [Description("Seed engineering document types")]
    [ResponseSchema<SeedDocTypeResponse>]
    public async Task<IResult> SeedDocTypes(CT ct)
    {
        _logger.LogInformation("SeedDocTypes");
        var result = await _logic.DocTypeSeeder(ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDiscipline")]
    [Description("Get engineering disciplines")]
    [ResponseSchema<GetDisciplineResponse>]
    public async Task<IResult> GetDiscipline(
        [FromBody] GetDisciplineRequest request, CT ct)
    {
        _logger.LogInformation("GetDiscipline");
        var result = await _logic.GetDiscipline(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDisciplineDoc")]
    [Description("Get engineering discipline documents")]
    [ResponseSchema<GetDisciplineDocResponse>]
    public async Task<IResult> GetDisciplineDoc(
        [FromBody] GetDisciplineDocRequest request, CT ct)
    {
        _logger.LogInformation("GetDisciplineDoc");
        var result = await _logic.GetDisciplineDoc(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDisciplineDocType")]
    [Description("Get allowed document types for each discipline(engineering Field)")]
    [ResponseSchema<GetDisciplineDocTypeResponse>]
    public async Task<IResult> GetDisciplineDocType(
        [FromBody] GetDisciplineDocTypeRequest request, CT ct)
    {
        _logger.LogInformation("GetDisciplineDocType");
        var result = await _logic.GetDisciplineDocType(request, ct);
        return result.GetHttpResponse();
    }

    ////////////////////// ProjectDocs

    [HttpPost("CreateProjectDoc")]
    [Description("Create engineering project document")]
    [ResponseSchema<CreateProjectDocResponse>]
    public async Task<IResult> CreateProjectDoc(
        [FromBody] CreateProjectDocRequest request, CT ct)
    {
        _logger.LogInformation("CreateProjectDoc");
        var result = await _logic.CreateProjectDoc(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateProjectDoc")]
    [Description("Update engineering project document")]
    [ResponseSchema<UpdateProjectDocResponse>]
    public async Task<IResult> UpdateProjectDoc(
        [FromBody] UpdateProjectDocRequest request, CT ct)
    {
        _logger.LogInformation("UpdateProjectDoc");
        var result = await _logic.UpdateProjectDoc(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ProjectDocCodeGenerator")]
    [Description("Update engineering project document")]
    [ResponseSchema<ProjectDocCodeGeneratorResponse>]
    public async Task<IResult> ProjectDocCodeGenerator(
        [FromBody] ProjectDocCodeGeneratorRequest request, CT ct)
    {
        _logger.LogInformation("ProjectDocCodeGenerator");
        var result = await _logic.ProjectDocCodeGenerator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProjectDoc")]
    [Description("Delete engineering project document")]
    [ResponseSchema<DeleteProjectDocResponse>]
    public async Task<IResult> DeleteProjectDoc(
        [FromBody] DeleteProjectDocRequest request, CT ct)
    {
        _logger.LogInformation("DeleteProjectDoc");
        var result = await _logic.DeleteProjectDoc(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectDocById")]
    [Description("Get engineering project document by id")]
    [ResponseSchema<GetProjectDocByIdResponse>]
    public async Task<IResult> GetProjectDocById(
        [FromQuery] GetProjectDocByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectDocById");
        var result = await _logic.GetProjectDocById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectDocs")]
    [Description("Get filtered engineering project documents")]
    [ResponseSchema<GetProjectDocsResponse>]
    public async Task<IResult> GetProjectDocs(
        [FromBody] GetProjectDocsRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectDocs");
        var result = await _logic.GetProjectDocs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectDocHistories")]
    [Description("Get project document histories")]
    [ResponseSchema<GetProjectDocHistoriesResponse>]
    public async Task<IResult> GetProjectDocHistories(
    [FromBody] GetProjectDocHistoriesRequest request,
    CT ct)
    {
        _logger.LogInformation("GetProjectDocHistories");

        var result = await _logic.GetProjectDocHistories(request, ct);

        return result.GetHttpResponse();
    }

}
