using Engineering.Application.Services.Categories.Models.CategoryModels;
using Engineering.Application.Services.Categories.Models.GetsByFilterData;
using Engineering.Application.Services.OperationInfoSeasons.Models;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;
using Engineering.Domain.Entities.Branchs;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Seasons;
using Category = Engineering.Domain.Entities.Categories.Category;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Extensions.ServiceModeling
{
    public static class DataModeling
    {

        public static List<GetsByFilterDataResponseModel>? GetsCategoryTree(
            List<Category>? categoryData,
            List<Branch>? branchData,
            List<Season>? seasonData,
            List<Company>? companies)
        {
            var result = new List<GetsByFilterDataResponseModel>();

            if (categoryData is not null && categoryData.Count > 0)
                foreach (var item in categoryData)
                {
                    result.Add(new()
                    {
                        AlternativeId = $"Category{item.Id}",
                        Branchs = new(),
                        CategoryCode = item.CategoryCode,
                        CategoryName = item.CategoryName,
                        Id = item.Id,
                        IsActive = item.IsActive
                    });
                }

            if (branchData is not null && branchData.Count > 0)
                foreach (var item in branchData)
                {
                    var branch = new BranchModel(
                        item.Id,
                        $"Branch{item.Id}",
                        item.BranchName,
                        item.BranchCode,
                        item.IsActive,
                        new());

                    if (result.Any(a => a.Id == item.Category.Id))
                        result.Where(w => w.Id == item.Category.Id).FirstOrDefault()!.Branchs?.Add(branch);
                    else
                    {
                        result.Add(new()
                        {
                            AlternativeId = $"Category{item.Id}",
                            Branchs = new(),
                            CategoryCode = item.Category.CategoryCode,
                            CategoryName = item.Category.CategoryName,
                            Id = item.Category.Id,
                            IsActive = item.Category.IsActive
                        });
                        result.Where(w => w.Id == item.Category.Id).FirstOrDefault()!.Branchs?.Add(branch);
                    }
                }

            if (seasonData is not null && seasonData.Count > 0)
                foreach (var item in seasonData)
                {
                    var season = new SeasonModel(
                        item.Id,
                        $"Season{item.Id}",
                        item.SeasonName,
                        item.SeasonCode,
                        item.IsActive);

                    if (result.Any(a => a.Branchs!.Any(a => a.Id == item.Branch.Id)))
                    {
                        var branchItem = result.Where(w => w.Id == item.Branch.Category.Id).FirstOrDefault()!;
                        branchItem.Branchs!.Where(w => w.Id == item.Branch.Id).FirstOrDefault()!.Seasons?.Add(season);
                    }
                    else if (result.Any(a => a.Id == item.Branch.Category.Id))
                    {
                        result.Where(w =>
                            w.Id == item.Branch.Category.Id)
                            .FirstOrDefault()!
                            .Branchs?.Add(new
                                (item.Branch.Id,
                                    $"Branch{item.Id}",
                                    item.Branch.BranchName,
                                    item.Branch.BranchCode,
                                    item.Branch.IsActive,
                                    new()));
                        var branchItem = result.Where(w => w.Id == item.Branch.Category.Id).FirstOrDefault()!;
                        branchItem.Branchs!.Where(w => w.Id == item.Branch.Id).FirstOrDefault()!.Seasons?.Add(season);
                    }
                    else
                    {
                        result.Add(new()
                        {
                            AlternativeId = $"Category{item.Id}",
                            Branchs = new(),
                            CategoryCode = item.Branch.Category.CategoryCode,
                            CategoryName = item.Branch.Category.CategoryName,
                            Id = item.Branch.Category.Id,
                            IsActive = item.Branch.Category.IsActive
                        });
                        result.Where(w =>
                            w.Id == item.Branch.Category.Id)
                            .FirstOrDefault()!
                            .Branchs?.Add(new
                                (item.Branch.Id,
                                    $"Branch{item.Id}",
                                    item.Branch.BranchName,
                                    item.Branch.BranchCode,
                                    item.Branch.IsActive,
                                    new()));

                        var branchItem = result.Where(w => w.Id == item.Branch.Category.Id).FirstOrDefault()!;
                        branchItem.Branchs!.Where(w => w.Id == item.Branch.Id).FirstOrDefault()!.Seasons?.Add(season);
                    }
                }


            return result;
        }

        public static GetsOperationInfoSeasonByIdResponse? GetsCategoryBranchSeasonModeling(
            List<OperationInfoSeason>? items)
        {
            var categoryData = new List<OperationInfoSeasonsCategoryModel>();
            var branchData = new List<OperationInfoSeasonsBranchModel>();
            var seasonData = new List<OperationInfoSeasonsSeasonModel>();

            if (items is not null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    var season = new SeasonModel(
                        item.Season.Id,
                        $"Season{item.Season.Id}",
                        item.Season.SeasonName,
                        item.Season.SeasonCode,
                        item.Season.IsActive);

                    if (!categoryData.Any(a => a.Id == item.Season.Branch.Category.Id))
                        categoryData.Add(new
                            (item.Season.Branch.Category.Id,
                                item.Season.Branch.Category.CategoryName,
                                item.Season.Branch.Category.CategoryCode,
                                item.Season.Branch.Category.IsActive));

                    if (!branchData.Any(a => a.Id == item.Season.Branch.Id))
                        branchData.Add(new
                            (item.Season.Branch.Id,
                                item.Season.Branch.Category.Id,
                                item.Season.Branch.BranchName,
                                item.Season.Branch.BranchCode,
                                item.Season.Branch.IsActive));

                    if (!seasonData.Any(a => a.Id == item.Season.Id))
                    {
                        seasonData.Add(new()
                        {
                            OperationInfoSeasonId = item.Id,
                            Id = item.Season.Id,
                            BranchId = item.Season.Branch.Id,
                            SeasonName = item.Season.SeasonName,
                            SeasonCode = item.Season.SeasonCode,
                            IsLast = false,
                            IsActive = item.Season.IsActive
                        });
                    }
                }

                var requestGoodsSupplies = items.SelectMany(s => s.RequestGoodsSupplies).ToList();
                var isLast = requestGoodsSupplies.OrderByDescending(o => o.Created).FirstOrDefault()?.OperationInfoSeason;

                if (isLast is not null && seasonData.Any(a => a.Id.Equals(isLast.Season.Id)))
                    seasonData.Where(w => w.Id.Equals(isLast.Season.Id)).FirstOrDefault()!.IsLast = true;

                if (seasonData.Count == 1)
                    seasonData.FirstOrDefault()!.IsLast = true;
            }

            return new GetsOperationInfoSeasonByIdResponse(categoryData, branchData, seasonData);
        }

        public static GetsCategoryBranchSeasonNameModel? GetsCategoryBranchSeasonNameModeling(
            GetsOperationInfoSeasonByIdResponse? seasonData)
        {
            var categoriesName = "";
            var branchesName = "";
            var seasonsName = "";

            var categories = seasonData?.CategoryData?.Select(s => s.CategoryName);
            if (categories is not null)
                categoriesName = string.Join(", ", categories);

            var branches = seasonData?.BranchData?.Select(s => s.BranchName);
            if (branches is not null)
                branchesName = string.Join(", ", branches);

            var seasons = seasonData?.SeasonData?.Select(s => s.SeasonName);
            if (seasons is not null)
                seasonsName = string.Join(", ", seasons);

            return new GetsCategoryBranchSeasonNameModel(categoriesName, branchesName, seasonsName);
        }

        public static GetsCategoryBranchSeasonNameModel? GetsCategoryBranchSeasonNameModeling(
             List<OperationInfoSeasonsCategoryModel>? categoryData,
             List<OperationInfoSeasonsBranchModel>? branchData,
             List<OperationInfoSeasonsSeasonModel>? seasonData)
        {
            var categoriesName = "";
            var branchesName = "";
            var seasonsName = "";

            var categories = categoryData?.Select(x => x.CategoryName);
            if (categories is not null)
                categoriesName = string.Join(", ", categories);

            var branches = branchData?.Select(x => x.BranchName);
            if (branches is not null)
                branchesName = string.Join(", ", branches);

            var seasons = seasonData?.Select(x => x.SeasonName);
            if (seasons is not null)
                seasonsName = string.Join(", ", seasons);

            return new GetsCategoryBranchSeasonNameModel(categoriesName, branchesName, seasonsName);
        }
    }
}
