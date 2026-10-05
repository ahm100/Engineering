using Engineering.Application.Services.ProjectOperationDetailInspections;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.CreateProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.DeleteProjectOperationDetailInspection;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetFilteredProjectOperationDetailInspections;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetProjectOperationDetailInspectionById;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionCreator;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelEnum;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelExporter;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsTotalInspectionReport;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetTotalDailiesByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionGroupDelete;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.UpdateProjectOperationDetailInspection;

namespace Engineering.Api.Controllers.ProjectOperationDetailInspections;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDetailInspection")]
public class ProjectOperationDetailInspectionController : ControllerBase
{
    private readonly IProjectOperationDetailInspectionLogic _logic;

    public ProjectOperationDetailInspectionController(IProjectOperationDetailInspectionLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateInspection")]
    [ResponseSchema<CreateProjectOperationDetailInspectionResponse>]
    public async Task<IResult> CreateInspection([FromBody] CreateProjectOperationDetailInspectionRequest request, CT ct)
    {
        var result = await _logic.CreateProjectOperationDetailInspection(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("InspectionGroupDelete")]
    [ResponseSchema<ProjectOperationDetailInspectionGroupDeleteResponse>]
    public async Task<IResult> InspectionGroupDelete([FromBody] ProjectOperationDetailInspectionGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationDetailInspectionGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateInspection")]
    [ResponseSchema<UpdateProjectOperationDetailInspectionResponse>]
    public async Task<IResult> UpdateInspection([FromBody] UpdateProjectOperationDetailInspectionRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationDetailInspection(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredInspections")]
    [ResponseSchema<GetFilteredProjectOperationDetailInspectionsResponse>]
    public async Task<IResult> GetFilteredInspections([FromBody] GetFilteredProjectOperationDetailInspectionsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredProjectOperationDetailInspections(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetInspectionById")]
    [ResponseSchema<GetProjectOperationDetailInspectionByIdResponse>]
    public async Task<IResult> GetInspectionById([FromQuery] GetProjectOperationDetailInspectionByIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationDetailInspectionById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTotalDailiesByProjectOperationDetailId")]
    [ResponseSchema<GetTotalDailiesByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetTotalDailiesByProjectOperationDetailId([FromQuery] GetTotalDailiesByProjectOperationDetailIdRequest request, CT ct)
    {
        var result = await _logic.GetTotalDailiesByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsInspectionReport")]
    [ResponseSchema<GetsInspectionReportResponse>]
    public async Task<IResult> GetsInspectionReport([FromBody] GetsInspectionReportRequest request, CT ct)
    {
        var result = await _logic.GetsInspectionReport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsInspectionReportExcelExporter")]
    [ResponseSchema<GetsInspectionReportExcelExporterResponse>]
    public async Task<IResult> GetsInspectionReportExcelExporter([FromBody] GetsInspectionReportExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsInspectionReportExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsInspectionReportExcelEnum")]
    [ResponseSchema<GetsInspectionReportExcelEnumResponse>]
    public async Task<IResult> GetsInspectionReportExcelEnum([FromQuery] GetsInspectionReportExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsInspectionReportExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalInspectionReport")]
    [ResponseSchema<GetsTotalInspectionReportResponse>]
    public async Task<IResult> GetsTotalInspectionReport([FromBody] GetsTotalInspectionReportRequest request, CT ct)
    {
        var result = await _logic.GetsTotalInspectionReport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsInspectionCreator")]
    [ResponseSchema<GetsInspectionCreatorResponse>]
    public async Task<IResult> GetsInspectionCreator([FromQuery] GetsInspectionCreatorRequest request, CT ct)
    {
        var result = await _logic.GetsInspectionCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteInspection")]
    [ResponseSchema<DeleteProjectOperationDetailInspectionResponse>]
    public async Task<IResult> DeleteInspection([FromBody] DeleteProjectOperationDetailInspectionRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectOperationDetailInspection(request, ct);
        return result.GetHttpResponse();
    }
}