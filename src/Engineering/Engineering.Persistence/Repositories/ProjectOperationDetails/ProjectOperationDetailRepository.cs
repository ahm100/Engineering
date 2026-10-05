using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public partial class ProjectOperationDetailRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetail>, IProjectOperationDetailRepository
{
    public ProjectOperationDetailRepository(EngineeringDBContext context) : base(context) { }

    public async Task<ProjectOperationDetail?> FindByIdAndMore(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.Project)
            .Include(x => x.OperationLocation)
            .Include(x => x.UserImplementations)
            .Include(x => x.UserTechnicals)
            .Include(x => x.UserPlaners)
            .Include(x => x.ProjectOperationDetailDocuments)
            .Include(x => x.ProjectOperationDetailDeductions)
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.OperationInfoService.ServiceInfo)
            .Include(x => x.DailyOperations)
         

            .Where(x =>
                x.Id == id &&
                x.OperationLocation!.IsDeleted == false &&
                !x.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<bool> GetProjectOperationDetailValidator(
        long projectOperationId, CT ct)
    {
        var query = DbSet

            .Where(x =>
                x.ProjectOperation.Id.Equals(projectOperationId) &&
                x.OperationLocation!.IsDeleted == false &&
                (x.RequestGoodsSupplies.Any() || x.DailyOperations.Any() || x.ConsumableVolumeProducts.Any(p => p.RequestGoodsSupplyDetails.Any())) &&
                !x.IsDeleted);

        var result = await query.AnyAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByIdNoIncluding(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(x => x.CostCenter)

            .Where(x => x.Id == id &&
                x.OperationLocation!.IsDeleted == false);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByCode(
        string code,
        long? operationLocationId,
        long? companyId, CT ct)
    {
        var query = DbSet
            .Where(x =>
                (companyId == null || x.CompanyId == companyId) &&
                (operationLocationId == null || x.OperationLocation.Id == operationLocationId) &&
                x.Code == code);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<string> CodeCreator(
        long? projectOperationId,
        long? operationLocationId,
        long? companyId, CT ct)
    {
        var query = await DbSet
           .Where(x =>
                (companyId == null || x.CompanyId == companyId) &&
                (projectOperationId == null || x.ProjectOperation.Id == projectOperationId) &&
                (operationLocationId == null || x.OperationLocation.Id == operationLocationId) &&
                EF.Functions.IsNumeric(x.Code)).Select(x => Convert.ToInt64(x.Code)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailDoneVolume(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.DailyOperations)
                .ThenInclude(x => x.DailyProjectOperationDocuments)
            .Include(x => x.ProjectOperationDetailDeductions)

            .Where(x => x.Id == id && !x.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByIdLessIncludes(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.Project)
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)
            .Include(x => x.UserImplementations)
            .Include(x => x.UserTechnicals)
            .Include(x => x.UserPlaners)
            .Include(x => x.ProjectOperationDetailDocuments)
            .Include(x => x.ProjectOperationDetailDeductions)
            .Include(x => x.ConsumableVolumeExperts)
                .ThenInclude(x => x.ProjectOperationDetailContractorExperts)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.OperationInfoService.ServiceInfo)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.DailyOperationServices)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.ProjectServiceDetail.ProjectService.ServiceInfo)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.ProjectOperationDetailContractorExperts)

            .Where(x => x.Id.Equals(id));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailForContractorServices(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.OperationInfoService.ServiceInfo)

            .Where(x =>
                x.Id.Equals(id));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailForValidates(
        long projectOperationId,
        long operationLocationId,
        string code,
        long? companyId, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Include(x => x.OperationLocation)

            .Where(x =>
                x.ProjectOperation.Id.Equals(projectOperationId) &&
                x.OperationLocation.Id.Equals(operationLocationId) &&
                x.CompanyId.Equals(companyId) &&
                x.Code.Equals(code));

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByIdLessInclude(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Include(x => x.ProjectOperation)
        ///TODO Employers
                //.ThenInclude(x => x.EmployerContract)
            .Include(x => x.OperationLocation)
            .Include(x => x.DailyOperations)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByIdForTotal(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.Project.ProjectCostCenters)
                    .ThenInclude(x => x.CostCenter)
            .Include(x => x.OperationLocation)
            .Include(x => x.DailyOperations)
            .Include(x => x.ProjectOperationDetailDeductions)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByIdWithoutInclude(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.Id == id && !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailWithVolumes(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.OperationInfo)
                    .ThenInclude(x => x.ConsumptionStandardExperts)
            .Include(x => x.ProjectOperation.OperationInfo)
                    .ThenInclude(x => x.ConsumptionStandardProduct)
            .Include(x => x.ProjectOperation.OperationInfo)
                    .ThenInclude(x => x.ConsumptionStandardMachineries)
                        .ThenInclude(x => x.Machinery)

            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailVolumes(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }


    public async Task<ProjectOperationDetail?> GetProjectOperationDetailWithStandards(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperation.OperationInfo.ConsumptionStandardExperts)
            .Include(x => x.ProjectOperation.OperationInfo.ConsumptionStandardProduct)
            .Include(x => x.ProjectOperation.OperationInfo.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailWithExpertValues(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumableVolumeExperts)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailWithProductVolumes(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumableVolumeProducts)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetsProjectOperationDetailWithProductSupply(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationLocation)
            .Include(x => x.ProjectOperation)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
                    .ThenInclude(x => x.RequestGoodsSupplyManagements)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }
    public async Task<ProjectOperationDetail?> GetProjectOperationDetailWithMachineryVolumes(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumableVolumeMachineries)
            .ThenInclude(x => x.Machinery)

            .Where(x =>
                x.Id == id &&
                !x.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailsByIdAsync(
        long projectOperationDetailId, CT ct)
    {
        return await DbSet
            .Include(x => x.OperationLocation)

            .Where(x => !x.IsDeleted && x.Id.Equals(projectOperationDetailId))

            .FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByIdForDaily(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation.Project)
                .ThenInclude(x => x.ProjectCostCenters)
                    .ThenInclude(x => x.CostCenter)
            .Include(x => x.ProjectOperation.OperationInfo)
            .Include(x => x.OperationLocation)
            .Include(x => x.ConsumableVolumeExperts)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
                    .ThenInclude(x => x.RequestGoodsSupplyManagements)
            .Include(x => x.ConsumableVolumeMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.OperationInfoService.ServiceInfo)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.ProjectServiceDetail.ProjectService)

            .Where(x => x.Id.Equals(id));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailForDelete(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperation)
                .ThenInclude(x => x.ProjectOperationDetails)
            .Include(x => x.DailyOperations)
            .Include(x => x.RequestGoodsSupplies)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
            .Include(x => x.ProjectOperationDetailContractorServices)
                .ThenInclude(x => x.ContractorContractDetailServices)

            .Where(x => x.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetByIdWithDaily(
        long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.DailyOperations)
            .Include(x => x.RequestGoodsSupplies)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)
            .Include(x => x.ProjectOperation.ProjectOperationDetails)
            .Include(x => x.ProjectOperation.Project)

            .Where(x => x.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetsProjectOperationDetailDocumentResponse?> GetsProjectOperationDetailDocument(
        long projectOperationDetailId, CT ct)
    {
        var query = BuildQueryGetsProjectOperationDetailDocument(projectOperationDetailId);

        var item = await query.AsNoTracking().SingleOrDefaultAsync();
        return item;
    }

    public async Task<ProjectOperationDetail?> GetByIdForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.DailyOperations)
            .Include(x => x.RequestGoodsSupplies)
            .Include(x => x.ConsumableVolumeProducts)
                .ThenInclude(x => x.RequestGoodsSupplyDetails)

            .Where(x => x.Id == id && !x.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<ProjectOperationDetail?> GetProjectOperationDetailByContractor(
        long projectId,
        long contractorId, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectOperation.Project)
            .Include(x => x.ProjectOperationDetailContractorServices)

            .Where(x => !x.IsDeleted &&
                x.ProjectOperation.Project.Id.Equals(projectId) &&
                    x.ProjectOperationDetailContractorServices.Any(x => x.ContractorId == contractorId))

            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetFilteredProjectOperationDetailsOperationModel> Data, int RowCount)> GetProjectOperationDetailByCostCenterId(
    long costCenterId,
    List<long>? projectIds,
    List<long>? categoryIds,
    List<long>? branchIds,
    List<long>? seasonIds,
    List<long>? operationInfoIds,
    string? filterData,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
            .Where(x =>
                x.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                (projectIds == null || projectIds.Contains(x.ProjectOperation.ProjectId)) &&
                (seasonIds == null || x.ProjectOperation.OperationInfo.OperationInfoSeasons.Any(x => seasonIds.Contains(x.Season.Id))) &&
                (branchIds == null || x.ProjectOperation.OperationInfo.OperationInfoSeasons.Any(x => branchIds.Contains(x.Season.Branch.Id))) &&
                (categoryIds == null || x.ProjectOperation.OperationInfo.OperationInfoSeasons.Any(x => categoryIds.Contains(x.Season.Branch.Category.Id))) &&
                (operationInfoIds == null || operationInfoIds.Contains(x.ProjectOperation.OperationInfoId))
                )
            .Select(x => new GetFilteredProjectOperationDetailsOperationModel
            {
                Id = x.Id,
                Length = x.Length,
                LengthChangeable = x.LengthChangeable,
                Width = x.Width,
                WidthChangeable = x.WidthChangeable,
                Height = x.Height,
                HeightChangeable = x.HeightChangeable,
                Weight = x.Weight,
                WeightChangeable = x.WeightChangeable,
                Number = x.Number,
                NumberChangeable = x.NumberChangeable,
                Priority = x.Priority,
                Day = x.Day,
                Hour = x.Hour,
                CreatedProductId = x.CreatedProductId,
                FinalAmount = x.FinalAmount,
                Description = x.Description,
                CompanyId = x.CompanyId,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                SeasonId = x.ProjectOperation.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.ProjectOperation.OperationInfo.Id).Season.Id,
                SeasonName = x.ProjectOperation.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.ProjectOperation.OperationInfo.Id).Season.SeasonName,
                BranchId = x.ProjectOperation.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.ProjectOperation.OperationInfo.Id).Season.BranchId,
                BranchName = x.ProjectOperation.OperationInfo.OperationInfoSeasons.FirstOrDefault(z => z.OperationInfo.Id == x.ProjectOperation.OperationInfo.Id).Season.Branch.BranchName,

                POperationModel = new GetFilteredProjectOperationsOperationModel
                {
                    Id = x.ProjectOperation.Id,
                    CostCenterId = x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                    CostCenterName = x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                    ProjectId = x.ProjectOperation.Project.Id,
                    ProjectName = x.ProjectOperation.Project.ProjectName,
                    ProjectCode = x.ProjectOperation.Project.ProjectCode,
                    OperationInfoId = x.ProjectOperation.OperationInfoId,
                    OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                    OperationInfoCode = x.ProjectOperation.OperationInfo.OperationInfoCode,
                    MeasurementId = x.ProjectOperation.UnitOfMeasurementId,
                    Description = x.ProjectOperation.Description,
                    Workload = x.ProjectOperation.Workload,
                    BasePrice = x.ProjectOperation.BasePrice,
                    ChangePrice = x.ProjectOperation.ChangedPrice,
                    CreatorId = x.ProjectOperation.CreatorId,
                    Created = x.ProjectOperation.Created
                },
                Category = x.ProjectOperation.Project.ProjectCategories.Select(x => new GetProjectsCategoryModel
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    CategoryCode = x.Category.CategoryCode
                }).ToList()
            });
        query = query
            .OrderBy(x => x.CategoryId)
            .ThenBy(x => x.BranchId)
            .ThenBy(x => x.SeasonId);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .ToListAsync(ct);
        var count = await query.CountAsync(ct);
        return (items, count);

    }
}
