using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Persistence.Repositories.OperationInfos;

public partial class OperationInfoRepository : BaseRepository<EngineeringDBContext, OperationInfo>, IOperationInfoRepository
{
    private IQueryable<GetOperationInfosModel> BuildQueryGetOperationInfos(
       List<long>? ids,
       string? filterData,
       long? categoryId,
       long? branchId,
       long? seasonId,
       bool? isActive,
       long? companyId)
    {
        var query = DbSet.AsQueryable();
        var newQuery = query.Where(oo =>
                    (companyId == null || oo.CompanyId == companyId) &&
                    (categoryId == null || oo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) &&
                    (branchId == null || oo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == branchId)) &&
                    (seasonId == null || oo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)) &&
                    (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
                     string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
                     string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern())) &&
                    (isActive == null || oo.IsActive == isActive))

            .Select(x => new GetOperationInfosModel
            {
                Id = x.Id,
                Created = x.Created,
                CreatorId = x.CreatorId,
                UpdaterId = x.UpdaterId,
                CompanyId = x.CompanyId,
                UnitOfMeasurementId = x.UnitOfMeasurementId,
                BasePrice = x.OperationInfoActions.Any(x => x.Price != 0)
                            ? x.OperationInfoActions.Sum(x => x.Price) ?? 0
                            : x.BasePrice,
                Priority = x.Priority,
                OperationLatinName = x.OperationLatinName,
                OperationInfoName = x.OperationInfoName,
                HaveExpertStandard = x.ConsumptionStandardExperts.Any(),
                HaveMachineryStandard = x.ConsumptionStandardMachineries.Any(),
                HaveProductStandard = x.ConsumptionStandardProduct.Any(),
                HaveServices = x.OperationInfoServices.Any(),
                HasChanged = x.HasChanged,
                HaveStandard = x.HaveStandard,
                IsActive = x.IsActive,
                OperationInfoCode = x.OperationInfoCode
            });

        return newQuery;
    }

    private IQueryable<GetOperationInfosModel> BuildQueryGetOperationInfosModelByIds(
       List<long>? ids)
    {
        var query = DbSet.AsQueryable();
        var newQuery = query.Where(oo =>
                    (ids == null || ids.Contains(oo.Id)))

            .Select(x => new GetOperationInfosModel
            {
                Id = x.Id,
                Created = x.Created,
                CompanyId = x.CompanyId,
                UnitOfMeasurementId = x.UnitOfMeasurementId,
                BasePrice = x.OperationInfoActions.Any(x => x.Price != 0)
                            ? x.OperationInfoActions.Sum(x => x.Price) ?? 0
                            : x.BasePrice,
                Priority = x.Priority,
                OperationLatinName = x.OperationLatinName,
                OperationInfoName = x.OperationInfoName,
                HaveExpertStandard = x.ConsumptionStandardExperts.Any(),
                HaveMachineryStandard = x.ConsumptionStandardMachineries.Any(),
                HaveProductStandard = x.ConsumptionStandardProduct.Any(),
                HaveServices = x.OperationInfoServices.Any(),
                HaveStandard = x.HaveStandard,
                IsActive = x.IsActive,
                HasChanged = x.HasChanged,
                OperationInfoCode = x.OperationInfoCode,
                GroupItems = x.OperationInfoGroupRelations.Select(o => o.OperationInfoGroup.OperationInfoGroupTitle).ToList(),
                SeasonItems = x.OperationInfoSeasons.Select(x => x.Season.SeasonName).ToList(),
                BranchItems = x.OperationInfoSeasons.Select(x => x.Season.Branch.BranchName).ToList(),
                CategoryItems = x.OperationInfoSeasons.Select(x => x.Season.Branch.Category.CategoryName).ToList(),
            });

        return newQuery;
    }
}