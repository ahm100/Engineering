using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Application.Services.Projects.Models.ActiveProject;
using Engineering.Application.Services.Projects.Models.AddAuthorizedThirdPartyToProject;
using Engineering.Application.Services.Projects.Models.AssignProjectsToCostCenter;
using Engineering.Application.Services.Projects.Models.CreateProject;
using Engineering.Application.Services.Projects.Models.CreateProjectCode;
using Engineering.Application.Services.Projects.Models.CreateProjectProduct;
using Engineering.Application.Services.Projects.Models.Delete;
using Engineering.Application.Services.Projects.Models.DeleteProjectProduct;
using Engineering.Application.Services.Projects.Models.DeleteProjectThirdParty;
using Engineering.Application.Services.Projects.Models.FindLastUnitOrg;
using Engineering.Application.Services.Projects.Models.GetActiveProjects;
using Engineering.Application.Services.Projects.Models.GetContractorProjects;
using Engineering.Application.Services.Projects.Models.GetProjectByCode;
using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.Projects.Models.GetProjectByName;
using Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectForPdf;
using Engineering.Application.Services.Projects.Models.GetProjectHistory;
using Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;
using Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByCostCenterId;
using Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;
using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectProgress;
using Engineering.Application.Services.Projects.Models.GetProjects;
using Engineering.Application.Services.Projects.Models.GetProjectsByCostCenter;
using Engineering.Application.Services.Projects.Models.GetProjectThirdParties;
using Engineering.Application.Services.Projects.Models.GetsActiveProjectByCostCenterIds;
using Engineering.Application.Services.Projects.Models.GetsByEmployerId;
using Engineering.Application.Services.Projects.Models.GetsByNameOrCode;
using Engineering.Application.Services.Projects.Models.GetsContractedProject;
using Engineering.Application.Services.Projects.Models.GetsProjectByIds;
using Engineering.Application.Services.Projects.Models.GetsProjectByProjectManagerId;
using Engineering.Application.Services.Projects.Models.GetsProjectExcelEnum;
using Engineering.Application.Services.Projects.Models.GetsProjectExcelExporter;
using Engineering.Application.Services.Projects.Models.GetsProjectSorting;
using Engineering.Application.Services.Projects.Models.GetSummarizedProjectById;
using Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;
using Engineering.Application.Services.Projects.Models.GroupProjectStatusChanger;
using Engineering.Application.Services.Projects.Models.InactiveProject;
using Engineering.Application.Services.Projects.Models.ProjectGroupDelete;
using Engineering.Application.Services.Projects.Models.ProjectStatusChanger;
using Engineering.Application.Services.Projects.Models.SetManagerToProjects;
using Engineering.Application.Services.Projects.Models.StateChangerProjects;
using Engineering.Application.Services.Projects.Models.UpdateProject;
using Engineering.Application.Services.Projects.Models.UpdateProjectProduct;
using Engineering.Application.Services.Projects.Models.UpdateProjectProductQuantities;

namespace Engineering.Application.Services.Projects;

public interface IProjectLogic
{
    ///Commands
    Task<Result<CreateProjectResponse?>> CreateProject(
        CreateProjectRequest request, CT ct);

    Task<Result<CreateProjectThirdPartyResponse?>> CreateProjectThirdParty(
        CreateProjectThirdPartyRequest request, CT ct);

    Task<Result<CreateProjectCodeResponse?>> CreateProjectCode(
        CreateProjectCodeRequest request, CT ct);

    Task<Result<FindLastUnitOrgResponse?>> FindLastUnitOrg(
        FindLastUnitOrgRequest request, CT ct);

    Task<Result<CreateProjectProductResponse?>> CreateProjectProduct(
        CreateProjectProductRequest request, CT ct);

    Task<Result<UpdateProjectProductResponse?>> UpdateProjectProduct(
        UpdateProjectProductRequest request, CT ct);

    Task<Result<UpdateProjectProductQuantitiesResponse?>> UpdateProjectProductQuantity(
       UpdateProjectProductQuantitiesRequest request, CT ct);

    Task<Result<DeleteProjectProductResponse?>> DeleteProjectProduct(
        DeleteProjectProductRequest request, CT ct);

    Task<Result<DeleteProjectThirdPartyResponse?>> DeleteProjectThirdParty(
        DeleteProjectThirdPartyRequest request, CT ct);

    Task<Result<UpdateProjectResponse?>> UpdateProject(
        UpdateProjectRequest request, CT ct);

    Task<Result<InactiveProjectResponse?>> InactiveProject(
        InactiveProjectRequest request, CT ct);

    Task<Result<ActiveProjectResponse?>> ActiveProject(
        ActiveProjectRequest request, CT ct);

    Task<Result<DeleteProjectResponse?>> DeleteProject(
        DeleteProjectRequest request, CT ct);

    Task<Result<ProjectStatusChangerResponse?>> ProjectStatusChanger(
        ProjectStatusChangerRequest request, CT ct);

    Task<Result<StateChangerProjectsResponse?>> StateChangerProjects(
        StateChangerProjectsRequest request, CT ct);

    Task<Result<GroupProjectStatusChangerResponse?>> GroupProjectStatusChanger(
        GroupProjectStatusChangerRequest request, CT ct);

    Task<Result<ProjectGroupDeleteResponse?>> ProjectGroupDelete(
        ProjectGroupDeleteRequest request, CT ct);

    Task<Result<SetManagerToProjectsResponse?>> SetManagerToProjects(
        SetManagerToProjectsRequest request, CT ct);

    ///Queries
    Task<Result<GetProjectByIdResponse?>> GetProjectById(
        GetProjectByIdRequest request, CT ct);

    Task<Result<GetProjectByNameResponse?>> GetProjectByName(
        GetProjectByNameRequest request, CT ct);

    Task<Result<GetProjectByCodeResponse?>> GetProjectByCode(
        GetProjectByCodeRequest request, CT ct);

    Task<Result<GetProjectProductByProjectIdResponse?>> GetProjectProductByProjectId(
        GetProjectProductByProjectIdRequest request, CT ct);

    Task<Result<GetProjectCategoryProductByProjectIdResponse?>> GetProjectCategoryProductByProjectId(
        GetProjectCategoryProductByProjectIdRequest request, CT ct);

    Task<Result<GetSummarizedProjectByIdResponse?>> GetSummarizedProjectById(
        GetSummarizedProjectByIdRequest request, CT ct);

    Task<Result<GetFltrProjectThirdPartyResponse?>> GetFltrProjectThirdParty(
        GetFltrProjectThirdPartyRequest request, CT ct);

    Task<Result<GetActiveProjectsResponse?>> GetActiveProjects(
        GetActiveProjectsRequest request, CT ct);

    Task<Result<GetsActiveProjectByCostCenterIdsResponse?>> GetsActiveProjectByCostCenterIds(
        GetsActiveProjectByCostCenterIdsRequest request, CT ct);

    Task<Result<GetProjectsResponse?>> GetProjects(
        GetProjectsRequest request, CT ct);

    Task<Result<GetsByNameOrCodeResponse?>> GetsByNameOrCode(
        GetsByNameOrCodeRequest request, CT ct);

    Task<Result<GetProjectsByCostCenterResponse?>> GetProjectsByCostCenterId(
        GetProjectsByCostCenterRequest request, CT ct);

    Task<Result<GetsProjectByEmployerIdResponse?>> GetsByEmployerId(
        GetsProjectByEmployerIdRequest request, CT ct);

    Task<Result<GetsProjectByProjectManagerIdResponse?>> GetsProjectByProjectManagerId(
        GetsProjectByProjectManagerIdRequest request, CT ct);

    Task<Result<GetsProjectStatusResponse?>> GetsProjectStatus(
        GetsProjectStatusRequest request, CT ct);

    Task<Result<GetsContractedProjectResponse?>> GetsContractedProject(
        GetsContractedProjectRequest request, CT ct);

    Task<Result<GetsProjectByIdsResponse?>> GetsProjectByIds(
        GetsProjectByIdsRequest request, CT ct);

    Task<Result<GetsProjectSortingResponse?>> GetsProjectSorting(
        GetsProjectSortingRequest request, CT ct);

    Task<Result<GetContractorProjectsResponse?>> GetContractorProjects(
        GetContractorProjectsRequest request, CT ct);

    Task<Result<GetsProjectExcelEnumResponse?>> GetsProjectExcelEnum(
        GetsProjectExcelEnumRequest request, CT ct);

    Task<Result<GetProjectForPdfResponse?>> GetProjectForPdf(
        GetProjectForPdfRequest request, CT ct);

    Task<Result<GetProjectPOTimelinesResponse?>> GetProjectPOTimelines(
        GetProjectPOTimelinesRequest request, CT ct);

    Task<Result<GetProjectProgressResponse?>> GetProjectProgress(
        GetProjectProgressRequest request, CT ct);

    Task<Result<GetUnAssignedProjectsResponse?>> GetUnAssignedProjects(
        GetUnAssignedProjectsRequest request, CT ct);

    Task<Result<AssignProjectsToCostCenterResponse?>> AssignProjectsToCostCenter(
        AssignProjectsToCostCenterRequest request, CT ct);

    Task<Result<GetsProjectExcelExporterResponse?>> GetsProjectExcelExporter(
        GetsProjectExcelExporterRequest request, CT ct);

    Task<Result<GetProjectHistoryResponse?>> GetProjectHistory(
        GetProjectHistoryRequest request, CT ct);

    Task<Result<GetProjectProductGroupByCostCenterIdResponse?>> GetProjectProductGroupByCostCenterId(
        GetProjectProductGroupByCostCenterIdRequest request, CT ct);

    Task<Result<GetProjectProductCategoryByCostCenterIdResponse?>> GetProjectProductCategoryByCostCenterId(
        GetProjectProductCategoryByCostCenterIdRequest request, CT ct);

    Task<Result<GetProjectProductGroupByProjectIdResponse?>> GetProjectProductGroupByProjectId(
        GetProjectProductGroupByProjectIdRequest request, CT ct);

    Task<Result<GetProjectProductCategoryByProjectIdResponse?>> GetProjectProductCategoryByProjectId(
        GetProjectProductCategoryByProjectIdRequest request, CT ct);
}
