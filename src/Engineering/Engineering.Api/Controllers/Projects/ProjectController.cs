using Engineering.Api.Controllers.Projects.Reports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;
using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Application.Services.Projects;
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
using Engineering.Domain.Entities.Projects.Enums;

[ApiController]
[Route("api/engineering/v1/Project")]
public class ProjectController : ControllerBase
{
    private readonly IProjectLogic _logic;
    private readonly GetPdfProjectContractsHandle _pdfHandle;
    private readonly GetPdfProjectContractorHumanResourcesHandle _contractorHumanResourcepdfHandle;
    private readonly GetPdfProjectEmployerHumanResourcesHandle _employerHumanResourcepdfHandle;
    private readonly GetPdfProjectByIdHandle _pdfProjectHandle;

    public ProjectController(IProjectLogic logic,
        GetPdfProjectContractsHandle pdfHandle,
        GetPdfProjectByIdHandle pdfProjectHandle,
        GetPdfProjectContractorHumanResourcesHandle contractorHumanResourcepdfHandle,
        GetPdfProjectEmployerHumanResourcesHandle employerHumanResourcepdfHandle)
    {
        _logic = logic;
        _pdfHandle = pdfHandle;
        _contractorHumanResourcepdfHandle = contractorHumanResourcepdfHandle;
        _employerHumanResourcepdfHandle = employerHumanResourcepdfHandle;
        _pdfProjectHandle = pdfProjectHandle;
    }

    [HttpPost("AddProject")]
    [ResponseSchema<CreateProjectResponse>]
    public async Task<IResult> AddProject([FromBody] CreateProjectRequest request, CT ct)
    {
        var result = await _logic.CreateProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectThirdParty")]
    [ResponseSchema<CreateProjectResponse>]
    public async Task<IResult> CreateProjectThirdParty([FromBody] CreateProjectThirdPartyRequest request, CT ct)
    {
        var result = await _logic.CreateProjectThirdParty(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectProduct")]
    [ResponseSchema<CreateProjectProductResponse>]
    public async Task<IResult> CreateProjectProduct([FromBody] CreateProjectProductRequest request, CT ct)
    {
        var result = await _logic.CreateProjectProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateProjectProduct")]
    [ResponseSchema<UpdateProjectProductResponse>]
    public async Task<IResult> UpdateProjectProduct([FromBody] UpdateProjectProductRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteProjectThirdParty")]
    [ResponseSchema<DeleteProjectProductResponse>]
    public async Task<IResult> DeleteProjectThirdParty([FromBody] DeleteProjectThirdPartyRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectThirdParty(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteProjectProduct")]
    [ResponseSchema<DeleteProjectProductResponse>]
    public async Task<IResult> DeleteProjectProduct([FromBody] DeleteProjectProductRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectProduct(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("NewProjectCode")]
    [ResponseSchema<CreateProjectCodeResponse>]
    public async Task<IResult> NewProjectCode([FromBody] CreateProjectCodeRequest request, CT ct)
    {
        var result = await _logic.CreateProjectCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("FindLastUnitOrg")]
    [ResponseSchema<FindLastUnitOrgResponse>]
    public async Task<IResult> FindLastUnitOrg([FromBody] FindLastUnitOrgRequest request, CT ct)
    {
        var result = await _logic.FindLastUnitOrg(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ProjectGroupDelete")]
    [ResponseSchema<ProjectGroupDeleteResponse>]
    public async Task<IResult> ProjectGroupDelete([FromBody] ProjectGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.ProjectGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateProjects")]
    [ResponseSchema<StateChangerProjectsResponse>]
    public async Task<IResult> ActivateProjects([FromBody] ActivateProjectsRequest request, CT ct)
    {
        var result = await _logic.StateChangerProjects(new(request.Ids, null, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateProjects")]
    [ResponseSchema<StateChangerProjectsResponse>]
    public async Task<IResult> InactivateProjects([FromBody] InactivateProjectsRequest request, CT ct)
    {
        var result = await _logic.StateChangerProjects(new(request.Ids, null, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProject")]
    [ResponseSchema<UpdateProjectResponse>]
    public async Task<IResult> UpdateProject([FromBody] UpdateProjectRequest request, CT ct)
    {
        var result = await _logic.UpdateProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveProject")]
    [ResponseSchema<ActiveProjectResponse>]
    public async Task<IResult> ActiveProject([FromBody] ActiveProjectRequest request, CT ct)
    {
        var result = await _logic.ActiveProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveProject")]
    [ResponseSchema<InactiveProjectResponse>]
    public async Task<IResult> InactiveProject([FromBody] InactiveProjectRequest request, CT ct)
    {
        var result = await _logic.InactiveProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("GroupProjectStatusChanger")]
    [ResponseSchema<GroupProjectStatusChangerResponse>]
    public async Task<IResult> GroupProjectStatusChanger([FromBody] GroupProjectStatusChangerRequest request, CT ct)
    {
        var result = await _logic.GroupProjectStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ProjectStatusChanger")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> ProjectStatusChanger([FromBody] ProjectStatusChangerRequest request, CT ct)
    {
        var result = await _logic.ProjectStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToCanceled")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToCanceled([FromBody] SetProjectStatusToCanceledRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.Canceled);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToClosed")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToClosed([FromBody] SetProjectStatusToClosedRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.Closed);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToDefiniteDelivery")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToDefiniteDelivery([FromBody] SetProjectStatusToDefiniteDeliveryRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.DefiniteDelivery);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToDoing")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToDoing([FromBody] SetProjectStatusToDoingRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.Doing);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToEndOfWork")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToEndOfWork([FromBody] SetProjectStatusToEndOfWorkRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.EndOfWork);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToNotStarted")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToNotStarted([FromBody] SetProjectStatusToNotStartedRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.NotStarted);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToStopped")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToStopped([FromBody] SetProjectStatusToStoppedRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.Stopped);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetProjectStatusToTemporaryDelivery")]
    [ResponseSchema<ProjectStatusChangerResponse>]
    public async Task<IResult> SetProjectStatusToTemporaryDelivery([FromBody] SetProjectStatusToTemporaryDeliveryRequest request, CT ct)
    {
        var requestModel = new ProjectStatusChangerRequest(request.Id, ProjectStatus.TemporaryDelivery);
        var result = await _logic.ProjectStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetManagerToProjects")]
    [ResponseSchema<SetManagerToProjectsResponse>]
    public async Task<IResult> SetManagerToProjects([FromBody] SetManagerToProjectsRequest request, CT ct)
    {
        var result = await _logic.SetManagerToProjects(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectById")]
    [ResponseSchema<GetProjectByIdResponse>]
    public async Task<IResult> GetProjectById([FromQuery] GetProjectByIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFltrProjectThirdParty")]
    [ResponseSchema<GetFltrProjectThirdPartyResponse>]
    public async Task<IResult> GetFltrProjectThirdParty([FromQuery] GetFltrProjectThirdPartyRequest request, CT ct)
    {
        var result = await _logic.GetFltrProjectThirdParty(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectCategoryProductByProjectId")]
    [ResponseSchema<GetProjectCategoryProductByProjectIdResponse>]
    public async Task<IResult> GetProjectCategoryProductByProjectId([FromQuery] GetProjectCategoryProductByProjectIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectCategoryProductByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSummarizedProjectById")]
    [ResponseSchema<GetSummarizedProjectByIdResponse>]
    public async Task<IResult> GetSummarizedProjectById([FromQuery] GetSummarizedProjectByIdRequest request, CT ct)
    {
        var result = await _logic.GetSummarizedProjectById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectProductByProjectId")]
    [ResponseSchema<GetProjectProductByProjectIdResponse>]
    public async Task<IResult> GetProjectProductByProjectId([FromQuery] GetProjectProductByProjectIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectProductByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectByName")]
    [ResponseSchema<GetProjectByNameResponse>]
    public async Task<IResult> GetProjectByName([FromQuery] GetProjectByNameRequest request, CT ct)
    {
        var result = await _logic.GetProjectByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectByCode")]
    [ResponseSchema<GetProjectByCodeResponse>]
    public async Task<IResult> GetProjectByCode([FromQuery] GetProjectByCodeRequest request, CT ct)
    {
        var result = await _logic.GetProjectByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectProductCategoryByCostCenterId")]
    [ResponseSchema<GetProjectProductCategoryByCostCenterIdResponse>]
    public async Task<IResult> GetProjectProductCategoryByCostCenterId([FromQuery] GetProjectProductCategoryByCostCenterIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectProductCategoryByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectProductCategoryByProjectId")]
    [ResponseSchema<GetProjectProductCategoryByProjectIdResponse>]
    public async Task<IResult> GetProjectProductCategoryByProjectId(
        [FromBody] GetProjectProductCategoryByProjectIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectProductCategoryByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectByProjectManagerId")]
    [ResponseSchema<GetsProjectByProjectManagerIdResponse>]
    public async Task<IResult> GetsProjectByProjectManagerId([FromBody] GetsProjectByProjectManagerIdRequest request, CT ct)
    {
        var result = await _logic.GetsProjectByProjectManagerId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveProject")]
    [ResponseSchema<GetActiveProjectsResponse>]
    public async Task<IResult> GetsActiveProject([FromBody] GetActiveProjectsRequest request, CT ct)
    {
        var result = await _logic.GetActiveProjects(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetContractorProjects")]
    [ResponseSchema<GetContractorProjectsResponse>]
    public async Task<IResult> GetContractorProjects([FromBody] GetContractorProjectsRequest request, CT ct)
    {
        var result = await _logic.GetContractorProjects(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveProjectByCostCenterIds")]
    [ResponseSchema<GetsActiveProjectByCostCenterIdsResponse>]
    public async Task<IResult> GetsActiveProjectByCostCenterIds([FromBody] GetsActiveProjectByCostCenterIdsRequest request, CT ct)
    {
        var result = await _logic.GetsActiveProjectByCostCenterIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProject")]
    [ResponseSchema<GetProjectsResponse>]
    public async Task<IResult> GetsProject([FromBody] GetProjectsRequest request, CT ct)
    {
        var result = await _logic.GetProjects(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsByNameOrCode")]
    [ResponseSchema<GetsByNameOrCodeResponse>]
    public async Task<IResult> GetsByNameOrCode([FromBody] GetsByNameOrCodeRequest request, CT ct)
    {
        var result = await _logic.GetsByNameOrCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectByCostCenterId")]
    [ResponseSchema<GetProjectsByCostCenterResponse>]
    public async Task<IResult> GetsProjectByCostCenterId([FromBody] GetProjectsByCostCenterRequest request, CT ct)
    {
        var result = await _logic.GetProjectsByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsByEmployerId")]
    [ResponseSchema<GetsProjectByEmployerIdResponse>]
    public async Task<IResult> GetsByEmployerId([FromBody] GetsProjectByEmployerIdRequest request, CT ct)
    {
        var result = await _logic.GetsByEmployerId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectStatus")]
    [ResponseSchema<GetsProjectStatusResponse>]
    public async Task<IResult> GetsProjectStatus([FromQuery] GetsProjectStatusRequest request, CT ct)
    {
        var result = await _logic.GetsProjectStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectPOTimelines")]
    [ResponseSchema<GetProjectPOTimelinesResponse>]
    public async Task<IResult> GetProjectPOTimelines([FromQuery] GetProjectPOTimelinesRequest request, CT ct)
    {
        var result = await _logic.GetProjectPOTimelines(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectProgress")]
    [ResponseSchema<GetProjectProgressResponse>]
    public async Task<IResult> GetProjectProgress([FromQuery] GetProjectProgressRequest request, CT ct)
    {
        var result = await _logic.GetProjectProgress(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetUnAssignedProjects")]
    [ResponseSchema<GetUnAssignedProjectsResponse>]
    public async Task<IResult> GetUnAssignedProjects([FromBody] GetUnAssignedProjectsRequest request, CT ct)
    {
        var result = await _logic.GetUnAssignedProjects(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AssignProjectsToCostCenter")]
    [ResponseSchema<AssignProjectsToCostCenterResponse>]
    public async Task<IResult> AssignProjectsToCostCenter([FromBody] AssignProjectsToCostCenterRequest request, CT ct)
    {
        var result = await _logic.AssignProjectsToCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractedProject")]
    [ResponseSchema<GetsContractedProjectResponse>]
    public async Task<IResult> GetsContractedProject([FromBody] GetsContractedProjectRequest request, CT ct)
    {
        var result = await _logic.GetsContractedProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectByIds")]
    [ResponseSchema<GetsProjectByIdsResponse>]
    public async Task<IResult> GetsProjectByIds([FromBody] GetsProjectByIdsRequest request, CT ct)
    {
        var result = await _logic.GetsProjectByIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectSorting")]
    [ResponseSchema<GetsProjectSortingResponse>]
    public async Task<IResult> GetsProjectSorting([FromBody] GetsProjectSortingRequest request, CT ct)
    {
        var result = await _logic.GetsProjectSorting(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsProjectExcelExporter")]
    [ResponseSchema<GetsProjectExcelExporterResponse>]
    public async Task<IResult> GetsProjectExcelExporter([FromBody] GetsProjectExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsProjectExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectExcelEnum")]
    [ResponseSchema<GetsProjectExcelEnumResponse>]
    public async Task<IResult> GetsProjectExcelEnum([FromQuery] GetsProjectExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsProjectExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectHistory")]
    [ResponseSchema<GetProjectHistoryResponse>]
    public async Task<IResult> GetProjectHistory([FromQuery] GetProjectHistoryRequest request, CT ct)
    {
        var result = await _logic.GetProjectHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectProductGroupByCostCenterId")]
    [ResponseSchema<GetProjectProductGroupByCostCenterIdResponse>]
    public async Task<IResult> GetProjectProductGroupByCostCenterId([FromQuery] GetProjectProductGroupByCostCenterIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectProductGroupByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectProductGroupByProjectId")]
    [ResponseSchema<GetProjectProductGroupByProjectIdResponse>]
    public async Task<IResult> GetProjectProductGroupByProjectId(
        [FromBody] GetProjectProductGroupByProjectIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectProductGroupByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProject")]
    [ResponseSchema<DeleteProjectResponse>]
    public async Task<IResult> DeleteProject([FromQuery] DeleteProjectRequest request, CT ct)
    {
        var result = await _logic.DeleteProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetPdfProjectContracts")]
    public async Task<IResult> GetPdfProjectContracts(
        [FromBody] GetContractsByProjectIdRequest request,
        CT ct)
    {
        var result = await _pdfHandle.Handle(request, ct);
        return result;
    }

    [HttpPost("GetPdfProjectContractorHumanResources")]
    public async Task<IResult> GetPdfProjectContractorHumanResources(
        [FromBody] GetCCThirdPartiesRequest request,
        CT ct)
    {
        var result = await _contractorHumanResourcepdfHandle.Handle(request, ct);
        return result;
    }

    [HttpPost("GetPdfProjectEmployerHumanResourcesHandle")]
    public async Task<IResult> GetPdfProjectEmployerHumanResourcesHandle(
        [FromBody] GetECThirdPartiesRequest request,
        CT ct)
    {
        var result = await _employerHumanResourcepdfHandle.Handle(request, ct);
        return result;
    }

    [HttpPost("GetPdfProjectById")]
    public async Task<IResult> GetPdfProjectById(
        [FromBody] GetPdfProjectByIdRequest request,
        CT ct)
    {
        var result = await _pdfProjectHandle.Handle(new GetProjectByIdRequest(request.Id), ct);
        return result;
    }

}
