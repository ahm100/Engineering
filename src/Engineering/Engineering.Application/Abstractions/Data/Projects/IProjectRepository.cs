using Engineering.Application.Services.Projects.Models.GetProjectForPdf;
using Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.Services.Projects.Queries.GetProjectModelById;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<bool> ExistsProject(long projectId, CT ct);

    Task<bool> ExistsProject(long projectId, long? companyId, CT ct);

    Task<bool> ExistsProjects(List<long> projectIds, CT ct);

    Task<bool> ExistsProjects(List<long> projectIds, long? companyId, CT ct);

    Task<Project?> GetById(
        long id,
        CT ct);

    Task<Project?> GetProjectByIdIncludeLess(
        long id,
        CT ct);

    Task<GetProjectModelByIdReponse?> GetProjectModelById(
        long id,
        CT ct);

    Task<Project?> GetForChangeStatus(
        long id,
        CT ct);

    Task<Project?> FindByIdAndChild(
        long id,
        CT ct);

    Task<Project?> GetProjectByIdNoIncluding(
        long id,
        CT ct);

    Task<Project?> GetSummarizedProjectById(
        long id,
        CT ct);

    Task<Project?> HaveProjectChild(
        long id,
        CT ct);

    Task<long> FindLastProject(
        string prefix,
        CT ct);

    Task<long> FindLastProject(
        long employerId,
        long? costCenterId,
        CT ct);

    Task<long> FindLastUnitOrg(
        long organizationId, CT ct);

    Task<Project?> FindByName(
        string name,
        long? costCenterId,
        long? companyId,
        CT ct);

    Task<Project?> FindByCode(
        string code,
        long? costCenterId,
        long? companyId,
        CT ct);

    Task<List<Project>?> GetByCodes(
        List<string> codes,
        long? companyId,
        bool haveCostCenter,
        bool isOrganizationUnit,
        List<ProjectStatus>? statuses,
        CT ct);

    Task<(List<GetProjectsModel> Data, int RowCount)> GetProjects(
        List<long>? ids,
        string? filterData,
        ProjectStatus? status,
        List<long>? categoryIds,
        List<long>? costCenterIds,
        long? advisorId,
        long? projectManagerId,
        long? planningAssistantId,
        long? thirdPartyId,
        long? supervisorEngineerId,
        long? employerId,
        long? projectTypeId,
        long? implementationAssistantId,
        long? technicalAssistantId,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        List<ProjectStatus>? statuses,
        bool checkThirdParty,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetsContractedProject(
        List<long>? costCenterIds,
        ProjectStatus? status,
        string? filterData,
        long? employerId,
        long? projectTypeId,
        long? categoryId,
        bool? isActive,
        string[]? orderBy,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetProjectsByCostCenter(
        long costCenterId,
        string? filterData,
        List<ProjectStatus>? statuses,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetActiveProjects(
        string? filterData,
        long? employerId,
        long? costCenterId,
        long? projectTypeId,
        long? categoryId,
        long? projectManagerId,
        long? planningAssistantId,
        long? thirdPartyId,
        long? supervisorEngineerId,
        long? implementationAssistantId,
        long? technicalAssistantId,
        long? advisorId,
        long? companyId,
        bool? contractual,
        List<ProjectStatus>? statuses,
        bool checkThirdParty,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetContractorProjects(
        long contractorId,
        long? costCenterId,
        string? filterData,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetsActiveProjectByCostCenterIds(
        string? filterData,
        long? employerId,
        List<long>? costCenterIds,
        long? projectTypeId,
        long? categoryId,
        long? projectManagerId,
        long? planningAssistantId,
        long? supervisorEngineerId,
        long? implementationAssistantId,
        long? technicalAssistantId,
        long? advisorId,
        long? companyId,
        bool? contractual,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetsByNameOrCode(
        string filterData,
        long? companyId,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetsByEmployerId(
        long employerId,
        string? filterData,
        long? companyId,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetsProjectByProjectManagerId(
        List<long> costCenterIds,
        long projectManagerId,
        string? filterData,
        long? companyId,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<Project> Data, int RowCount)> GetsProjectByIds(
        List<long>? ids,
        string? filterData,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<Project>> GetProjectByIds(
        List<long> ids,
        bool haveCostCenter,
        bool isOrganizationUnit,
        CT ct);

    Task<Project?> GetProjectById(
            long id, CT ct);

    IQueryable<Project> GetsProjectSorting(
        string? filterData,
        ProjectStatus? status,
        bool? isActive,
        long? companyId,
        bool haveCostCenter,
        bool isOrganizationUnit,
        List<ProjectStatus>? statuses,
        CT ct);

    Task<GetProjectForPdfResponse?> GetProjectForPdf(
        long id,
        CT ct);

    Task<(List<GetUnAssignedProjectsModel>? Data, int RowCount)> GetUnAssignedProjects(
        string? filterData,
        long? cityId,
        int pageIndex,
        int pageSize, CT ct);
}
