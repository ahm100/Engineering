using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.RescheduleProjectOperations;
using Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationWorkloder;
using Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrPOForReports;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;
using Engineering.Application.Services.ProjectOperations.Models.GetPODate;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;
using Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.Models;
using Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;
using Engineering.Application.Services.Projects.Models.GetProjectProgress;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Persistence.Repositories.ProjectOperations;

public partial class ProjectOperationRepository : BaseRepository<EngineeringDBContext, ProjectOperation>, IProjectOperationRepository
{
    public ProjectOperationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectOperation?> GetProjectOperationById(
        long projectOperationId, CT ct)
    {
        ///TODO Employers
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            //.Include(x => x.EmployerContract)
            .Include(x => x.ProjectOperationDocuments)
            .Include(oo => oo.ProjectOperationDetails)

            .Where(b => b.Id == projectOperationId && !b.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationByIdIncludelessNew(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Include(oo => oo.Project)

            .Where(b => b.Id == id && !b.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<bool> GetProjectOperationForValidating(
        long operationInfoId,
        long projectId,
        long unitOfMeasurementId, CT ct)
    {
        var query = DbSet

            .Where(b =>
                b.OperationInfo.Id == operationInfoId &&
                b.Project.Id == projectId &&
                b.UnitOfMeasurementId == unitOfMeasurementId &&
                !b.IsDeleted);

        var result = await query.AnyAsync(ct);
        return result;
    }

    public async Task<List<ProjectOperation>?> GetProjectOperationForImport(
        List<long> operationInfoIds,
        List<long> projectIds, CT ct)
    {
        var query = DbSet

            .Where(b =>
                 operationInfoIds.Contains(b.OperationInfo.Id) &&
                 projectIds.Contains(b.Project.Id) &&
                !b.IsDeleted);

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationForUpdate(
        long id, CT ct)
    {
        ///TODO Employers
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Include(oo => oo.Project)
            //.Include(oo => oo.EmployerContract)
            .Include(oo => oo.ProjectOperationDocuments)

            .Where(b => b.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationByIdNoIncluding(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)

            .Where(b => b.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationByIdOperationInfoInclude(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Include(oo => oo.Project)

            .Where(b => b.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationDocuments(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDocuments)

            .Where(b => b.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationForRequestGoodsSupply(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
                .ThenInclude(oo => oo.ProjectCostCenters)
                    .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ProjectOperationDetails)

            .Where(b => b.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationWorkloderModel?> ProjectOperationWorkloder(
        long id, CT ct)
    {
        var query = BuildQueryProjectOperationWorkloder(id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    private IQueryable<ProjectOperationWorkloderModel> BuildQueryProjectOperationWorkloder(
        long id)
    {
        var query = DbSet.AsQueryable();
        var newQuery = query

            .Where(x =>
                x.Id == id)

            .Select(x => new ProjectOperationWorkloderModel
            {
                Id = x.Id,
                ProjectOperationDetailLists = (List<ProjectOperationWorkloderDTO>)x.ProjectOperationDetails.Select(p => new ProjectOperationWorkloderDTO
                {
                    Id = p.Id,
                    FinalAmount = p.Length * p.Weight * p.Width * p.Height * p.Number
                })
            });

        return newQuery;
    }

    public async Task<ProjectOperation?> GetProjectOperationByParams(
        long projectId,
        long operationInfoId,
        long? employerContractId,
        long measurementId,
        CT ct)
    {
        ///TODO Employers
        var query = DbSet
            //.Include(x => x.EmployerContract)
            .Include(x => x.Project)
            .Include(x => x.OperationInfo)

            .Where(b =>
            b.Project.Id == projectId &&
            b.OperationInfo.Id == operationInfoId &&
            b.UnitOfMeasurementId == measurementId
            );

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8604 // Possible null reference argument.
        ///TODO Employers
        //if (employerContractId is not null && employerContractId > 0)
        //    query = query.Where(x => x.EmployerContract != null && x.EmployerContract.Id.Equals(employerContractId));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationRealtionInfo(
        long projectOperationId,
        CT ct)
    {
        var query = DbSet.Where(b => b.Id == projectOperationId && !b.IsDeleted)
            .Include(oo => oo.OperationInfo)
            .Include(oo => oo.Project);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> ProjectOperationWorkloadManagement(
        long projectOperationId,
        CT ct)
    {
        var query = DbSet.Where(b => b.Id == projectOperationId && !b.IsDeleted)
            .Include(oo => oo.ProjectOperationDetails);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetByProjectOperationDetailId(
        long projectOperationDetailId,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetails)
            .ThenInclude(oo => oo.DailyOperations)
            .Where(b => b.ProjectOperationDetails.Any(x => x.Id == projectOperationDetailId) && !b.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetProjectOperationByIdLessInclude(
        long projectOperationId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
            .Include(oo => oo.ProjectOperationDetails)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardExperts)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardProduct)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardMachineries)
                    .ThenInclude(x => x.Machinery)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.OperationInfoServices)
                    .ThenInclude(x => x.ServiceInfo)

            .Where(b => b.Id == projectOperationId && !b.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetProjectOperationByIdsLessInclude(
        List<long> projectOperationIds, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardExperts)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardProduct)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardMachineries)
                    .ThenInclude(x => x.Machinery)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.OperationInfoServices)
                    .ThenInclude(x => x.ServiceInfo)
            .Include(a => a.ProjectOperationDetails)

            .Where(b => projectOperationIds.Contains(b.Id));

        var count = await query.CountAsync(ct);
        var projectOperations = await query.ToListAsync(ct);

        return (projectOperations, count);
    }

    public async Task<ProjectOperation?> GetByIdWithDependencies(
        long projectOperationId, CT ct)
    {
        var query = DbSet.Where(b => b.Id == projectOperationId && !b.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> GetForValidate(
        long projectId,
        long operationInfoId,
        long? employerContractId,
        long unitOfMeasurementId, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet.Where(oo =>
        oo.Project.Id == projectId &&
        oo.UnitOfMeasurementId == unitOfMeasurementId &&
        oo.OperationInfo.Id == operationInfoId
        ///TODO Employers
        //&& (employerContractId == null || oo.EmployerContract.Id == employerContractId)
        );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperation?> FindForDelete(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetails)

            .Where(b => b.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsByEmployerContract(
        long employerContractId,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(x => x.OperationInfo)

            .Where(oo =>
            ///TODO Employers
            //_.EmployerContract!.Id == employerContractId &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern())) &&
            oo.IsDeleted != true);

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsByOperationInfo(
        long operationInfoId,
        int pageIndex,
        int pageSize, CT ct)
    {
        ///TODO Employers
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(x => x.OperationInfo)
            //.Include(x => x.EmployerContract)

            .Where(oo =>
                oo.OperationInfo!.Id == operationInfoId &&
                oo.IsDeleted != true);

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsByOperationInfoIncludeLess(
        long operationInfoId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(oo =>
                oo.OperationInfo!.Id == operationInfoId &&
                oo.IsDeleted != true);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsProposedPrice(
        long operationInfoId,
        string? filterData,
        DateTime? startDate,
        DateTime? endDate,
        long? employerId,
        long? costCenterId,
        long? projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        ///TODO Employers
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet
            .Include(x => x.Project)
                .ThenInclude(x => x.ProjectCostCenters)
                    .ThenInclude(x => x.CostCenter)
            .Include(x => x.OperationInfo)
            //.Include(x => x.EmployerContract)

            .Where(oo =>
                oo.OperationInfo!.Id == operationInfoId &&
                //(oo.EmployerContract != null) &&
                (employerId == null || oo.Project.EmployerId == employerId) &&
                //(filterData == null || oo.EmployerContract.Code == filterData) &&
                (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null || oo.Project.Id == projectId) &&
                //(startDate == null || oo.EmployerContract.StartDate >= startDate) &&
                //(endDate == null || oo.EmployerContract.EndDate <= endDate) &&
                oo.IsDeleted != true);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsForPricing(
        long employerId,
        long projectId,
        long costCenterId,
        string? contractCode,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        ///TODO Employers
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        var query = DbSet
            .Include(p => p.Project)
                .ThenInclude(c => c.ProjectCostCenters)
                    .ThenInclude(c => c.CostCenter)
            .Include(c => c.OperationInfo)
            //.Include(i => i.EmployerContract)

            .Where(oo => oo.Project.Id == projectId &&
                oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                oo.Project.EmployerId == employerId
                ///TODO Employers
                //&& (string.IsNullOrWhiteSpace(contractCode) || EF.Functions.Like(oo.EmployerContract.Code, contractCode.MakeLikePattern()))
                );
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsProjectOperationByProjectModel> Data, int RowCount)> GetsByProject(
        long projectId,
        long? categoryId,
        long? branchId,
        long? seasonId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = BuildQueryGetsByProject(
            projectId,
            categoryId,
            branchId,
            seasonId,
            filterData);

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<string>> GetExistingOperationInfoCodesInProject(
    long projectId,
    List<string> operationInfoCodes,
    CT ct)
    {
        if (operationInfoCodes == null || !operationInfoCodes.Any())
            return new List<string>();

        return await DbSet
            .Where(po => po.ProjectId == projectId
                      && !po.IsDeleted
                      && po.OperationInfo != null
                      && operationInfoCodes.Contains(po.OperationInfo.OperationInfoCode))
            .Select(po => po.OperationInfo.OperationInfoCode!)
            .Distinct()
            .ToListAsync(ct);
    }

    public async Task<(List<GetsProjectOperationByProjectModel> Data, int RowCount)> GetsFilteredByProject(
        long projectId,
        long? categoryId,
        long? branchId,
        long? seasonId,
        List<long>? contractorIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = BuildQueryGetsFilteredByProject(
            projectId,
            categoryId,
            branchId,
            seasonId,
            contractorIds,
            startDate,
            endDate,
            filterData);

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var stringQuery = query.ToQueryString();

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByProjectIds(
        List<long>? projectIds,
        List<long>? contractorIds,
        long? categoryId,
        long? branchId,
        long? seasonId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        ///TODO Employers
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(x => x.OperationInfo)
                .ThenInclude(x => x.OperationInfoSeasons)
                    .ThenInclude(x => x.Season.Branch.Category)
            //.Include(x => x.EmployerContract)
            .Include(x => x.ProjectOperationDetails)

            .Where(p =>
                (projectIds == null || projectIds.Contains(p.Project.Id)) &&
                (contractorIds == null || p.OperationInfo.OperationInfoServices.Any(o => o.ProjectOperationDetailContractorServices.Any(c => c.ContractorId != null && contractorIds.Contains(c.ContractorId.Value)))) &&
                (categoryId == null || p.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) &&
                (branchId == null || p.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == branchId)) &&
                (seasonId == null || p.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                p.IsDeleted != true);

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsFilteredByProjectIds(
        List<long>? projectIds,
        List<long>? notShowProjectOperationIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)

            .Where(oo =>
                (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                (notShowProjectOperationIds == null || !notShowProjectOperationIds.Contains(oo.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                oo.IsDeleted != true);

        query = query.OrderByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsPrioritizeProjectOperation(
        string? filterData,
        long? id,
        int priority,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)

            .Where(oo =>
                oo.Priority < priority &&
                (id == null || oo.Id != id) &&
                (companyId == null || oo.CompanyId != companyId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                oo.IsDeleted != true);

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsForDailyProjectOperations(
        long projectId,
        int? priority,
        string? filterData,
        DateTime? startDate,
        DateTime? endDate,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(x => x.OperationInfo)
                .ThenInclude(x => x.OperationInfoSeasons)
                    .ThenInclude(x => x.Season.Branch.Category)
            ///TODO Employers
            //.Include(x => x.EmployerContract)
            .Include(x => x.ProjectOperationDetails)
                .ThenInclude(x => x.DailyOperations)

            .Where(oo => oo.Project!.Id == projectId &&
                (priority == null || oo.Priority == priority) &&
                oo.ProjectOperationDetails.Any() &&
                ///TODO Employers
                //(startDate == null || oo.EmployerContract.StartDate >= startDate) &&
                //(endDate == null || oo.EmployerContract.EndDate <= endDate) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                oo.IsDeleted != true);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsWithoutContract(
        List<long>? ids,
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(x => x.OperationInfo)

            ///TODO Employers
            .Where(oo =>
            oo.Project!.Id == projectId &&
            (ids == null || !ids.Contains(oo.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                //_.EmployerContract.Equals(null) &&
                oo.IsDeleted != true);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsWithoutContractProjectOperationForESS(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(x => x.OperationInfo)
            .Include(x => x.ProjectOperationDetails)
                .ThenInclude(x => x.DailyOperations)
            .Include(x => x.EmployerStatusStatementProjectOperations)
                .ThenInclude(x => x.EmployerStatusStatementProjectOperationDetails)

            ///TODO Employers
            .Where(oo => oo.Project!.Id == projectId &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                //_.EmployerContract.Equals(null) &&
                oo.IsDeleted != true);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsWithContractProjectOperationForESS(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDocuments)
            .Include(x => x.OperationInfo)
            .Include(x => x.ProjectOperationDetails)
                .ThenInclude(x => x.DailyOperations)
            .Include(x => x.EmployerStatusStatementProjectOperations)
                .ThenInclude(x => x.EmployerStatusStatementProjectOperationDetails)

            ///TODO Employers
            .Where(oo => oo.Project!.Id == projectId &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                //!_.EmployerContract.Equals(null) &&
                oo.IsDeleted != true);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsByProjectOperationId(
        long projectId,
        int? priority,
        long? seasonId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Project)
            .Include(x => x.OperationInfo)
            ///TODO Employers
            //.Include(x => x.EmployerContract)

            .Where(oo =>
                oo.Project.Id == projectId &&
                (priority == null || oo.Priority < priority) &&
                (seasonId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)));

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ProjectOperation>> GetWithoutIncludeByIds(
        List<long> Ids,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Where(oo => Ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return (items);
    }

    public async Task<List<ProjectOperation>> GetByProjectCodesAndOInfoCodes(
        List<string> projectCodes,
        List<string> oInfoCodes,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDetails)
            .Where(oo => projectCodes.Contains(oo.Project.ProjectCode) && oInfoCodes.Contains(oo.OperationInfo.OperationInfoCode));

        var items = await query.ToListAsync(ct);
        return (items);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsSummarizedProjectOperation(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)

            .Where(oo =>
                oo.Project.Id == projectId &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())));

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationDependency(
        long projectId,
        long operationInfoId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)

            .Where(oo =>
                oo.Project.Id == projectId &&
                oo.OperationInfo.Id == operationInfoId
                );

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsProjectOperationReportingModel> Data, int RowCount)> GetsProjectOperationReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<ProjectOperationStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        string? dailyDescription,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = BuildQueryGetsProjectOperationReporting(
            ids,
            costCenterId,
            projectIds,
            operationInfoIds,
            contractorIds,
            statuses,
            startDate,
            endDate,
            description,
            dailyDescription,
            filterData);

        query = query.OrderByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsTotalProjectOperationReportingResponseModel> Data, int RowCount)> GetsTotalProjectOperationReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<ProjectOperationStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        string? dailyDescription,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(oo => !oo.IsDeleted &&
                                  (ids == null || ids.Contains(oo.Id)) &&
                                  (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                                  (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                                  (operationInfoIds == null || operationInfoIds.Contains(oo.OperationInfo.Id)) &&
                                  (startDate == null || oo.ProjectOperationDetails.Any(x => x.StartDate.HasValue && x.StartDate.Value.Date >= startDate.Value.Date)) &&
                                  (endDate == null || oo.ProjectOperationDetails.Any(x => x.EndDate.HasValue && x.EndDate.Value.Date <= endDate.Value.Date)) &&
                                  (statuses == null || statuses.Contains(oo.ProjectOperationStatus)) &&
                                  (contractorIds == null || oo.ProjectOperationDetails.Any(x => x.DailyOperations.Any(s => s.DailyProjectOperationServices
                                      .Any(s => s.ContractorId != null && s.ContractorId > 0 && contractorIds.Contains(s.ContractorId.Value))))) &&
                                  (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                                   string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                                  (string.IsNullOrWhiteSpace(description) || oo.ProjectOperationDetails.Any(x => EF.Functions.Like(x.Description, description.MakeLikePattern()))) &&
                                  (string.IsNullOrWhiteSpace(dailyDescription) || oo.ProjectOperationDetails
                                      .Any(x => x.DailyOperations.Any(z => EF.Functions.Like(z.Description, dailyDescription.MakeLikePattern()))))
             ).Select(x => new GetsTotalProjectOperationReportingResponseModel()
             {
                 Dailies = x.ProjectOperationDetails
                     .Where(x => !x.IsDeleted && x.DailyOperations != null && x.DailyOperations.Count > 0)
                     .SelectMany(x => x.DailyOperations)
                     .Where(z => !z.IsDeleted)
                     .Select(z => new GetsTotalDailyModel()
                     {
                         Id = z.Id,
                         Width = z.Width,
                         Height = z.Height,
                         Length = z.Length,
                         Number = z.Number,
                         Weight = z.Weight
                     }).ToList(),

                 Deductions = x.ProjectOperationDetails
                     .Where(x => !x.IsDeleted && x.ProjectOperationDetailDeductions != null && x.ProjectOperationDetailDeductions.Count > 0)
                     .SelectMany(x => x.ProjectOperationDetailDeductions)
                     .Where(z => !z.IsDeleted)
                     .Select(z => new GetsTotalDeductionModel()
                     {
                         Id = z.Id,
                         Width = z.Width,
                         Height = z.Height,
                         Length = z.Length,
                         Number = z.Number,
                         Weight = z.Weight
                     }).ToList(),

                 ProjectOperationWorkload = x.Workload,

                 Details = x.ProjectOperationDetails.Where(z => !z.IsDeleted).Select(z => new GetsTotalDetailModel()
                 {
                     Id = z.Id,
                     Width = z.Width,
                     Height = z.Height,
                     Length = z.Length,
                     Number = z.Number,
                     Weight = z.Weight
                 }).ToList(),
             });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsProjectOperationEmployerReportingModel> Data, int RowCount)> GetsProjectOperationEmployerReporting(
        List<long>? ids,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? contractorIds,
        List<long>? employerIds,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = BuildQueryGetsProjectOperationEmployerReporting(
            ids,
            costCenterId,
            projectIds,
            operationInfoIds,
            contractorIds,
            employerIds,
            startDate,
            endDate,
            filterData);
        query = query.OrderByDescending(a => a.Created);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsProjectOperationDailyReportingModel> Data, int RowCount)> GetsProjectOperationDailyReporting(
    List<long>? ids,
    long? costCenterId,
    List<long>? projectIds,
    List<long>? operationInfoIds,
    DateTime? startDate,
    DateTime? endDate,
    string? filterData,
    string[]? orderBy,
    int pageIndex,
    int pageSize, CT ct)
    {
        var query = BuildQueryGetsProjectOperationDailyReporting(
            ids,
            costCenterId,
            projectIds,
            operationInfoIds,
            startDate,
            endDate,
            filterData);
        query = query.OrderByDescending(a => a.CostCenterName);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsForEmployerStatusStatementModel> Data, int RowCount)> GetsForEmployerStatusStatement(
        long? employerId,
        long? employerStatusStatementId,
        long projectId,
        long costCenterId,
        long? employerContractId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = BuildQueryGetsForEmployerStatusStatement(
            employerId,
            employerStatusStatementId,
            projectId,
            costCenterId,
            employerContractId,
            startDate,
            endDate,
            filterData);

        query = query.OrderByDescending(a => a.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetDailyProjectOperationFilter(
        long costCenterId,
        long projectId,
        long? contractorId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(a => a.OperationInfo)
            .Include(a => a.Project)
            .Include(a => a.ProjectOperationDetails)
            .ThenInclude(a => a.DailyOperations)

            .Where(oo =>
                (oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (oo.Project.Id.Equals(projectId)) &&
                (contractorId == null || oo.ProjectOperationDetails.Any(x => x.ProjectOperationDetailContractorServices.Any(s => s.ContractorId != null && s.ContractorId.Value.Equals(contractorId)))) &&
                (startDate == null || oo.ProjectOperationDetails.Any(oo => oo.StartDate >= startDate)) &&
                (endDate == null || oo.ProjectOperationDetails.Any(oo => oo.EndDate <= endDate)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode!, filterData.MakeLikePattern())))

            .Select(c => new
            {
                c,
                SortStatus = c.ProjectOperationDetails.Count > 0 ? c.ProjectOperationDetails.Select(c => new
                { DetailSortStatus = c.Status == ProjectOperationDetailStatus.Doing ? 1 : 2 }).OrderBy(c => c.DetailSortStatus).FirstOrDefault().DetailSortStatus : 2
            })
            .Select(c => c.c);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetFilteredProjectOperationsByProjectIds(
        long costCenterId,
        List<long> projectIds,
        long? contractorId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(a => a.OperationInfo)
            .Include(a => a.Project)
                .ThenInclude(a => a.ProjectCostCenters)
                    .ThenInclude(a => a.CostCenter)
            .Include(a => a.ProjectOperationDetails)
                .ThenInclude(a => a.DailyOperations)

            .Where(oo =>
                (oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectIds.Contains(oo.Project.Id)) &&
                (contractorId == null || oo.ProjectOperationDetails.Any(x => x.ProjectOperationDetailContractorServices.Any(s => s.ContractorId != null && s.ContractorId.Value.Equals(contractorId)))) &&
                (startDate == null || oo.ProjectOperationDetails.Any(oo => oo.StartDate >= startDate)) &&
                (endDate == null || oo.ProjectOperationDetails.Any(oo => oo.EndDate <= endDate)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfo.OperationInfoCode!, filterData.MakeLikePattern())))

            .Select(c => new
            {
                c,
                SortStatus = c.ProjectOperationDetails.Count > 0 ? c.ProjectOperationDetails.Select(c => new
                { DetailSortStatus = c.Status == ProjectOperationDetailStatus.Doing ? 1 : 2 }).OrderBy(c => c.DetailSortStatus).FirstOrDefault().DetailSortStatus : 2
            })
            .Select(c => c.c);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByIds(
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            ///TODO Employers
            //.Include(x => x.EmployerContract)
            .Include(oo => oo.Project)
            .Include(oo => oo.OperationInfo)
            .Include(a => a.ProjectOperationDetails)
                .ThenInclude(a => a.DailyOperations)

            .Where(p => !p.IsDeleted &&
            (companyId == null || p.CompanyId == companyId) &&
            (projectOperationIds == null || projectOperationIds.Contains(p.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfo.OperationInfoCode!, filterData.MakeLikePattern())));

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByIdsIncludeless(
        List<long>? projectOperationIds,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(p => !p.IsDeleted &&
            (companyId == null || p.CompanyId == companyId) &&
            (projectOperationIds == null || projectOperationIds.Contains(p.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(p.OperationInfo.OperationInfoCode!, filterData.MakeLikePattern())));

        query = query.OrderByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ProjectOperation>> GetByIds(
        List<long> ids,
        CT ct)
    {
        return await DbSet
            .Where(p => !p.IsDeleted && ids.Contains(p.Id)).ToListAsync(ct);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetFilteredProjectOperations(
        List<long> projectOperationIds, CT ct)
    {
        var query = DbSet
            ///TODO Employers
            //.Include(x => x.EmployerContract)
            .Include(oo => oo.Project)

            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardExperts)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardProduct)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.ConsumptionStandardMachineries)
                    .ThenInclude(x => x.Machinery)
            .Include(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.OperationInfoServices)
                    .ThenInclude(x => x.ServiceInfo)
            .Include(a => a.ProjectOperationDetails)
                .ThenInclude(a => a.DailyOperations)

            .Where(b => !b.IsDeleted && projectOperationIds.Contains(b.Id))

            .OrderBy(oo => oo.Priority);

        var count = await query.CountAsync(ct);
        var projectOperations = await query.ToListAsync(ct);

        return (projectOperations, count);
    }

    public async Task<List<ProjectOperation>> GetProjectOperations(
        List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(x => x.EmployerOperations)
                .ThenInclude(x => x.EmployerContract.EmployerContractHead)
            .Include(oo => oo.Project)
            .Include(a => a.ProjectOperationDetails)
            .Where(b => !b.IsDeleted && ids.Contains(b.Id));

        return await query.ToListAsync(ct);
    }

    public async Task<List<ProjectOperation>> GetForSchecdulingAsync(long projectId, long operationInfo, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetails)
                .ThenInclude(oo => oo.OperationLocation)
            .Include(oo => oo.OperationInfo)
            .Include(oo => oo.Project)

            .Where(oo =>
                oo.Project.Id.Equals(projectId) &&
                oo.OperationInfo.Id.Equals(operationInfo));

        return await query.ToListAsync(ct);
    }

    public async Task<(List<ProjectOperation> Data, int RowCount)> GetsProjectOperationByIdIncludeLess(
        List<long> projectOperationIds, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfo)
            .Include(oo => oo.Project)
            ///TODO Employers
            //.Include(oo => oo.EmployerContract)

            .Where(oo => projectOperationIds.Contains(oo.Id));

        var count = await query.CountAsync(ct);
        var items = await query
            .ToListAsync(ct);

        return (items, count);
    }

    public async Task<ProjectOperation?> FindMergedProjectOperation(
        long projectOperationId,
        long projectId,
        long unitOfMeasurementId,
        long operationInfoId,
        CT ct)
    {
        var query = DbSet

            .Where(x => x.UnitOfMeasurementId == unitOfMeasurementId &&
            x.OperationInfo.Id == operationInfoId &&
            x.Project.Id == projectId &&
            x.Id != projectOperationId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<GetFltrBasePricedPOsModel>> GetFltrBasePricedPOs(
        long costCenterId,
        List<long>? projectIds,
        List<long>? actionIds,
        List<long>? categoryIds,
        List<long>? branchIds,
        List<long>? seasonIds,
        List<long>? operationInfoIds,
        string? filterData,
        CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                (actionIds == null || x.OperationInfo.OperationInfoActions.Any(a => actionIds.Contains(a.ActionId))) &&
                (projectIds == null || projectIds.Contains(x.ProjectId) || projectIds == null)
                && (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationInfo.OperationInfoName, filterData.MakeLikePattern())
                || string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationInfo.OperationInfoCode!, filterData.MakeLikePattern())) &&
                (seasonIds == null || x.OperationInfo.OperationInfoSeasons.Any(x => seasonIds.Contains(x.Season.Id))) &&
                (branchIds == null || x.OperationInfo.OperationInfoSeasons.Any(x => branchIds.Contains(x.Season.Branch.Id))) &&
                (categoryIds == null || x.OperationInfo.OperationInfoSeasons.Any(x => categoryIds.Contains(x.Season.Branch.Category.Id))) &&
                (operationInfoIds == null || operationInfoIds.Contains(x.OperationInfoId))
                )

            .Select(x => new GetFltrBasePricedPOsModel
            {
                Id = x.Id,
                CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.Project.Id,
                ProjectName = x.Project.ProjectName,
                ProjectCode = x.Project.ProjectCode,
                OperationInfoId = x.OperationInfoId,
                OperationInfoName = x.OperationInfo.OperationInfoName,
                OperationInfoCode = x.OperationInfo.OperationInfoCode,
                MeasurementId = x.UnitOfMeasurementId,
                Description = x.Description,
                Workload = x.Workload,
                IncreaseRate = x.IncreaseRate,
                BasePrice = x.BasePrice,
                ChangePrice = x.ChangedPrice,
                CreatorId = x.CreatorId,
                Created = x.Created,
                SeasonId = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Id,
                SeasonName = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.SeasonName,
                BranchId = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.BranchId,
                BranchName = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Branch.BranchName,
                CategoryId = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Branch.CategoryId,
                CategoryName = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Branch.Category.CategoryName,
                ActionData = x.OperationInfo.OperationInfoActions.Select(a => new GetFltrBasePricedPOActionModel
                {
                    Id = a.Action.Id,
                    Price = a.Price.Value!,
                    ActionName = a.Action.ActionName,
                }).ToList(),
                POActionData = x.ProjectOperationActions.Select(a => new GetBasePricedPOActionModel
                {
                    Id = a.Id,
                    OIActionId = a.OperationInfoActionId,
                    ActionId = a.OperationInfoAction.Action.Id,
                    Price = a.Price.Value,
                    ActionName = a.OperationInfoAction.Action.ActionName,
                }).ToList(),
                Category = x.Project.ProjectCategories.Select(x => new GetProjectsCategoryModel
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    CategoryCode = x.Category.CategoryCode
                }).ToList()
            });

        query = query.OrderByDescending(a => a.Created);
        return await query.ToListAsync(ct);
    }

    public async Task<List<GetFltrProjectOperationModel>> GetFltrProjectOperation(
       long costCenterId,
        List<long>? projectIds,
        List<long>? operationInfoIds,
        List<long>? measureUnitIds,
        decimal? minPrice,
        decimal? maxPrice,
        string? filterData,
        int pageIndex,
        int pageSize,
       CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                (projectIds == null || projectIds.Contains(x.ProjectId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationInfo.OperationInfoCode!, filterData.MakeLikePattern())) &&
                (operationInfoIds == null || operationInfoIds.Contains(x.OperationInfo.Id)) &&
                (measureUnitIds == null || measureUnitIds.Contains(x.UnitOfMeasurementId)) &&
                (maxPrice == null || x.ChangedPrice < maxPrice) &&
                (minPrice == null || x.ChangedPrice > minPrice) &&
                (x.EmployerOperations.Any(eo => eo.EmployerContract.AdvancePayment != 0))
                )

            .Select(x => new GetFltrProjectOperationModel
            {
                Id = x.Id,
                CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectId = x.Project.Id,
                ProjectName = x.Project.ProjectName,
                ProjectCode = x.Project.ProjectCode,
                OperationInfoId = x.OperationInfoId,
                OperationInfoName = x.OperationInfo.OperationInfoName,
                OperationInfoCode = x.OperationInfo.OperationInfoCode,
                MeasurementId = x.UnitOfMeasurementId,
                Description = x.Description,
                Workload = x.Workload,
                BasePrice = x.BasePrice,
                ChangePrice =
                    x.OperationInfo.OperationInfoActions.Any(a => (a.Price ?? 0) != 0)
                    ? x.OperationInfo.OperationInfoActions.Sum(a => a.Price ?? 0)
                    : x.ChangedPrice,
                CreatorId = x.CreatorId,
                Created = x.Created,
                CategoryId = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Branch.Category.Id,
                CategoryName = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Branch.Category.CategoryName,
                SeasonId = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Id,
                SeasonName = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.SeasonName,
                BranchId = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.BranchId,
                BranchName = x.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.OperationInfo.Id).Season.Branch.BranchName,
            });

        query = query.OrderByDescending(a => a.Created);

        return await query.ToListAsync(ct);
    }


    public async Task<List<GetFltrPOForReportsModel>> GetFltrPOForReports(
    List<long>? costCenterIds,
    List<long>? projectIds,
    string? filterData,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
            .Where(x =>
                (projectIds == null || projectIds.Contains(x.Project.Id)) &&
                (costCenterIds == null || x.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())) &&
                x.IsDeleted != true)
            .Select(x => new GetFltrPOForReportsModel()
            {
                Id = x.Id,
                OperationInfoId = x.OperationInfoId,
                OperationInfoName = x.OperationInfo.OperationInfoName,
                OperationInfoCode = x.OperationInfo.OperationInfoCode,
                OperationLatinName = x.OperationInfo.OperationLatinName,
                UnitOfMeasurementId = x.OperationInfo.UnitOfMeasurementId,
                Created = x.Created
            }).Distinct();

        query = query.OrderByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return items;
    }


    public async Task<GetProjectOperationProgressResponse?> GetProjectOperationProgress(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.Id == id &&
            x.PlannedStartDate.HasValue &&
            x.PlannedFinishDate.HasValue)
            .Select(x => new GetProjectOperationProgressResponse
            {
                PlannedStartDate = x.PlannedStartDate.Value,
                PlannedFinishDate = x.PlannedFinishDate.Value,
                ActualProgressPercent = (x.ProjectOperationDetails.SelectMany(d => d.DailyOperations).Sum(x => x.Length * x.Width * x.Height * x.Weight * x.Number) / x.Workload) * 100
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetPODateResponse?> GetPODate(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetPODateResponse
            {
                PlannedStartDate = x.PlannedStartDate,
                PlannedFinishDate = x.PlannedFinishDate,
                ActualStartDate = x.ActualStartDate,
                ActualFinishDate = x.ActualFinishDate,
                BaselineStartDate = x.BaselineStartDate,
                BaselineFinishDate = x.BaselineFinishDate,
            });

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<List<ProjectOperation>?> GetByProjectId(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.ProjectId == id);

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<int?> GetDelayed(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.ProjectId == id &&
            x.PlannedFinishDate.HasValue &&
            x.PlannedFinishDate < DateTime.Now);

        return await query.CountAsync(ct);
    }

    public async Task<int?> GetCriticalPO(
        long id, CT ct)
    {
        var now = DateTime.UtcNow;

        var query = DbSet
            .Where(x => x.ProjectId == id &&
            x.PlannedFinishDate.HasValue &&
            EF.Functions.DateDiffDay(now, x.PlannedFinishDate.Value) >= 0 &&
            EF.Functions.DateDiffDay(now, x.PlannedFinishDate.Value) < 3);

        return await query.CountAsync(ct);
    }

    public async Task<DateTime?> GetLastPO(
    long id,
    CT ct)
    {
        return await DbSet
            .Where(x => x.ProjectId == id &&
                        x.PlannedFinishDate.HasValue)
            .OrderByDescending(x => x.PlannedFinishDate)
            .Select(x => x.PlannedFinishDate)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<DateTime?> GetFirstPO(
    long id,
    CT ct)
    {
        return await DbSet
            .Where(x => x.ProjectId == id &&
                        x.PlannedFinishDate.HasValue)
            .OrderBy(x => x.PlannedFinishDate)
            .Select(x => x.PlannedFinishDate)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetCriticalPOModel>? Data, int RowCount)> GetCriticalPOModel(
        long id,
        int pageIndex,
        int pageSize, CT ct)
    {
        var now = DateTime.UtcNow;

        var query = DbSet
            .Where(x => x.ProjectId == id &&
            x.PlannedFinishDate.HasValue &&
            EF.Functions.DateDiffDay(now, x.PlannedFinishDate.Value) >= 0 &&
            EF.Functions.DateDiffDay(now, x.PlannedFinishDate.Value) < 3)
            .Select(x => new GetCriticalPOModel
            {
                Id = x.Id,
                OperationInfoId = x.OperationInfoId,
                OperationInfoName = x.OperationInfo.OperationInfoName,
                OperationInfoCode = x.OperationInfo.OperationInfoCode,
                OperationLatinName = x.OperationInfo.OperationLatinName,
            });

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);

    }

    public async Task<(List<GetProjectPOTimelinesModel>? Data, int Count)> GetProjectPOTimelines(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x =>
            x.PlannedStartDate.HasValue &&
                    x.PlannedFinishDate.HasValue)
            .Select(x => new GetProjectPOTimelinesModel
            {
                Id = x.Id,
                OperationInfoId = x.OperationInfoId,
                OperationInfoName = x.OperationInfo.OperationInfoName,
                OperationInfoCode = x.OperationInfo.OperationInfoCode,
                WorkLoad = x.Workload,

                PlannedStartDate = x.PlannedStartDate,
                PlannedFinishDate = x.PlannedFinishDate,
                ActualStartDate = x.ActualStartDate,
                ActualFinishDate = x.ActualFinishDate,

                PlannedStartProgressPercent =
                (x.ProjectOperationDetails
                .SelectMany(d => d.DailyOperations)
                .Where(d => d.StartDate <= x.PlannedStartDate)
                .Sum(d => d.Length * d.Width * d.Height * d.Weight * d.Number)
                / x.Workload) * 100,

                PlannedEndProgressPercent =
                (x.ProjectOperationDetails
                .SelectMany(d => d.DailyOperations)
                .Where(d => d.StartDate <= x.PlannedFinishDate)
                .Sum(d => d.Length * d.Width * d.Height * d.Weight * d.Number)
                / x.Workload) * 100,

                ActualStartProgressPercent =
                x.ActualStartDate == null ? 0 :
                (x.ProjectOperationDetails
                .SelectMany(d => d.DailyOperations)
                .Where(d => d.StartDate <= x.ActualStartDate)
                .Sum(d => d.Length * d.Width * d.Height * d.Weight * d.Number)
                / x.Workload) * 100,

                ActualEndProgressPercent =
                x.ActualFinishDate == null ? 0 :
                (x.ProjectOperationDetails
                .SelectMany(d => d.DailyOperations)
                .Where(d => d.StartDate <= x.ActualFinishDate)
                .Sum(d => d.Length * d.Width * d.Height * d.Weight * d.Number)
                / x.Workload) * 100
            });

        var count = await query.CountAsync(ct);

        var result = await query.ToListAsync(ct);
        return (result, count);
    }

    public async Task<(List<GetProjectProgressModel>? Data, int Count)> GetProjectProgress(
    long id, CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.ProjectId == id &&
                x.PlannedStartDate.HasValue &&
                x.PlannedFinishDate.HasValue);

        var raw = await query.Select(x => new
        {
            x.Workload,

            PlannedStart = x.PlannedStartDate!.Value,
            PlannedFinish = x.PlannedFinishDate!.Value,

            ActualStart = x.ActualStartDate,
            ActualFinish = x.ActualFinishDate,

            Daily = x.ProjectOperationDetails
                .SelectMany(d => d.DailyOperations)
                .Select(d => new
                {
                    d.StartDate,
                    Volume =
                        d.Length * d.Width * d.Height * d.Weight * d.Number
                })
        }).ToListAsync(ct);

        if (!raw.Any())
            return (new List<GetProjectProgressModel>(), 0);

        var totalWorkload = raw.Sum(x => x.Workload);

        var dates = raw
            .SelectMany(x => new DateTime?[]
            {
                x.PlannedStart,
                x.PlannedFinish,
                x.ActualStart,
                x.ActualFinish
            })
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        decimal GetActual(DateTime date)
        {
            var done = raw
                .SelectMany(x => x.Daily)
                .Where(d => d.StartDate <= date)
                .Sum(d => d.Volume);

            return totalWorkload == 0 ? 0 : done / totalWorkload * 100m;
        }

        decimal GetPlanned(DateTime date)
        {
            decimal planned = 0;

            foreach (var x in raw)
            {
                if (date <= x.PlannedStart)
                    continue;

                if (date >= x.PlannedFinish)
                {
                    planned += x.Workload;
                    continue;
                }

                var duration = (x.PlannedFinish - x.PlannedStart).TotalDays;
                if (duration <= 0)
                    continue;

                var elapsed = (date - x.PlannedStart).TotalDays;

                var ratio = (decimal)(elapsed / duration);

                planned += x.Workload * Math.Clamp(ratio, 0, 1);
            }

            return totalWorkload == 0 ? 0 : planned / totalWorkload * 100m;
        }

        var result = dates
            .Select(date => new GetProjectProgressModel
            {
                Date = date,
                PlannedPercent = Math.Round(GetPlanned(date), 2),
                ActualPercent = Math.Round(GetActual(date), 2)
            })
            .ToList();

        return (result, result.Count);
    }

    public async Task<List<ProjectOperationScheduleModel>> GetOperationsForReschedule(
    List<long> ids,
    CT ct)
    {
        return await DbSet
            .Where(x => ids.Contains(x.Id))
            .Select(x => new ProjectOperationScheduleModel
            {
                Id = x.Id,

                PlannedStartDate = x.PlannedStartDate,
                PlannedFinishDate = x.PlannedFinishDate,
                PlannedDuration = x.PlannedDuration,

                Predecessors = x.SuccessorProjectOperationDependencies
                    .Select(d => new ProjectOperationPredecessorModel
                    {
                        Id = d.PredecessorId,

                        PlannedStartDate = d.Predecessor.PlannedStartDate,
                        PlannedFinishDate = d.Predecessor.PlannedFinishDate,
                        PlannedDuration = d.Predecessor.PlannedDuration,

                        DependencyType = d.DependencyType,
                        LagDays = d.LagDays
                    })
                    .ToList()
            })
            .ToListAsync(ct);
    }
}