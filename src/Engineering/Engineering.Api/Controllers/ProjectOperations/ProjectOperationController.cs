using Engineering.Api.Controllers.ProjectOperations.Contracts.GetsFilteredByProjectIds;
using Engineering.Api.Extensions.Enums;
using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.ChangeProject;
using Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperationActions;
using Engineering.Application.Services.ProjectOperations.Models.CreatesProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperationActions;
using Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrPOForReports;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;
using Engineering.Application.Services.ProjectOperations.Models.GetPODate;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationById;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationByParams;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationDocuments;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;
using Engineering.Application.Services.ProjectOperations.Models.GetsByEmployerContract;
using Engineering.Application.Services.ProjectOperations.Models.GetsByOperationInfo;
using Engineering.Application.Services.ProjectOperations.Models.GetsByProject;
using Engineering.Application.Services.ProjectOperations.Models.GetsByProjectOperationId;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProject;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProjectIds;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;
using Engineering.Application.Services.ProjectOperations.Models.GetsForDailyProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;
using Engineering.Application.Services.ProjectOperations.Models.GetsForPricing;
using Engineering.Application.Services.ProjectOperations.Models.GetsPrioritizeProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByIds;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByProjectIds;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelEnum;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelExporter;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReportingExcelExporter;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelEnum;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelExporter;
using Engineering.Application.Services.ProjectOperations.Models.GetsProposedPrice;
using Engineering.Application.Services.ProjectOperations.Models.GetsStatusType;
using Engineering.Application.Services.ProjectOperations.Models.GetsSummarizedProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetsSummaryProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsWithoutContract;
using Engineering.Application.Services.ProjectOperations.Models.GroupProjectOperationStatusChanger;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationExcelImports;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationGroupDelete;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationWorkloadManagement;
using Engineering.Application.Services.ProjectOperations.Models.SetPlannedDate;
using Engineering.Application.Services.ProjectOperations.Models.SetProjectOperationPriority;
using Engineering.Application.Services.ProjectOperations.Models.UpdatePrice;
using Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationDocuments;
using Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationPrice;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using static Engineering.Api.Helpers.ExcelTools.ExcelHelpers;

[ApiController]
[Route("api/engineering/v1/ProjectOperation")]
[Tags("ProjectOperation")]
public class ProjectOperationController : ControllerBase
{
    private readonly IProjectOperationLogic _logic;
    private readonly ILogger<ProjectOperationController> _logger;

    public ProjectOperationController(IProjectOperationLogic logic,
        ILogger<ProjectOperationController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpPost("AddProjectOperation")]
    [ResponseSchema<CreateProjectOperationResponse>]
    public async Task<IResult> AddProjectOperation([FromBody] CreateProjectOperationRequest request, CT ct)
    {
        var result = await _logic.CreateProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddProjectOperations")]
    [ResponseSchema<CreatesProjectOperationResponse>]
    public async Task<IResult> AddProjectOperations([FromBody] CreatesProjectOperationRequest request, CT ct)
    {
        var result = await _logic.CreatesProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectOperationExcelImports")]
    public async Task<IResult> ProjectOperationExcelImports(ProjectOperationExcelImportsRequest request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - ProjectOperationExcelImports for Project {request.ProjectId}");
        var result = await _logic.ProjectOperationExcelImports(request, ct);

        if (result.IsSuccess && result.Value?.ErrorModels != null && result.Value.ErrorModels.Any())
        {
            var fileBytes = ExcelExporter.ExportImportErrorsToExcel<ProjectOperationImportErrorModel, ProjectOperationImportErrorEnum>(
                result.Value.ErrorModels,
                "ImportErrors");

            // 422 + the corrected Excel file as the body
            return new ErrorFileResult(
                fileBytes,
                $"ProjectOperation_Errors-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx");
        }

        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectOperationActions")]
    [ResponseSchema<CreateProjectOperationActionsResponse>]
    public async Task<IResult> CreateProjectOperationActions([FromBody] CreateProjectOperationActionsRequest request, CT ct)
    {
        var result = await _logic.CreateProjectOperationActions(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectOperationGroupDelete")]
    [ResponseSchema<ProjectOperationGroupDeleteResponse>]
    public async Task<IResult> ProjectOperationGroupDelete([FromBody] ProjectOperationGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ChangeProject")]
    [ResponseSchema<ChangeProjectResponse>]
    public async Task<IResult> ChangeProject([FromBody] ChangeProjectRequest request, CT ct)
    {
        var result = await _logic.ChangeProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrPOForReports")]
    [ResponseSchema<GetFltrPOForReportsResponse>]
    public async Task<IResult> GetFltrPOForReports([FromBody] GetFltrPOForReportsRequest request, CT ct)
    {
        var result = await _logic.GetFltrPOForReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteProjectOperationActions")]
    [ResponseSchema<DeleteProjectOperationActionsResponse>]
    public async Task<IResult> DeleteProjectOperationActions([FromBody] DeleteProjectOperationActionsRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectOperationActions(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectOperation")]
    [ResponseSchema<UpdateProjectOperationResponse>]
    public async Task<IResult> EditProjectOperation([FromBody] UpdateProjectOperationRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateProjectOperationPrice")]
    [ResponseSchema<UpdateProjectOperationPriceResponse>]
    public async Task<IResult> UpdateProjectOperationPrice([FromBody] UpdateProjectOperationPriceRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationPrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("GroupProjectOperationStatusChanger")]
    [ResponseSchema<GroupProjectOperationStatusChangerResponse>]
    public async Task<IResult> GroupProjectOperationStatusChanger([FromBody] GroupProjectOperationStatusChangerRequest request, CT ct)
    {
        var result = await _logic.GroupProjectOperationStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetPriority")]
    [ResponseSchema<SetProjectOperationPriorityResponse>]
    public async Task<IResult> SetPriority([FromBody] SetProjectOperationPriorityRequest request, CT ct)
    {
        var result = await _logic.SetProjectOperationPriority(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ProjectOperationStatusChanger")]
    [ResponseSchema<ProjectOperationStatusChangerResponse>]
    public async Task<IResult> ProjectOperationStatusChanger([FromBody] ProjectOperationStatusChangerRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationStatusToNotStarted")]
    [ResponseSchema<ProjectOperationStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationStatusToNotStarted([FromBody] SetProjectOperationToNotStartedRequest request, CT ct)
    {
        var requestModel = new ProjectOperationStatusChangerRequest(request.Id, ProjectOperationStatus.NotStarted);
        var result = await _logic.ProjectOperationStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationStatusToDoing")]
    [ResponseSchema<ProjectOperationStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationStatusToDoing([FromBody] SetProjectOperationStatusToDoingRequest request, CT ct)
    {
        var requestModel = new ProjectOperationStatusChangerRequest(request.Id, ProjectOperationStatus.Doing);
        var result = await _logic.ProjectOperationStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationStatusToStopped")]
    [ResponseSchema<ProjectOperationStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationStatusToStopped([FromBody] SetProjectOperationToStoppedRequest request, CT ct)
    {
        var requestModel = new ProjectOperationStatusChangerRequest(request.Id, ProjectOperationStatus.Stopped);
        var result = await _logic.ProjectOperationStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationStatusToEndOfWork")]
    [ResponseSchema<ProjectOperationStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationStatusToEndOfWork([FromBody] SetProjectOperationToEndOfWorkRequest request, CT ct)
    {
        var requestModel = new ProjectOperationStatusChangerRequest(request.Id, ProjectOperationStatus.EndOfWork);
        var result = await _logic.ProjectOperationStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationStatusToTemporaryDelivery")]
    [ResponseSchema<ProjectOperationStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationStatusToTemporaryDelivery([FromBody] SetProjectOperationToTemporaryDeliveryRequest request, CT ct)
    {
        var requestModel = new ProjectOperationStatusChangerRequest(request.Id, ProjectOperationStatus.TemporaryDelivery);
        var result = await _logic.ProjectOperationStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectOperationStatusToDefiniteDelivery")]
    [ResponseSchema<ProjectOperationStatusChangerResponse>]
    public async Task<IResult> SetProjectOperationStatusToDefiniteDelivery([FromBody] SetProjectOperationStatusToDefiniteDeliveryRequest request, CT ct)
    {
        var requestModel = new ProjectOperationStatusChangerRequest(request.Id, ProjectOperationStatus.DefiniteDelivery);
        var result = await _logic.ProjectOperationStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateProjectOperationDocuments")]
    [ResponseSchema<UpdateProjectOperationDocumentsResponse>]
    public async Task<IResult> UpdateProjectOperationDocuments([FromBody] UpdateProjectOperationDocumentsRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationDocuments(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationById")]
    [ResponseSchema<GetProjectOperationByIdResponse>]
    public async Task<IResult> GetProjectOperationById([FromQuery] GetProjectOperationByIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPOActionByPOId")]
    [ResponseSchema<GetPOActionByPOIdResponse>]
    public async Task<IResult> GetPOActionByPOId([FromQuery] GetPOActionByPOIdRequest request, CT ct)
    {
        var result = await _logic.GetPOActionByPOId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationDocuments")]
    [ResponseSchema<GetProjectOperationDocumentsResponse>]
    public async Task<IResult> GetProjectOperationDocuments([FromQuery] GetProjectOperationDocumentsRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationDocuments(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationByParams")]
    [ResponseSchema<GetProjectOperationByParamsResponse>]
    public async Task<IResult> GetProjectOperationByParams([FromQuery] GetProjectOperationByParamsRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationByParams(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectOperationStatusType")]
    [ResponseSchema<GetsProjectOperationStatusTypeResponse>]
    public async Task<IResult> GetsProjectOperationStatusType([FromQuery] GetsProjectOperationStatusTypeRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationStatusType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByEmployerContractId")]
    [ResponseSchema<GetsByEmployerContractResponse>]
    public async Task<IResult> GetsByEmployerContractId([FromQuery] GetsByEmployerContractRequest request, CT ct)
    {
        var result = await _logic.GetsByEmployerContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByProjectId")]
    [ResponseSchema<GetsByProjectResponse>]
    public async Task<IResult> GetsByProjectId([FromQuery] GetsByProjectRequest request, CT ct)
    {
        var result = await _logic.GetsByProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredByProjectId")]
    [ResponseSchema<GetsFilteredByProjectResponse>]
    public async Task<IResult> GetsFilteredByProjectId([FromBody] GetsFilteredByProjectRequest request, CT ct)
    {
        var result = await _logic.GetsFilteredByProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationByProjectIdsId")]
    [ResponseSchema<GetsProjectOperationByProjectIdsResponse>]
    public async Task<IResult> GetsProjectOperationByProjectIdsId([FromBody] GetsProjectOperationByProjectIdsRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationByProjectIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationEmployerReportingExcelExporter")]
    [ResponseSchema<GetsProjectOperationEmployerReportingExcelExporterResponse>]
    public async Task<IResult> GetsProjectOperationEmployerReportingExcelExporter([FromBody] GetsProjectOperationEmployerReportingExcelExporterRequest request, CT ct)
    {
        try
        {
            var response = await _logic.GetsProjectOperationEmployerReporting(
                new GetsProjectOperationEmployerReportingRequest(
                    request.Ids,
                    request.CostCenterId,
                    request.ProjectIds,
                    request.OperationInfoIds,
                    request.ContractorIds,
                    request.EmployerIds,
                    request.StartDate,
                    request.EndDate,
                    request.FilterData,
                    request.OrderBy,
                    request.PageIndex,
                    request.PageSize),
                ct);
            if (response.IsFailure)
                return Result.Failure<GetsProjectOperationEmployerReportingExcelExporterResponse>(response.Error!).GetHttpResponse();

            var values = response.Value?.Data ?? [];
            values.ForEach(item => item.DailyProjectOperations ??= []);
            var dailyValues = values.SelectMany(item => item.DailyProjectOperations ?? []).ToList();

            var file = new FileContentResult(
                ExcelExporter.ExportToExcel<
                    GetsProjectOperationEmployerReportingModel,
                    DailyProjectOperationModel,
                    ProjectOperationDailyExcelEnum,
                    ProjectOperationDailyDetailExcelEnum>(
                        values,
                        dailyValues,
                        [
                            ProjectOperationDailyExcelEnum.Id,
                            ProjectOperationDailyExcelEnum.CostCenterId,
                            ProjectOperationDailyExcelEnum.CostCenterName,
                            ProjectOperationDailyExcelEnum.CostCenterCode,
                            ProjectOperationDailyExcelEnum.ProjectId,
                            ProjectOperationDailyExcelEnum.ProjectName,
                            ProjectOperationDailyExcelEnum.ProjectCode,
                            ProjectOperationDailyExcelEnum.OperationInfoId,
                            ProjectOperationDailyExcelEnum.OperationInfoName,
                            ProjectOperationDailyExcelEnum.OperationInfoCode,
                            ProjectOperationDailyExcelEnum.OperationInfoMeasurementId,
                            ProjectOperationDailyExcelEnum.OperationInfoMeasurementName,
                            ProjectOperationDailyExcelEnum.Workload,
                            ProjectOperationDailyExcelEnum.DoneWorkload,
                            ProjectOperationDailyExcelEnum.RemaindedWorkload,
                            ProjectOperationDailyExcelEnum.Description
                        ],
                        [
                            ProjectOperationDailyDetailExcelEnum.Id,
                            ProjectOperationDailyDetailExcelEnum.ProjectOperationId,
                            ProjectOperationDailyDetailExcelEnum.OperationInfoName,
                            ProjectOperationDailyDetailExcelEnum.OperationInfoCode,
                            ProjectOperationDailyDetailExcelEnum.OperationInfoMeasurementName,
                            ProjectOperationDailyDetailExcelEnum.ProjectName,
                            ProjectOperationDailyDetailExcelEnum.ProjectCode,
                            ProjectOperationDailyDetailExcelEnum.Location,
                            ProjectOperationDailyDetailExcelEnum.Length,
                            ProjectOperationDailyDetailExcelEnum.Height,
                            ProjectOperationDailyDetailExcelEnum.Width,
                            ProjectOperationDailyDetailExcelEnum.Weight,
                            ProjectOperationDailyDetailExcelEnum.Number,
                            ProjectOperationDailyDetailExcelEnum.Description
                        ],
                        "روکش صورت وضعیت",
                        "ریزمتره"),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"ProjectOperations-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
                LastModified = DateTime.UtcNow
            };

            return Result.Success<GetsProjectOperationEmployerReportingExcelExporterResponse?>(new(file)).GetHttpResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Project operation employer reporting Excel export failed. CostCenterId: {CostCenterId}, Exporter: {Exporter}",
                request.CostCenterId,
                nameof(ExcelExporter));
            throw;
        }
    }

    [HttpPost("GetsProjectOperationEmployerReporting")]
    [ResponseSchema<GetsProjectOperationEmployerReportingResponse>]
    public async Task<IResult> GetsProjectOperationEmployerReporting([FromBody] GetsProjectOperationEmployerReportingRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationEmployerReporting(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationDailyReporting")]
    [ResponseSchema<GetsProjectOperationDailyReportingResponse>]
    public async Task<IResult> GetsProjectOperationDailyReporting([FromBody] GetsProjectOperationDailyReportingRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDailyReporting(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationDailyReportingExcelExporter")]
    [ResponseSchema<GetsProjectOperationDailyReportingExcelExporterResponse>]
    public async Task<IResult> GetsProjectOperationDailyReportingExcelExporter([FromBody] GetsProjectOperationDailyReportingExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationDailyReportingExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsWithoutContract")]
    [ResponseSchema<GetsWithoutContractResponse>]
    public async Task<IResult> GetsWithoutContract([FromBody] GetsWithoutContractRequest request, CT ct)
    {
        var result = await _logic.GetsWithoutContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsForEmployerStatusStatement")]
    [ResponseSchema<GetsForEmployerStatusStatementResponse>]
    public async Task<IResult> GetsForEmployerStatusStatement([FromQuery] GetsForEmployerStatusStatementRequest request, CT ct)
    {
        var result = await _logic.GetsForEmployerStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByOperationInfoId")]
    [ResponseSchema<GetsByOperationInfoResponse>]
    public async Task<IResult> GetsByOperationInfo([FromQuery] GetsByOperationInfoRequest request, CT ct)
    {
        var result = await _logic.GetsByOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProposedPriceId")]
    [ResponseSchema<GetsProposedPriceResponse>]
    public async Task<IResult> GetsProposedPrice([FromQuery] GetsProposedPriceRequest request, CT ct)
    {
        var result = await _logic.GetsProposedPrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsForPricing")]
    [ResponseSchema<GetsForPricingResponse>]
    public async Task<IResult> GetsForPricing([FromQuery] GetsForPricingRequest request, CT ct)
    {
        var result = await _logic.GetsForPricing(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByProjectOperationId")]
    [ResponseSchema<GetsByProjectOperationIdResponse>]
    public async Task<IResult> GetsByProjectOperationId([FromQuery] GetsByProjectOperationIdRequest request, CT ct)
    {
        var result = await _logic.GetsByProjectOperationId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsSummaryProjectOperation")]
    [ResponseSchema<GetsSummaryProjectOperationResponse>]
    public async Task<IResult> GetsSummaryProjectOperation([FromQuery] GetsSummaryProjectOperationRequest request, CT ct)
    {
        var result = await _logic.GetsSummaryProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsForDailyProjectOperations")]
    [ResponseSchema<GetsForDailyProjectOperationsResponse>]
    public async Task<IResult> GetsForDailyProjectOperations([FromQuery] GetsForDailyProjectOperationsRequest request, CT ct)
    {
        var result = await _logic.GetsForDailyProjectOperations(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationWorkloadManagement")]
    [ResponseSchema<ProjectOperationWorkloadManagementResponse>]
    public async Task<IResult> GetProjectOperationWorkloadManagement([FromQuery] ProjectOperationWorkloadManagementRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationWorkloadManagement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsSummarizedProjectOperation")]
    [ResponseSchema<GetsSummarizedProjectOperationResponse>]
    public async Task<IResult> GetsSummarizedProjectOperation([FromQuery] GetsSummarizedProjectOperationRequest request, CT ct)
    {
        var result = await _logic.GetsSummarizedProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsPrioritizeProjectOperation")]
    [ResponseSchema<GetsPrioritizeProjectOperationResponse>]
    public async Task<IResult> GetsPrioritizeProjectOperation([FromQuery] GetsPrioritizeProjectOperationRequest request, CT ct)
    {
        var result = await _logic.GetsPrioritizeProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationReporting")]
    [ResponseSchema<GetsProjectOperationReportingResponse>]
    public async Task<IResult> GetsProjectOperationReporting([FromBody] GetsProjectOperationReportingRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationReporting(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectOperationByIds")]
    [ResponseSchema<GetsProjectOperationByIdsResponse>]
    public async Task<IResult> GetsProjectOperationByIds([FromBody] GetsProjectOperationByIdsRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationByIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredByProjectIds")]
    [ResponseSchema<GetsFilteredByProjectIdsResponse>]
    public async Task<IResult> GetsFilteredByProjectIds([FromBody] GetsFilteredByProjectIdsRequest request, CT ct)
    {
        var result = await _logic.GetsFilteredByProjectIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredByProjectIdsEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetFltrPOWbsEnum(
    [FromBody] GetEnumsRequest request,
    CT ct)
    {
        var result = EnumExtensions.GetEnums<FltrProjectOperationEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetsFilteredByProjectIdsExporter")]
    [ResponseSchema<GetsFilteredByProjectIdsExcelResponse>]
    public async Task<IResult> GetsFilteredByProjectIdsExporter(
        [FromBody] GetsFilteredByProjectIdsExcelRequest request, CT ct)
    {
        var response = await _logic.GetsFilteredByProjectIds(request.Adapt<GetsFilteredByProjectIdsRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetsFilteredByProjectIdsExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, "ProjectOperationWbs"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectOperationWbs-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetsFilteredByProjectIdsExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetsFilteredForReports")]
    [ResponseSchema<GetsFilteredForReportsResponse>]
    public async Task<IResult> GetsFilteredForReports([FromBody] GetsFilteredForReportsRequest request, CT ct)
    {
        var result = await _logic.GetsFilteredForReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectOperationReportingExcelEnum")]
    [ResponseSchema<GetsProjectOperationReportingExcelEnumResponse>]
    public async Task<IResult> GetsProjectOperationReportingExcelEnum([FromQuery] GetsProjectOperationReportingExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationReportingExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrProjectOperation")]
    [ResponseSchema<GetFltrProjectOperationResponse>]
    public async Task<IResult> GetFltrProjectOperation([FromBody] GetFltrProjectOperationRequest request, CT ct)
    {
        var result = await _logic.GetFltrProjectOperation(request, ct);
        return result.GetHttpResponse();
    }


    [HttpPost("GetsProjectOperationReportingExcelExporter")]
    [ResponseSchema<GetsProjectOperationReportingExcelExporterResponse>]
    public async Task<IResult> GetsProjectOperationReportingExcelExporter([FromBody] GetsProjectOperationReportingExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsProjectOperationReportingExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalProjectOperationReporting")]
    [ResponseSchema<GetsTotalProjectOperationReportingResponse>]
    public async Task<IResult> GetsTotalProjectOperationReporting([FromBody] GetsTotalProjectOperationReportingRequest request, CT ct)
    {
        var result = await _logic.GetsTotalProjectOperationReporting(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProjectOperation")]
    [ResponseSchema<DeleteProjectOperationResponse>]
    public async Task<IResult> DeleteProjectOperation([FromQuery] DeleteProjectOperationRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrBasePricedPOs")]
    [ResponseSchema<GetFltrBasePricedPOsResponse>]
    public async Task<IResult> GetFltrBasePricedPOs([FromBody] GetFltrBasePricedPOsRequest request, CT ct)
    {
        var result = await _logic.GetFltrBasePricedPOs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdatePrice")]
    [ResponseSchema<UpdatePriceResponse>]
    public async Task<IResult> UpdatePrice([FromBody] UpdatePriceRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationPrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectOperationProgress")]
    [ResponseSchema<GetProjectOperationProgressResponse>]
    public async Task<IResult> GetProjectOperationProgress(
        [FromQuery] GetProjectOperationProgressRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationProgress(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SetPlannedDate")]
    [ResponseSchema<SetPlannedDateResponse>]
    public async Task<IResult> SetPlannedDate(
        [FromBody] SetPlannedDateRequest request, CT ct)
    {
        var result = await _logic.SetPlannedDate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPODate")]
    [ResponseSchema<GetPODateResponse>]
    public async Task<IResult> GetPODate(
        [FromQuery] GetPODateRequest request, CT ct)
    {
        var result = await _logic.GetPODate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCriticalPO")]
    [ResponseSchema<GetCriticalPOResponse>]
    public async Task<IResult> GetCriticalPO(
        [FromQuery] GetCriticalPORequest request, CT ct)
    {
        var result = await _logic.GetCriticalPO(request, ct);
        return result.GetHttpResponse();
    }
}
