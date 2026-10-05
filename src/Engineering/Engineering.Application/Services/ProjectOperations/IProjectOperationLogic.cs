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

namespace Engineering.Application.Services.ProjectOperations;

public interface IProjectOperationLogic
{
    Task<Result<CreateProjectOperationResponse?>> CreateProjectOperation(
        CreateProjectOperationRequest request, CT ct);

    Task<Result<CreatesProjectOperationResponse?>> CreatesProjectOperation(
        CreatesProjectOperationRequest request, CT ct);

    Task<Result<ProjectOperationExcelImportsResponse>> ProjectOperationExcelImports(
        ProjectOperationExcelImportsRequest request, CT ct);

    Task<Result<CreateProjectOperationActionsResponse>> CreateProjectOperationActions(
        CreateProjectOperationActionsRequest request,
        CT ct);
    Task<Result<UpdateProjectOperationResponse?>> UpdateProjectOperation(
        UpdateProjectOperationRequest request, CT ct);

    Task<Result<UpdateProjectOperationPriceResponse?>> UpdateProjectOperationPrice(
        UpdateProjectOperationPriceRequest request, CT ct);

    Task<Result<ChangeProjectResponse>> ChangeProject(
        ChangeProjectRequest request,
        CT ct);

    Task<Result<UpdateProjectOperationDocumentsResponse?>> UpdateProjectOperationDocuments(
        UpdateProjectOperationDocumentsRequest request, CT ct);

    Task<Result<ProjectOperationStatusChangerResponse?>> ProjectOperationStatusChanger(
        ProjectOperationStatusChangerRequest request, CT ct);

    Task<Result<DeleteProjectOperationResponse?>> DeleteProjectOperation(
        DeleteProjectOperationRequest request, CT ct);

    Task<Result<DeleteProjectOperationActionsResponse>> DeleteProjectOperationActions(
        DeleteProjectOperationActionsRequest request,
        CT ct);

    Task<Result<ProjectOperationGroupDeleteResponse?>> ProjectOperationGroupDelete(
        ProjectOperationGroupDeleteRequest request, CT ct);

    Task<Result<GroupProjectOperationStatusChangerResponse?>> GroupProjectOperationStatusChanger(
        GroupProjectOperationStatusChangerRequest request, CT ct);

    Task<Result<GetProjectOperationByIdResponse?>> GetProjectOperationById(
        GetProjectOperationByIdRequest request, CT ct);

    Task<Result<GetProjectOperationDocumentsResponse?>> GetProjectOperationDocuments(
        GetProjectOperationDocumentsRequest request, CT ct);

    Task<Result<GetProjectOperationByParamsResponse?>> GetProjectOperationByParams(
        GetProjectOperationByParamsRequest request, CT ct);

    Task<Result<GetsByEmployerContractResponse?>> GetsByEmployerContract(
        GetsByEmployerContractRequest request, CT ct);

    Task<Result<GetsByProjectResponse?>> GetsByProject(
        GetsByProjectRequest request, CT ct);

    Task<Result<GetsFilteredForReportsResponse?>> GetsFilteredForReports(
        GetsFilteredForReportsRequest request, CT ct);

    Task<Result<GetFltrPOForReportsResponse?>> GetFltrPOForReports(
        GetFltrPOForReportsRequest request, CT ct);

    Task<Result<GetsFilteredByProjectResponse?>> GetsFilteredByProject(
        GetsFilteredByProjectRequest request, CT ct);

    Task<Result<GetsProjectOperationByProjectIdsResponse?>> GetsProjectOperationByProjectIds(
        GetsProjectOperationByProjectIdsRequest request, CT ct);

    Task<Result<GetsFilteredByProjectIdsResponse?>> GetsFilteredByProjectIds(
        GetsFilteredByProjectIdsRequest request, CT ct);

    Task<Result<GetsPrioritizeProjectOperationResponse?>> GetsPrioritizeProjectOperation(
        GetsPrioritizeProjectOperationRequest request, CT ct);

    Task<Result<GetsWithoutContractResponse?>> GetsWithoutContract(
        GetsWithoutContractRequest request, CT ct);

    Task<Result<GetsForEmployerStatusStatementResponse?>> GetsForEmployerStatusStatement(
        GetsForEmployerStatusStatementRequest request, CT ct);

    Task<Result<GetsProjectOperationStatusTypeResponse?>> GetsProjectOperationStatusType(
        GetsProjectOperationStatusTypeRequest request, CT ct);

    Task<Result<GetsByOperationInfoResponse?>> GetsByOperationInfo(
        GetsByOperationInfoRequest request, CT ct);

    Task<Result<GetsProposedPriceResponse?>> GetsProposedPrice(
        GetsProposedPriceRequest request, CT ct);

    Task<Result<GetsForPricingResponse?>> GetsForPricing(
        GetsForPricingRequest request, CT ct);

    Task<Result<SetProjectOperationPriorityResponse?>> SetProjectOperationPriority(
        SetProjectOperationPriorityRequest request, CT ct);

    Task<Result<GetsByProjectOperationIdResponse?>> GetsByProjectOperationId(
        GetsByProjectOperationIdRequest request, CT ct);

    Task<Result<GetsSummaryProjectOperationResponse?>> GetsSummaryProjectOperation(
        GetsSummaryProjectOperationRequest request, CT ct);

    Task<Result<GetsForDailyProjectOperationsResponse?>> GetsForDailyProjectOperations(
        GetsForDailyProjectOperationsRequest request, CT ct);

    Task<Result<ProjectOperationWorkloadManagementResponse?>> ProjectOperationWorkloadManagement(
        ProjectOperationWorkloadManagementRequest request, CT ct);

    Task<Result<GetsSummarizedProjectOperationResponse?>> GetsSummarizedProjectOperation(
        GetsSummarizedProjectOperationRequest request, CT ct);

    Task<Result<GetsProjectOperationByIdsResponse?>> GetsProjectOperationByIds(
        GetsProjectOperationByIdsRequest request, CT ct);

    Task<Result<GetsProjectOperationReportingResponse?>> GetsProjectOperationReporting(
        GetsProjectOperationReportingRequest request, CT ct);

    Task<Result<GetsProjectOperationReportingExcelEnumResponse?>> GetsProjectOperationReportingExcelEnum(
        GetsProjectOperationReportingExcelEnumRequest request, CT ct);

    Task<Result<GetsProjectOperationReportingExcelExporterResponse?>> GetsProjectOperationReportingExcelExporter(
        GetsProjectOperationReportingExcelExporterRequest request, CT ct);

    Task<Result<GetsTotalProjectOperationReportingResponse?>> GetsTotalProjectOperationReporting(
        GetsTotalProjectOperationReportingRequest request, CT ct);

    Task<Result<GetsProjectOperationEmployerReportingResponse?>> GetsProjectOperationEmployerReporting(
        GetsProjectOperationEmployerReportingRequest request, CT ct);

    Task<Result<GetsProjectOperationEmployerReportingExcelExporterResponse?>> GetsProjectOperationEmployerReportingExcelExporter(
        GetsProjectOperationEmployerReportingExcelExporterRequest request, CT ct);

    Task<Result<GetsProjectOperationDailyReportingResponse?>> GetsProjectOperationDailyReporting(
        GetsProjectOperationDailyReportingRequest request, CT ct);

    Task<Result<GetsProjectOperationDailyReportingExcelExporterResponse?>> GetsProjectOperationDailyReportingExcelExporter(
        GetsProjectOperationDailyReportingExcelExporterRequest request, CT ct);

    Task<Result<GetFltrBasePricedPOsResponse?>> GetFltrBasePricedPOs(
        GetFltrBasePricedPOsRequest request, CT ct);

    Task<Result<UpdatePriceResponse?>> UpdateProjectOperationPrice(
        UpdatePriceRequest request, CT ct);

    Task<Result<GetFltrProjectOperationResponse?>> GetFltrProjectOperation(
        GetFltrProjectOperationRequest request, CT ct);

    Task<Result<GetPOActionByPOIdResponse?>> GetPOActionByPOId(
        GetPOActionByPOIdRequest request, CT ct);

    Task<Result<GetProjectOperationProgressResponse?>> GetProjectOperationProgress(
        GetProjectOperationProgressRequest request, CT ct);

    Task<Result<SetPlannedDateResponse?>> SetPlannedDate(
        SetPlannedDateRequest request, CT ct);

    Task<Result<GetPODateResponse?>> GetPODate(
        GetPODateRequest request, CT ct);

    Task<Result<GetCriticalPOResponse?>> GetCriticalPO(
        GetCriticalPORequest request, CT ct);
}