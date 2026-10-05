using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.GetProjectForPdf;
using Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.Services.Projects.Queries.GetProjectModelById;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Microsoft.EntityFrameworkCore.Internal;

namespace Engineering.Persistence.Repositories.Projects;

public partial class ProjectRepository : BaseRepository<EngineeringDBContext, Project>, IProjectRepository
{

    public ProjectRepository(EngineeringDBContext context) : base(context)
    {

    }

    public async Task<bool> ExistsProject(long projectId, CT ct)
    {
        return await DbSet
            .AsNoTracking()
            .AnyAsync(oo => oo.Id == projectId, ct);
    }

    public async Task<bool> ExistsProject(long projectId, long? companyId, CT ct)
    {
        return await DbSet.AnyAsync(
            oo => oo.Id == projectId && (companyId == null || oo.CompanyId == companyId),
            ct);
    }

    public async Task<bool> ExistsProjects(List<long> projectIds, CT ct)
    {
        var count = await DbSet
            .AsNoTracking()
            .CountAsync(oo => projectIds.Contains(oo.Id), ct);
        return count == projectIds.Count;
    }

    public async Task<bool> ExistsProjects(List<long> projectIds, long? companyId, CT ct)
    {
        var count = await DbSet.CountAsync(
            oo => projectIds.Contains(oo.Id) && (companyId == null || oo.CompanyId == companyId),
            ct);
        return count == projectIds.Count;
    }

    public async Task<Project?> GetById(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.ProjectType)
            .Include(i => i.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(p => p.EmployerContracts)
            .Include(p => p.ProjectTechnicalAssistants)
            .Include(p => p.ProjectImplementationAssistants)
            .Include(p => p.ProjectOperations)
                .ThenInclude(i => i.RequestGoodsSupplies)
            .Include(i => i.FiduciaryProducts)

            .Where(p => p.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Project?> GetProjectByIdIncludeLess(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetProjectModelByIdReponse?> GetProjectModelById(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.Id == id)
            .Select(item => new GetProjectModelByIdReponse()
            {
                Id = item.Id,
                Status = item.Status,
                PreferentialReferenceCode = item.PreferentialReferenceCode,
                ProjectName = item.ProjectName,
                ProjectCode = item.ProjectCode,
                Prefix = item.Prefix,
                CostCenterId = item.ProjectCostCenters.Any() ? item.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = item.ProjectCostCenters.Any() ? item.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                CostCenterCode = item.ProjectCostCenters.Any() ? item.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterCode : null,
                ProjectManagerId = item.ProjectManager,
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Project?> GetForChangeStatus(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(p => p.ProjectType)
            .Include(p => p.ProjectOperations)
            .ThenInclude(p => p.ProjectOperationDetails)

            .Where(p => p.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Project?> GetProjectByIdNoIncluding(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)

            .Where(p => p.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Project?> FindByIdAndChild(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(p => p.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(p => p.ProjectTechnicalAssistants)
            .Include(p => p.ProjectImplementationAssistants)
            .Include(p => p.EmployerContracts)
            .Include(p => p.ProjectProducts)
                .ThenInclude(p => p.RequestGoodsSupplyDetails)

            .Where(p => p.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Project?> GetSummarizedProjectById(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p => p.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Project?> HaveProjectChild(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.Id == id &&
            p.ProjectImplementationAssistants.Any() &&
            p.ProjectTechnicalAssistants.Any());

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<long> FindLastProject(string prefix, CT ct)
    {
        var codes = await DbSet
            .AsNoTracking()
            .Where(p => p.ProjectCode != null && p.ProjectCode.StartsWith(prefix))
            .Select(p => p.ProjectCode!)
            .ToListAsync(ct);

        var max = codes
            .Select(c => c.Substring(prefix.Length))
            // remainder must be purely numeric, so "EMP-5-3" (a cost center code) is not counted for prefix "EMP-"
            .Where(s => s.Length <= 9 && long.TryParse(s, out _))
            .Select(long.Parse)
            .DefaultIfEmpty(0)
            .Max();

        return max + 1;
    }

    public async Task<long> FindLastProject(long employerId, long? costCenterId, CT ct)
    {
        var projects = await DbSet
            .Where(p =>
                p.EmployerId == employerId &&
                p.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId))
            .ToListAsync(ct);

        var maxNumber = projects
            .Select(p => p.ProjectCode!.Contains('-')
                ? long.Parse(p.ProjectCode.Split('-').Last())
                : long.Parse(p.ProjectCode))
            .DefaultIfEmpty(0)
            .Max();

        return maxNumber + 1;
    }



    public async Task<long> FindLastUnitOrg(long organizationId, CT ct)
    {
        var projects = await DbSet
            .Where(p =>
                p.OrganizationId == organizationId)
            .ToListAsync(ct);

        var maxNumber = projects
            .Select(p => p.ProjectCode!.Contains('-')
                ? long.Parse(p.ProjectCode.Split('-').Last())
                : long.Parse(p.ProjectCode))
            .DefaultIfEmpty(0)
            .Max();

        return maxNumber + 1;
    }

    public async Task<Project?> FindByName(
        string projectName,
        long? costCenterId,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                p.ProjectName == projectName &&
                (costCenterId == null || p.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (companyId == null || p.CompanyId.Equals(companyId)));

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<Project?> FindByCode(
        string ProjectCode,
        long? costCenterId,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                p.ProjectCode == ProjectCode &&
                (costCenterId == null || p.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (companyId == null || p.CompanyId.Equals(companyId)));

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<List<Project>?> GetByCodes(
        List<string> codes,
        long? companyId,
        bool haveCostCenter,
        bool isOrganizationUnit,
        List<ProjectStatus>? statuses,
        CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var query = DbSet
            .Include(x => x.ProjectOperations)
                .ThenInclude(x => x.OperationInfo)
            .Where(p =>
                codes.Contains(p.ProjectCode) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (companyId == null || p.CompanyId.Equals(companyId)));

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

#pragma warning restore CS8604 // Possible null reference argument.

        var result = await query
            .ToListAsync(ct);

        return result;
    }

    public async Task<(List<GetProjectsModel> Data, int RowCount)> GetProjects(
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
        CT ct)
    {
        var query = BuildQueryGetProjects(
            ids,
            filterData,
            status,
            categoryIds,
            costCenterIds,
            advisorId,
            projectManagerId,
            planningAssistantId,
            thirdPartyId,
            supervisorEngineerId,
            implementationAssistantId,
            technicalAssistantId,
            employerId,
            projectTypeId,
            isActive,
            companyId,
            statuses,
            checkThirdParty,
            haveCostCenter,
            isOrganizationUnit);

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetsContractedProject(
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
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(p => p.ProjectImplementationAssistants)
            .Include(p => p.ProjectTechnicalAssistants)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
                .ThenInclude(p => p.CostCenterWarehouses)
            .Include(p => p.EmployerContracts)

            .Where(p =>
                (companyId == null || p.CompanyId == companyId) && // <--- این خط را به‌روز کنید تا null-safe باشد
                (costCenterIds == null || p.ProjectCostCenters.Any() && p.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode!, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())) &&
                (employerId == null || p.EmployerId == employerId) &&
                (status == null || p.Status == status) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (categoryId == null || p.ProjectCategories.Any(x => x.CategoryId == categoryId)) &&
                (projectTypeId == null || p.ProjectType!.Id == projectTypeId) &&
                (isActive == null || p.IsActive == isActive) &&
                (p.EmployerContracts.Any() || p.Contractual));

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetsByNameOrCode(
        string filterData,
        long? companyId,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var query = DbSet

            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                (companyId == null || p.CompanyId == companyId) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())));

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

#pragma warning restore CS8604 // Possible null reference argument.

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetProjectsByCostCenter(
        long costCenterId,
        string? filterData,
        List<ProjectStatus>? statuses,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var query = DbSet

            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                p.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())));
#pragma warning restore CS8604 // Possible null reference argument.

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetActiveProjects(
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
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet

            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                (companyId == null || p.CompanyId == companyId) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (categoryId == null || p.ProjectCategories.Any(x => x.CategoryId == categoryId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode!, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())) &&
                (employerId == null || p.EmployerId == employerId) &&
                (costCenterId == null || p.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectTypeId == null || p.ProjectType.Id == projectTypeId) &&
                (projectManagerId == null || p.ProjectManager == projectManagerId) &&
                (planningAssistantId == null || p.PlanningAssistant == planningAssistantId) &&
                (supervisorEngineerId == null || p.SupervisorEngineer == supervisorEngineerId) &&
                (advisorId == null || p.Advisor == advisorId) &&
                (implementationAssistantId == null || p.ProjectImplementationAssistants.Any(x => x.ImplementationAssistantUserId == implementationAssistantId)) &&
                (technicalAssistantId == null || p.ProjectTechnicalAssistants.Any(x => x.TechnicalAssistantUserId == technicalAssistantId)) &&
                (contractual == null || p.Contractual == contractual) &&
                (checkThirdParty == false || !p.ProjectThirdParties.Any() || p.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId)) &&
                p.IsActive);

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetContractorProjects(
        long contractorId,
        long? costCenterId,
        string? filterData,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(p =>
                (costCenterId == null || p.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode, filterData.MakeLikePattern())) &&
                p.ProjectOperations.Any(po => po.ProjectOperationDetails.Any(pod =>
                pod.ProjectOperationDetailContractorServices.Any(x => x.ContractorId != null && x.ContractorId > 0 && x.ContractorId.Equals(contractorId)))));

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

        query = query.OrderBy(p => p.IsActive).ThenByDescending(p => p.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetsActiveProjectByCostCenterIds(
        string? filterData,
        long? employerId,
        List<long>? costCenterIds,
        long? projectTypeId,
        long? categoryId,
        long? projectManagerId,
        long? planningAssistantId,
        long? supervisorEngineerId,
        long? advisorId,
        long? implementationAssistantId,
        long? technicalAssistantId,
        long? companyId,
        bool? contractual,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet

            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                (companyId == null || p.CompanyId == companyId) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (categoryId == null || p.ProjectCategories.Any(x => x.CategoryId == categoryId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode!, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())) &&
                (employerId == null || p.EmployerId == employerId) &&
                (costCenterIds == null || (p.ProjectCostCenters.Any() && p.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId)))) &&
                (projectTypeId == null || p.ProjectType.Id == projectTypeId) &&
                (projectManagerId == null || p.ProjectManager == projectManagerId) &&
                (planningAssistantId == null || p.PlanningAssistant == planningAssistantId) &&
                (supervisorEngineerId == null || p.SupervisorEngineer == supervisorEngineerId) &&
                (advisorId == null || p.Advisor == advisorId) &&
                (implementationAssistantId == null || p.ProjectImplementationAssistants.Any(x => x.ImplementationAssistantUserId == implementationAssistantId)) &&
                (technicalAssistantId == null || p.ProjectTechnicalAssistants.Any(x => x.TechnicalAssistantUserId == technicalAssistantId)) &&
                (contractual == null || p.Contractual == contractual) &&
                p.IsActive);

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetsByEmployerId(
        long employerId,
        string? filterData,
        long? companyId,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                p.EmployerId == employerId &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (companyId == null || p.CompanyId == companyId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode!, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())) &&
                p.IsActive);

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<Project> Data, int RowCount)> GetsProjectByProjectManagerId(
        List<long> costCenterIds,
        long projectManagerId,
        string? filterData,
        long? companyId,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(p =>
                (p.ProjectCostCenters.Any() && p.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                p.ProjectManager == projectManagerId &&
                (companyId == null || p.CompanyId == companyId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode!, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())) &&
                p.IsActive);

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<Project>> GetProjectByIds(
        List<long> ids,
        bool haveCostCenter,
        bool isOrganizationUnit,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)
            .Where(p => ids.Contains(p.Id));

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

        return await query.ToListAsync(ct);
    }

    public async Task<Project?> GetProjectById(
        long id, CT ct)
        => await DbSet
            .Include(x => x.ProjectCostCenters).ThenInclude(x => x.CostCenter)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<(List<Project> Data, int RowCount)> GetsProjectByIds(
        List<long>? ids,
        string? filterData,
        List<ProjectStatus>? statuses,
        bool haveCostCenter,
        bool isOrganizationUnit,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(p =>
                (ids == null || ids.Contains(p.Id)) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode, filterData.MakeLikePattern()) &&
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())));

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

        query = query.OrderBy(p => !p.IsActive).ThenByDescending(p => p.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public IQueryable<Project> GetsProjectSorting(
        string? filterData,
        ProjectStatus? status,
        bool? isActive,
        long? companyId,
        bool haveCostCenter,
        bool isOrganizationUnit,
        List<ProjectStatus>? statuses,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.ProjectType)
            .Include(p => p.ProjectCategories)
                .ThenInclude(p => p.Category)
            .Include(p => p.ProjectCostCenters)
                .ThenInclude(p => p.CostCenter)
            .Include(i => i.ProjectTechnicalAssistants)
            .Include(i => i.ProjectImplementationAssistants)

            .Where(p =>
                (companyId == null || p.CompanyId == companyId) &&
                (statuses == null || statuses.Contains(p.Status)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())) &&
                (status == null || p.Status == status) &&
                (isActive == null || p.IsActive == isActive));

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

        var items = query.AsQueryable();
        return items;
    }

    public async Task<GetProjectForPdfResponse?> GetProjectForPdf(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.Id == id)
            .Select(item => new GetProjectForPdfResponse()
            {
                Id = item.Id,
                Status = item.Status,
                ProjectName = item.ProjectName,
                ProjectCode = item.ProjectCode,
                EmployerId = item.EmployerId,
                AdvisorId = item.Advisor,
                ProjectManagerId = item.ProjectManager,
                ApprovedBudget = item.ApprovedBudget,
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<GetUnAssignedProjectsModel>? Data, int RowCount)> GetUnAssignedProjects(
        string? filterData,
        long? cityId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(p =>
            !p.ProjectCostCenters.Any() &&
            (cityId == null || (p.CityId.HasValue && p.CityId == cityId)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.ProjectName, filterData.MakeLikePattern())))
            .Select(item => new GetUnAssignedProjectsModel()
            {
                Id = item.Id,
                ProjectName = item.ProjectName,
                ProjectCode = item.ProjectCode,
                CityId = item.CityId,
            });

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }
}
