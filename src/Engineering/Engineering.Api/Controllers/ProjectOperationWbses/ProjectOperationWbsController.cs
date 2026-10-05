using Engineering.Api.Controllers.ProjectOperationWbses.Contracts.GetFltrPOWbs;
using Engineering.Api.Extensions.Enums;
using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.ProjectOperationWbses;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.CreateProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.DeleteProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.UpdateProjectOperationWbs;

namespace Engineering.Api.Controllers.ProjectOperationWbses;

[ApiController]
[Route("api/engineering/v1/ProjectOperationWbs")]
public class ProjectOperationWbsController : ControllerBase
{
    private readonly IProjectOperationWbsLogic _logic;

    public ProjectOperationWbsController(IProjectOperationWbsLogic logic)
    {
        _logic = logic;
    }

    [HttpPost]
    [ResponseSchema<CreateProjectOperationWbsResponse>]
    public async Task<IResult> CreateProjectOperationWbs(
    [FromBody] CreateProjectOperationWbsRequest request,
    CT ct)
    {
        var result = await _logic.CreateProjectOperationWbs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("{id}")]
    [ResponseSchema<UpdateProjectOperationWbsResponse>]
    public async Task<IResult> UpdateProjectOperationWbs(
    [FromRoute] long id,
    [FromBody] UpdateProjectOperationWbsModel payload,
    CT ct)
    {
        var result = await _logic.UpdateProjectOperationWbs(new(id, payload), ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("{id}")]
    [ResponseSchema<DeleteProjectOperationWbsResponse>]
    public async Task<IResult> DeleteProjectOperationWbs(
    [FromRoute] long id,
    CT ct)
    {
        var result = await _logic.DeleteProjectOperationWbs(new(id), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("{id}")]
    [ResponseSchema<GetProjectOperationWbsByIdResponse>]
    public async Task<IResult> GetProjectOperationWbsById(
    [FromRoute] long id,
    CT ct)
    {
        var result = await _logic.GetProjectOperationWbsById(new(id), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("fltr")]
    [ResponseSchema<GetFltrPOWbsResponse>]
    public async Task<IResult> GetFltrPOWbs(
    [FromBody] GetFltrPOWbsRequest request,
    CT ct)
    {
        var result = await _logic.GetFltrPOWbs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("fltr-excel-columns")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetFltrPOWbsEnum(
    [FromBody] GetEnumsRequest request,
    CT ct)
    {
        var result = EnumExtensions.GetEnums<FltrPOWbsEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("fltr-excel-export")]
    [ResponseSchema<GetFltrPOWbsResponse>]
    public async Task<IResult> GetFltrPOWbsExporter(
        [FromBody] GetFltrPOWbsExcelRequest request, CT ct)
    {
        var response = await _logic.GetFltrPOWbs(request.Adapt<GetFltrPOWbsRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetFltrPOWbsExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, "ProjectOperationWbs"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperationWbs-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetFltrPOWbsExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpGet("GetPOWbsByPOId")]
    [ResponseSchema<GetPOWbsByPOIdResponse>]
    public async Task<IResult> GetPOWbsByPOId(
    [FromQuery] GetPOWbsByPOIdRequest request,
    CT ct)
    {
        var result = await _logic.GetPOWbsByPOId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetPOWbsByProjectWbsId")]
    [ResponseSchema<GetPOWbsByProjectWbsIdResponse>]
    public async Task<IResult> GetPOWbsByProjectWbsId(
    [FromBody] GetPOWbsByProjectWbsIdRequest request,
    CT ct)
    {
        var result = await _logic.GetPOWbsByProjectWbsId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDetailPOWbsByProjectWbsId")]
    [ResponseSchema<GetDetailPOWbsByProjectWbsIdResponse>]
    public async Task<IResult> GetDetailPOWbsByProjectWbsId(
    [FromBody] GetDetailPOWbsByProjectWbsIdRequest request,
    CT ct)
    {
        var result = await _logic.GetDetailPOWbsByProjectWbsId(request, ct);
        return result.GetHttpResponse();
    }
}