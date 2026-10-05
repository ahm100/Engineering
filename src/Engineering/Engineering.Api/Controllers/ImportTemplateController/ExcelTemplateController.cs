namespace Engineering.Api.Controllers.ImportTemplateController;

[Route("api/engineering/v1/[controller]")]
public class ExcelTemplateController : ControllerBase
{
    private readonly ILogger<ExcelTemplateController> _logger;
    private readonly TemplateService _templateService;

    public ExcelTemplateController(
        ILogger<ExcelTemplateController> logger,
        TemplateService templateService) : base()
    {
        _logger = logger;
        _templateService = templateService;
    }

    [HttpGet("PRGSImportTemplate")]
    public async Task<IResult> PRGSImportTemplate(CT ct)
    {
        _logger.LogInformation("PRGSImportTemplate");
        var result = await _templateService
            .GetTemplateFileBase64("PRGSImportTemplateFile");
        return result.GetHttpResponse();
    }

    [HttpGet("RGSImportTemplate")]
    public async Task<IResult> RGSImportTemplate(CT ct)
    {
        _logger.LogInformation("RGSImportTemplate");
        var result = await _templateService
            .GetTemplateFileBase64("RGSImportTemplateFile");
        return result.GetHttpResponse();
    }

    [HttpGet("PODetailImportTemplate")]
    public async Task<IResult> PODetailImportTemplate(CT ct)
    {
        _logger.LogInformation("PODetailImportTemplate");
        var result = await _templateService
            .GetTemplateFileBase64("PODetailImportTemplateFile");
        return result.GetHttpResponse();
    }

    [HttpGet("OInfoImportTemplate")]
    public async Task<IResult> OInfoImportTemplate(CT ct)
    {
        _logger.LogInformation("OInfoImportTemplate");
        var result = await _templateService
            .GetTemplateFileBase64("OInfoImportTemplateFile");
        return result.GetHttpResponse();
    }

    [HttpGet("POImportTemplate")]
    public async Task<IResult> POImportTemplate(CT ct)
    {
        _logger.LogInformation("POImportTemplate");
        var result = await _templateService
            .GetTemplateFileBase64("POImportTemplateFile");
        return result.GetHttpResponse();
    }

    [HttpGet("ImportItemForRGSTypeTemplateFile")]
    public async Task<IResult> ImportItemForRGSTypeTemplateFile(CT ct)
    {
        _logger.LogInformation("ImportItemForRGSTypeTemplateFile");
        var result = await _templateService
            .GetTemplateFileBase64("ImportItemForRGSTypeTemplateFile");
        return result.GetHttpResponse();
    }
}