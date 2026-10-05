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
using Engineering.Application.Services.ProjectOperationDetails.Models.PODetailExcelImport;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailCodeCreator;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailGroupDelete;
using Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;
using Engineering.Application.Services.ProjectOperationDetails.Models.SetProjectOperationDetailPriority;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailComment;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailVolumes;
using Engineering.Application.Services.ProjectOperationDetails.Models.UpdatesProjectOperationDetailDate;

namespace Engineering.Application.Services.ProjectOperationDetails;

public interface IProjectOperationDetailLogic
{
    Task<Result<CreateProjectOperationDetailResponse?>> CreateProjectOperationDetail(
        CreateProjectOperationDetailRequest request, CT ct);

    Task<Result<ProjectOperationDetailCodeCreatorResponse?>> ProjectOperationDetailCodeCreator(
        ProjectOperationDetailCodeCreatorRequest request, CT ct);

    Task<Result<CreateVizardProjectOperationDetailResponse?>> CreateVizardProjectOperationDetail(
        CreateVizardProjectOperationDetailRequest request, CT ct);

    Task<Result<PODetailExcelImportResponse>> PODetailExcelImport(
        PODetailExcelImportRequest request, CT ct);

    Task<Result<UpdateProjectOperationDetailResponse?>> UpdateProjectOperationDetail(
        UpdateProjectOperationDetailRequest request, CT ct);

    Task<Result<UpdateProjectOperationDetailsResponse?>> UpdateProjectOperationDetails(
        UpdateProjectOperationDetailsRequest request, CT ct);

    Task<Result<UpdateProjectOperationDetailVolumesResponse?>> UpdateProjectOperationDetailVolumes(
        UpdateProjectOperationDetailVolumesRequest request, CT ct);

    Task<Result<UpdatesProjectOperationDetailDateResponse?>> UpdatesProjectOperationDetailDate(
        UpdatesProjectOperationDetailDateRequest request, CT ct);

    Task<Result<DeleteProjectOperationDetailResponse?>> DeleteProjectOperationDetail(
        DeleteProjectOperationDetailRequest request, CT ct);

    Task<Result<ProjectOperationDetailGroupDeleteResponse?>> ProjectOperationDetailGroupDelete(
        ProjectOperationDetailGroupDeleteRequest request, CT ct);

    Task<Result<SetProjectOperationDetailPriorityResponse?>> SetProjectOperationDetailPriority(
        SetProjectOperationDetailPriorityRequest request, CT ct);

    Task<Result<ProjectOperationDetailStatusChangerResponse?>> ProjectOperationDetailStatusChanger(
        ProjectOperationDetailStatusChangerRequest request, CT ct);

    Task<Result<GroupProjectOperationDetailStatusChangerResponse?>> GroupProjectOperationDetailStatusChanger(
        GroupProjectOperationDetailStatusChangerRequest request, CT ct);

    Task<Result<GetProjectOperationDetailByIdResponse?>> GetProjectOperationDetailById(
        GetProjectOperationDetailByIdRequest request, CT ct);

    Task<Result<GetProjectOperationDetailByCodeResponse?>> GetProjectOperationDetailByCode(
        GetProjectOperationDetailByCodeRequest request, CT ct);

    Task<Result<GetProjectOperationDetailDoneVolumeResponse?>> GetProjectOperationDetailDoneVolume(
        GetProjectOperationDetailDoneVolumeRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailByProjectOperationIdResponse?>> GetsProjectOperationDetailByProjectOperationId(
        GetsProjectOperationDetailByProjectOperationIdRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailByExpertIdResponse?>> GetsProjectOperationDetailByExpertId(
        GetsProjectOperationDetailByExpertIdRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailByMachineryIdResponse?>> GetsProjectOperationDetailByMachineryId(
        GetsProjectOperationDetailByMachineryIdRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailByProductIdResponse?>> GetsProjectOperationDetailByProductId(
        GetsProjectOperationDetailByProductIdRequest request, CT ct);

    Task<Result<GetsConsumableVolumesResponse?>> GetsConsumableVolumes(
        GetsConsumableVolumesRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailStatusResponse?>> GetsProjectOperationDetailStatus(
        GetsProjectOperationDetailStatusRequest request, CT ct);

    Task<Result<GetsSummarizedByProjectOperationIdsResponse?>> GetsSummarizedByProjectOperationIds(
        GetsSummarizedByProjectOperationIdsRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailForSchedulingResponse?>> GetsProjectOperationDetailForScheduling(
        GetsProjectOperationDetailForSchedulingRequest request, CT ct);

    Task<Result<GetsMinimalByProjectOperationIdsResponse?>> GetsMinimalByProjectOperationIds(
        GetsMinimalByProjectOperationIdsRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailByIdsResponse?>> GetsProjectOperationDetailByIds(
        GetsProjectOperationDetailByIdsRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailReportingResponse?>> GetsProjectOperationDetailReporting(
        GetsProjectOperationDetailReportingRequest request, CT ct);

    Task<Result<GetProjectOperationDetailContractorsResponse?>> GetProjectOperationDetailContractors(
        GetProjectOperationDetailContractorsRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailByContractorIdsResponse?>> GetsProjectOperationDetailByContractorIds(
        GetsProjectOperationDetailByContractorIdsRequest request, CT ct);

    Task<Result<GetsContractorProjectOperationDetailReportsResponse?>> GetsContractorProjectOperationDetailReports(
        GetsContractorProjectOperationDetailReportsRequest request, CT ct);

    Task<Result<GetsContractorProjectOperationDetailDetailReportsResponse?>> GetsContractorProjectOperationDetailDetailReports(
        GetsContractorProjectOperationDetailDetailReportsRequest request, CT ct);
       
    Task<Result<GetsByProjectOperationIdExcelExporterResponse?>> GetsByProjectOperationIdExcelExporter(
        GetsByProjectOperationIdExcelExporterRequest request, CT ct);

    Task<Result<GetsByProjectOperationIdExcelEnumsResponse?>> GetsByProjectOperationIdExcelEnums(
        GetsByProjectOperationIdExcelEnumsRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailReportingExcelEnumResponse?>> GetsProjectOperationDetailReportingExcelEnum(
        GetsProjectOperationDetailReportingExcelEnumRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailReportingExcelExporterResponse?>> GetsProjectOperationDetailReportingExcelExporter(
        GetsProjectOperationDetailReportingExcelExporterRequest request, CT ct);

    Task<Result<GetsContractorReportsExcelEnumResponse?>> GetsContractorReportsExcelEnum(
        GetsContractorReportsExcelEnumRequest request, CT ct);

    Task<Result<GetsContractorReportsExcelExporterResponse?>> GetsContractorReportsExcelExporter(
        GetsContractorReportsExcelExporterRequest request, CT ct);

    Task<Result<GetsContractorDetailReportsExcelEnumResponse?>> GetsContractorDetailReportsExcelEnum(
        GetsContractorDetailReportsExcelEnumRequest request, CT ct);

    Task<Result<GetsContractorDetailReportsExcelExporterResponse?>> GetsContractorDetailReportsExcelExporter(
        GetsContractorDetailReportsExcelExporterRequest request, CT ct);

    Task<Result<GetsProjectOperationDetailDocumentResponse?>> GetsProjectOperationDetailDocument(
        GetsProjectOperationDetailDocumentRequest request, CT ct);

    Task<Result<GetTotalsByProjectOperationIdResponse?>> GetTotalsByProjectOperationId(
        GetTotalsByProjectOperationIdRequest request, CT ct);

    Task<Result<GetHistoryByProjectOperationDetailIdResponse?>> GetHistoryByProjectOperationDetailId(
        GetHistoryByProjectOperationDetailIdRequest request, CT ct);

    Task<Result<GetTotalsProjectOperationDetailReportResponse?>> GetTotalsProjectOperationDetailReport(
        GetTotalsProjectOperationDetailReportRequest request, CT ct);

    Task<Result<GetFilteredProjectOperationDetailsResponse?>> GetsFilteredDetailsByCostCenterId(
        GetFilteredProjectOperationDetailsRequest request, CT ct);

    Task<Result<GetProjectContractorsResponse?>> GetProjectContractors(
        GetProjectContractorsRequest request, CT ct);
}