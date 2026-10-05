using Engineering.Application.Services.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.CreateProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.CreateProjectOperationDetailComment;
using Engineering.Application.Services.ProjectOperationDetails.Models.CreateVizardProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.DeleteProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.DeleteProjectOperationDetailComment;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetHistoryByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailByCode;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailById;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailCommentById;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailDoneVolume;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelEnums;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsConsumableVolumes;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorProjectOperationDetailDetailReports;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorProjectOperationDetailReports;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsMinimalByProjectOperationIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByContractorIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByExpertId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByMachineryId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByProductId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByProjectOperationId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailForScheduling;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailStatus;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsSummarizedByProjectOperationIds;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsByProjectOperationId;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsProjectOperationDetailReport;
using Engineering.Application.Services.ProjectOperationDetails.Models.GroupProjectOperationDetailStatusChanger;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailCodeCreator;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailGroupDelete;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;
using Engineering.Application.Services.ProjectOperationDetails.Models.SetProjectOperationDetailPriority;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailComment;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailVolumes;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdatesProjectOperationDetailDate;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDetail")]
public class ProjectOperationDetailController : ControllerBase
{
    private readonly IProjectOperationDetailLogic _logic;

    public ProjectOperationDetailController(IProjectOperationDetailLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddProjectOperationDetail")]
    [ResponseSchema<CreateProjectOperationDetailResponse>]
    public async Task<IResult> AddProjectOperationDetail(
        [FromBody] CreateProjectOperationDetailRequest request, CT ct)
    {
        var result = await _logic.CreateProjectOperationDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectOperationDetailCodeCreator")]
    [ResponseSchema<ProjectOperationDetailCodeCreatorResponse>]
    public async Task<IResult> ProjectOperationDetailCodeCreator(
        [FromBody] ProjectOperationDetailCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationDetailCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddVizardProjectOperationDetail")]
    [ResponseSchema<CreateVizardProjectOperationDetailResponse>]
    public async Task<IResult> AddVizardProjectOperationDetail(
        [FromBody] CreateVizardProjectOperationDetailRequest request, CT ct)
    {
        var result = await _logic.CreateVizardProjectOperationDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectOperationDetail")]
    [ResponseSchema<UpdateProjectOperationDetailResponse>]
    public async Task<IResult> EditProjectOperationDetail(
        [FromBody] UpdateProjectOperationDetailRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectOperationDetails")]
    [ResponseSchema<UpdateProjectOperationDetailsResponse>]
    public async Task<IResult> EditProjectOperationDetails(
        [FromBody] UpdateProjectOperationDetailsRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationDetails(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateProjectOperationDetailVolumes")]
    [ResponseSchema<UpdateProjectOperationDetailVolumesResponse>]
    public async Task<IResult> UpdateProjectOperationDetailVolumes(
        [FromBody] UpdateProjectOperationDetailVolumesRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationDetailVolumes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdatesProjectOperationDetailDate")]
    [ResponseSchema<UpdatesProjectOperationDetailDateResponse>]
    public async Task<IResult> UpdatesProjectOperationDetailDate(
        [FromBody] UpdatesProjectOperationDetailDateRequest request, CT ct)
    {
        var result = await _logic.UpdatesProjectOperationDetailDate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetPriority")]
    [ResponseSchema<SetProjectOperationDetailPriorityResponse>]
    public async Task<IResult> SetPriority([FromBody] SetProjectOperationDetailPriorityRequest request, CT ct)
    {
        var result = await _logic.SetProjectOperationDetailPriority(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GroupProjectOperationDetailStatusChanger")]
    [ResponseSchema<GroupProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> GroupProjectOperationDetailStatusChanger(
        [FromBody] GroupProjectOperationDetailStatusChangerRequest request, CT ct)
    {
        var result = await _logic.GroupProjectOperationDetailStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ProjectOperationDetailStatusChanger")]
    [ResponseSchema<ProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> ProjectOperationDetailStatusChanger(
        [FromBody] ProjectOperationDetailStatusChangerRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationDetailStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationDetailToDoing")]
    [ResponseSchema<ProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationDetailToDoing(
        [FromBody] SetProjectOperationDetailToDoingRequest request, CT ct)
    {
        var requestModel = new ProjectOperationDetailStatusChangerRequest(
            request.Id,
            ProjectOperationDetailStatus.Doing,
            null);

        var result = await _logic.ProjectOperationDetailStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationDetailToDefiniteDelivery")]
    [ResponseSchema<ProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationDetailToDefiniteDelivery(
        [FromBody] SetProjectOperationDetailToDefiniteDeliveryRequest request, CT ct)
    {
        var requestModel = new ProjectOperationDetailStatusChangerRequest(
            request.Id,
            ProjectOperationDetailStatus.DefiniteDelivery,
            request.StatusDescription);

        var result = await _logic.ProjectOperationDetailStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationDetailToTemporaryDelivery")]
    [ResponseSchema<ProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationDetailToTemporaryDelivery(
        [FromBody] SetProjectOperationDetailToTemporaryDeliveryRequest request, CT ct)
    {
        var requestModel = new ProjectOperationDetailStatusChangerRequest(
            request.Id,
            ProjectOperationDetailStatus.TemporaryDelivery,
            request.StatusDescription);

        var result = await _logic.ProjectOperationDetailStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationDetailToEndOfWork")]
    [ResponseSchema<ProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationDetailToEndOfWork(
        [FromBody] SetProjectOperationDetailToEndOfWorkRequest request, CT ct)
    {
        var requestModel = new ProjectOperationDetailStatusChangerRequest(
            request.Id,
            ProjectOperationDetailStatus.EndOfWork,
            request.StatusDescription);

        var result = await _logic.ProjectOperationDetailStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationDetailToStopped")]
    [ResponseSchema<ProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationDetailToStopped(
        [FromBody] SetProjectOperationDetailToStoppedRequest request, CT ct)
    {
        var requestModel = new ProjectOperationDetailStatusChangerRequest(
            request.Id,
            ProjectOperationDetailStatus.Stopped,
            request.StatusDescription);

        var result = await _logic.ProjectOperationDetailStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationDetailToNotStarted")]
    [ResponseSchema<ProjectOperationDetailStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationDetailToNotStarted([
        FromBody] SetProjectOperationDetailToNotStartedRequest request, CT ct)
    {
        var requestModel = new ProjectOperationDetailStatusChangerRequest(
            request.Id,
            ProjectOperationDetailStatus.NotStarted,
            null);

        var result = await _logic.ProjectOperationDetailStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationDetailById")]
    [ResponseSchema<GetProjectOperationDetailByIdResponse>]
    public async Task<IResult> GetProjectOperationDetailById(
        [FromQuery] GetProjectOperationDetailByIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationDetailById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationDetailDoneVolume")]
    [ResponseSchema<GetProjectOperationDetailDoneVolumeResponse>]
    public async Task<IResult> GetProjectOperationDetailDoneVolume(
        [FromQuery] GetProjectOperationDetailDoneVolumeRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationDetailDoneVolume(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectOperationDetailStatus")]
    [ResponseSchema<GetsProjectOperationDetailStatusResponse>]
    public async Task<IResult> GetsProjectOperationDetailStatus(
        [FromQuery] GetsProjectOperationDetailStatusRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsByProjectOperationId")]
    [ResponseSchema<GetsProjectOperationDetailByProjectOperationIdResponse>]
    public async Task<IResult> GetsByProjectOperationId(
        [FromBody] GetsProjectOperationDetailByProjectOperationIdRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailByProjectOperationId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByExpertId")]
    [ResponseSchema<GetsProjectOperationDetailByExpertIdResponse>]
    public async Task<IResult> GetsByExpertId(
        [FromQuery] GetsProjectOperationDetailByExpertIdRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailByExpertId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByMachineryId")]
    [ResponseSchema<GetsProjectOperationDetailByMachineryIdResponse>]
    public async Task<IResult> GetsByMachineryId(
        [FromQuery] GetsProjectOperationDetailByMachineryIdRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailByMachineryId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByProductId")]
    [ResponseSchema<GetsProjectOperationDetailByProductIdResponse>]
    public async Task<IResult> GetsByProductId(
        [FromQuery] GetsProjectOperationDetailByProductIdRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailByProductId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationDetailByCode")]
    [ResponseSchema<GetProjectOperationDetailByCodeResponse>]
    public async Task<IResult> GetProjectOperationDetailByCode(
        [FromQuery] GetProjectOperationDetailByCodeRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationDetailByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsSummarizedByProjectOperationIds")]
    [ResponseSchema<GetsSummarizedByProjectOperationIdsResponse>]
    public async Task<IResult> GetsSummarizedByProjectOperationIds(
        [FromBody] GetsSummarizedByProjectOperationIdsRequest request, CT ct)
    {
        var result = await _logic.GetsSummarizedByProjectOperationIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsMinimalByProjectOperationIds")]
    [ResponseSchema<GetsMinimalByProjectOperationIdsResponse>]
    public async Task<IResult> GetsMinimalByProjectOperationIds(
        [FromBody] GetsMinimalByProjectOperationIdsRequest request, CT ct)
    {
        var result = await _logic.GetsMinimalByProjectOperationIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationDetailByIds")]
    [ResponseSchema<GetsProjectOperationDetailByIdsResponse>]
    public async Task<IResult> GetsProjectOperationDetailByIds(
        [FromBody] GetsProjectOperationDetailByIdsRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailByIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectOperationDetailForScheduling")]
    [ResponseSchema<GetsProjectOperationDetailForSchedulingResponse>]
    public async Task<IResult> GetsProjectOperationDetailForScheduling(
        [FromQuery] GetsProjectOperationDetailForSchedulingRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailForScheduling(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectOperationDetailContractors")]
    [ResponseSchema<GetProjectOperationDetailContractorsResponse>]
    public async Task<IResult> GetProjectOperationDetailContractors(
        [FromBody] GetProjectOperationDetailContractorsRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationDetailContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationDetailByContractorIds")]
    [ResponseSchema<GetsProjectOperationDetailByContractorIdsResponse>]
    public async Task<IResult> GetsProjectOperationDetailByContractorIds(
        [FromBody] GetsProjectOperationDetailByContractorIdsRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailByContractorIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationDetailReporting")]
    [ResponseSchema<GetsProjectOperationDetailReportingResponse>]
    public async Task<IResult> GetsProjectOperationDetailReporting(
        [FromBody] GetsProjectOperationDetailReportingRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailReporting(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsConsumableVolumes")]
    [ResponseSchema<GetsConsumableVolumesResponse>]
    public async Task<IResult> GetsConsumableVolumes(
        [FromQuery] GetsConsumableVolumesRequest request, CT ct)
    {
        var result = await _logic.GetsConsumableVolumes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsByProjectOperationIdExcelExporter")]
    [ResponseSchema<GetsByProjectOperationIdExcelExporterResponse>]
    public async Task<IResult> GetsByProjectOperationIdExcelExporter(
        [FromBody] GetsByProjectOperationIdExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsByProjectOperationIdExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByProjectOperationIdExcelEnums")]
    [ResponseSchema<GetsByProjectOperationIdExcelEnumsResponse>]
    public async Task<IResult> GetsByProjectOperationIdExcelEnums(
        [FromQuery] GetsByProjectOperationIdExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetsByProjectOperationIdExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetTotalsProjectOperationDetailReport")]
    [ResponseSchema<GetTotalsProjectOperationDetailReportResponse>]
    public async Task<IResult> GetTotalsProjectOperationDetailReport(
        [FromBody] GetTotalsProjectOperationDetailReportRequest request, CT ct)
    {
        var result = await _logic.GetTotalsProjectOperationDetailReport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectOperationDetailReportingExcelEnum")]
    [ResponseSchema<GetsProjectOperationDetailReportingExcelEnumResponse>]
    public async Task<IResult> GetsProjectOperationDetailReportingExcelEnum(
        [FromQuery] GetsProjectOperationDetailReportingExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailReportingExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationDetailReportingExcelExporter")]
    [ResponseSchema<GetsProjectOperationDetailReportingExcelExporterResponse>]
    public async Task<IResult> GetsProjectOperationDetailReportingExcelExporter(
        [FromBody] GetsProjectOperationDetailReportingExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailReportingExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorProjectOperationDetailReports")]
    [ResponseSchema<GetsContractorProjectOperationDetailReportsResponse>]
    public async Task<IResult> GetsContractorProjectOperationDetailReports(
        [FromBody] GetsContractorProjectOperationDetailReportsRequest request, CT ct)
    {
        var result = await _logic.GetsContractorProjectOperationDetailReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorProjectOperationDetailDetailReports")]
    [ResponseSchema<GetsContractorProjectOperationDetailDetailReportsResponse>]
    public async Task<IResult> GetsContractorProjectOperationDetailDetailReports(
        [FromBody] GetsContractorProjectOperationDetailDetailReportsRequest request, CT ct)
    {
        var result = await _logic.GetsContractorProjectOperationDetailDetailReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorReportsExcelEnum")]
    [ResponseSchema<GetsContractorReportsExcelEnumResponse>]
    public async Task<IResult> GetsContractorReportsExcelEnum(
        [FromQuery] GetsContractorReportsExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsContractorReportsExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorReportsExcelExporter")]
    [ResponseSchema<GetsContractorReportsExcelExporterResponse>]
    public async Task<IResult> GetsContractorReportsExcelExporter(
        [FromBody] GetsContractorReportsExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsContractorReportsExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorDetailReportsExcelEnum")]
    [ResponseSchema<GetsContractorDetailReportsExcelEnumResponse>]
    public async Task<IResult> GetsContractorDetailReportsExcelEnum(
        [FromQuery] GetsContractorDetailReportsExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsContractorDetailReportsExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorDetailReportsExcelExporter")]
    [ResponseSchema<GetsContractorDetailReportsExcelExporterResponse>]
    public async Task<IResult> GetsContractorDetailReportsExcelExporter(
        [FromBody] GetsContractorDetailReportsExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsContractorDetailReportsExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTotalsByProjectOperationId")]
    [ResponseSchema<GetTotalsByProjectOperationIdResponse>]
    public async Task<IResult> GetTotalsByProjectOperationId(
        [FromQuery] GetTotalsByProjectOperationIdRequest request, CT ct)
    {
        var result = await _logic.GetTotalsByProjectOperationId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetHistoryByProjectOperationDetailId")]
    [ResponseSchema<GetHistoryByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetHistoryByProjectOperationDetailId(
        [FromQuery] GetHistoryByProjectOperationDetailIdRequest request, CT ct)
    {
        var result = await _logic.GetHistoryByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProjectOperationDetail")]
    [ResponseSchema<DeleteProjectOperationDetailResponse>]
    public async Task<IResult> DeleteProjectOperationDetail(
        [FromQuery] DeleteProjectOperationDetailRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectOperationDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectOperationDetailGroupDelete")]
    [ResponseSchema<ProjectOperationDetailGroupDeleteResponse>]
    public async Task<IResult> ProjectOperationDetailGroupDelete(
        [FromBody] ProjectOperationDetailGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationDetailGroupDelete(request, ct);
        return result.GetHttpResponse();
    }


    [HttpGet("GetsProjectOperationDetailDocument")]
    [ResponseSchema<GetsProjectOperationDetailDocumentResponse>]
    public async Task<IResult> GetsProjectOperationDetailDocument(
        [FromQuery] GetsProjectOperationDetailDocumentRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailDocument(request, ct);
        return result.GetHttpResponse();
    }

     
    [HttpPost("GetFilteredProjectOperationDetailsByCostCenterId")]
    [ResponseSchema<GetFilteredProjectOperationDetailsResponse>]
    public async Task<IResult> GetFilteredProjectOperationDetailsByCostCenterId(
        [FromBody] GetFilteredProjectOperationDetailsRequest request, CT ct)
    {
        var result = await _logic.GetsFilteredDetailsByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectContractors")]
    [ResponseSchema<GetProjectContractorsResponse>]
    public async Task<IResult> GetProjectContractors(
        [FromQuery] GetProjectContractorsRequest request, CT ct)
    {
        var result = await _logic.GetProjectContractors(request, ct);
        return result.GetHttpResponse();
    }
}