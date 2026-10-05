using Engineering.Api.Controllers.WbsTemplates.Contracts.GetFltrWbsTemplate;
using Engineering.Api.Extensions.Enums;
using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.WbsTemplates;
using Engineering.Application.Services.WbsTemplates.Contracts.CreateWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.DeleteWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;
using Engineering.Application.Services.WbsTemplates.Contracts.UpdateWbsTemplate;


namespace Engineering.Api.Controllers.WbsTemplates;


[ApiController]
[Route("api/engineering/v1/wbsTemplate")]
public class WbsTemplateController : ControllerBase
{
    private readonly IWbsTemplateLogic _logic;

    public WbsTemplateController(IWbsTemplateLogic logic)
    {
        _logic = logic;
    }

    [HttpPost()]
    [ResponseSchema<CreateWbsTemplateResponse>]
    public async Task<IResult> CreateWbsTemplate(
    [FromBody] CreateWbsTemplateRequest request,
    CT ct)
    {
        var result = await _logic.CreateWbsTemplate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut()]
    [ResponseSchema<UpdateWbsTemplateResponse>]
    public async Task<IResult> UpdateWbsTemplate(
    [FromBody] UpdateWbsTemplateRequest request,
    CT ct)
    {
        var result = await _logic.UpdateWbsTemplate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("{id}")]
    [ResponseSchema<DeleteWbsTemplateResponse>]
    public async Task<IResult> DeleteWbsTemplate(
        [FromRoute] long id, CT ct)
    {
        var result = await _logic.DeleteWbsTemplate(new(id), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("{id}")]
    [ResponseSchema<GetWbsTemplateByIdResponse>]
    public async Task<IResult> GetWbsTemplateById(
    [FromRoute] long id,
    CT ct)
    {
        var result = await _logic.GetWbsTemplateById(new(id), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("fltr")]
    [ResponseSchema<GetFltrWbsTemplateResponse>]
    public async Task<IResult> GetFltrWbsTemplate(
    [FromBody] GetFltrWbsTemplateRequest request,
    CT ct)
    {
        var result = await _logic.GetFltrWbsTemplate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("fltr-excel-columns")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetFltrWbsTemplateEnum(
    [FromBody] GetEnumsRequest request,
    CT ct)
    {
        var result = EnumExtensions.GetEnums<WbsTemplateEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("fltr-excel-export")]
    [ResponseSchema<GetFltrWbsTemplateResponse>]
    public async Task<IResult> GetFltrWbsTemplateToExcel(
        [FromBody] GetFltrWbsTemplateToExcelRequest request, CT ct)
    {
        var response = await _logic.GetFltrWbsTemplate(request.Adapt<GetFltrWbsTemplateRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetFltrWbsTemplateToExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, "WbsTemplate"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"WbsTemplate-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetFltrWbsTemplateToExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetWbsTemplateForProject")]
    [ResponseSchema<GetWbsTemplateForProjectResponse>]
    public async Task<IResult> GetWbsTemplateForProject(
    [FromBody] GetWbsTemplateForProjectRequest request,
    CT ct)
    {
        var result = await _logic.GetWbsTemplateForProject(request, ct);
        return result.GetHttpResponse();
    }
}