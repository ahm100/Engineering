using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Application.Services.OperationInfos.Models.FehrestBaha;
using Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;
using Engineering.Domain.Entities.Branchs;
using Engineering.Domain.Entities.Categories;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Seasons;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Application.Services.OperationInfos;

public partial class OperationInfoLogic
{

    private async Task<Result<RasteReshteExcelImportsResponse>> CreateRasteReshteCommand(
    List<RasteReshteCategoryExcelModel> categories,
    List<RasteReshteBranchExcelModel> branches,
    List<RasteReshteSeasonExcelModel> seasons,
    long companyId,
    CT ct)
    {
        try
        {
            var categoryCodes = categories
                .Select(x => x.Code)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var branchCodes = branches
                .Select(x => x.Code)
                .Concat(seasons.Select(x => x.BranchCode))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var seasonCodes = seasons
                .Select(x => x.Code)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var existingCategories =
                await _categoryRepository.GetForRasteReshteImport(
                    categoryCodes,
                    companyId,
                    ct);

            var categoryDictionary =
                existingCategories.ToDictionary(
                    x => x.CategoryCode,
                    x => x,
                    StringComparer.OrdinalIgnoreCase);

            var newCategories = new List<Category>();


            foreach (var item in categories)
            {
                if (categoryDictionary.ContainsKey(item.Code))
                    continue;

                var category = new Category(
                    item.Title,
                    item.Code,
                    true,
                    companyId);

                categoryDictionary[item.Code] = category;
                newCategories.Add(category);
            }


            var existingCategoryIds = existingCategories
                .Select(x => x.Id)
                .ToList();

            var existingBranches =
                await _branchRepository.GetForRasteReshteImport(
                    existingCategoryIds,
                    branchCodes,
                    companyId,
                    ct);

            var branchDictionary =
                existingBranches.ToDictionary(
                    x => $"{x.Category.CategoryCode}|{x.BranchCode}",
                    x => x,
                    StringComparer.OrdinalIgnoreCase);

            var newBranches = new List<Branch>();

            foreach (var item in branches)
            {
                var branchKey =
                    $"{item.CategoryCode}|{item.Code}";

                if (branchDictionary.ContainsKey(branchKey))
                    continue;

                if (!categoryDictionary.TryGetValue(
                        item.CategoryCode,
                        out var category))
                {
                    return Result.Failure<RasteReshteExcelImportsResponse>(
                        SharedErrors.UnknownError)!;
                }

                var branch = new Branch(
                    category,
                    item.Title,
                    item.Code,
                    true,
                    companyId);

                branchDictionary[branchKey] = branch;
                newBranches.Add(branch);
            }

            var existingBranchIds = existingBranches
                .Select(x => x.Id)
                .ToList();

            var existingSeasons =
                await _seasonRepository.GetForRasteReshteImport(
                    existingBranchIds,
                    seasonCodes,
                    companyId,
                    ct);

            var seasonDictionary =
                existingSeasons.ToDictionary(
                    x =>
                        $"{x.Branch.Category.CategoryCode}|{x.Branch.BranchCode}|{x.SeasonCode}",
                    x => x,
                    StringComparer.OrdinalIgnoreCase);

            var newSeasons = new List<Season>();

            foreach (var item in seasons)
            {
                var seasonKey =
                    $"{item.CategoryCode}|{item.BranchCode}|{item.Code}";

                if (seasonDictionary.ContainsKey(seasonKey))
                    continue;

                var branchKey =
                    $"{item.CategoryCode}|{item.BranchCode}";

                if (!branchDictionary.TryGetValue(
                        branchKey,
                        out var branch))
                {
                    return Result.Failure<RasteReshteExcelImportsResponse>(
                        SharedErrors.UnknownError)!;
                }

                var season = new Season(
                    branch,
                    item.Title,
                    item.Code,
                    true,
                    companyId);

                seasonDictionary[seasonKey] = season;
                newSeasons.Add(season);
            }

            if (newCategories.Count > 0)
            {
                await _categoryRepository.AddRangeAsync(
                    newCategories,
                    ct);
            }

            if (newBranches.Count > 0)
            {
                await _branchRepository.AddRangeAsync(
                    newBranches,
                    ct);
            }

            if (newSeasons.Count > 0)
            {
                await _seasonRepository.AddRangeAsync(
                    newSeasons,
                    ct);
            }

            return new RasteReshteExcelImportsResponse(
                true,
                newCategories.Count,
                newBranches.Count,
                newSeasons.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error in CreateRasteReshteCommand");

            return Result.Failure<RasteReshteExcelImportsResponse>(
                SharedErrors.UnknownError)!;
        }
    }


    //fehrestBaha:
    private async Task<Result<FehrestBahaExcelImportsResponse>> CreateFehrestBahaCommand(
        List<FehrestBahaExcelModel> items, long branchId, long companyId, long yearId, CT ct)
    {
        var errors = new List<FehrestBahaExcelImportError>();
        try
        {
            var operationInfoCodes = items
                .Where(x => !string.IsNullOrWhiteSpace(x.OperationInfoCode))
                .Select(x => x.OperationInfoCode)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            var seasonCodes = items
                .Where(x => !string.IsNullOrWhiteSpace(x.SeasonCode))
                .Select(x => x.SeasonCode)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            var unitNames = items
                .Where(x => !string.IsNullOrWhiteSpace(x.UnitOfMeasurement))
                .Select(x => x.UnitOfMeasurement)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            var seasons = await _seasonRepository.GetForFehrestBahaImport(branchId, seasonCodes, companyId, ct);
            var seasonDictionary = seasons.ToDictionary(x => x.SeasonCode, x => x, StringComparer.OrdinalIgnoreCase);
            foreach (var seasonCode in seasonCodes)
            {
                if (!seasonDictionary.ContainsKey(seasonCode))
                {
                    return Result.Failure<FehrestBahaExcelImportsResponse>(
                        SeasonErrors.SeasonWithNameNotFound)!;
                }
            }
            var normalizedUnitNames = unitNames.Select(StringSeparator.Normalize).Distinct().ToList();
            var measureUnits = await _measureUnitRepository.GetByNamesForFehrestBaha(normalizedUnitNames, ct);
            var unitDictionary = measureUnits
                .Where(x => x != null)
                .ToDictionary(
                    x => StringSeparator.Normalize(x!.Name),
                    x => x!,
                    StringComparer.OrdinalIgnoreCase);
            var missingUnits = unitNames
                .Where(u => !unitDictionary.ContainsKey(StringSeparator.Normalize(u)))
                .Select(u => $"'{u}' (نرمال‌شده: '{StringSeparator.Normalize(u)}')")
                .Distinct()
                .ToList();

            if (missingUnits.Any())
            {
                _logger.LogWarning(
                "Measure units not found for FehrestBaha import. BranchId: {BranchId}, CompanyId: {CompanyId}, MissingUnits: {MissingUnits}",
                branchId,
                companyId,
                string.Join("، ", missingUnits));
            }

            var existingOperationInfos =
                await _operationInfoRepository.FindByCodes(operationInfoCodes, companyId, ct);
            var existingOperationInfoIds = existingOperationInfos.Select(x => x.Id).ToList();
            var existingOperationInfoEntities =
                existingOperationInfoIds.Count == 0
                    ? new List<OperationInfo>()
                    : await _operationInfoRepository.GetOperationInfos(existingOperationInfoIds, ct);
            var operationInfoDictionary =
                existingOperationInfoEntities.ToDictionary(
                    x => x.OperationInfoCode,
                    x => x,
                    StringComparer.OrdinalIgnoreCase);
            var newOperationInfos = new List<OperationInfo>();
            foreach (var item in items
               .Where(x => !string.IsNullOrWhiteSpace(x.OperationInfoCode)))
            {
                if (operationInfoDictionary.ContainsKey(item.OperationInfoCode))
                    continue;
                if (string.IsNullOrWhiteSpace(item.UnitOfMeasurement))
                    continue;
                var normalizedUnitName = StringSeparator.Normalize(item.UnitOfMeasurement);
                if (!unitDictionary.TryGetValue(normalizedUnitName, out var measureUnit)) // gets value and save
                {
                  errors.Add(new FehrestBahaExcelImportError(
                  nameof(item.UnitOfMeasurement),
                  item.UnitOfMeasurement,
                  $"واحد اندازه‌گیری '{item.UnitOfMeasurement}' یافت نشد."));
                    continue;
                }
                var operationInfo = new OperationInfo(
                    item.OperationInfoName,
                    item.OperationInfoCode,
                    null,
                    null,
                    measureUnit.Id,
                    true,
                    true,
                    item.BasePrice,
                    companyId);
                operationInfo.SetYearId(yearId);
                operationInfoDictionary[item.OperationInfoCode] = operationInfo;
                newOperationInfos.Add(operationInfo);
            }
            if (newOperationInfos.Count > 0)
            {
                await _operationInfoRepository.AddRangeAsync(newOperationInfos, ct);
            }

            // Existing OperationInfoSeason relations
            var allOperationInfos = operationInfoDictionary.Values.ToList();
            var operationInfoIdsForRelation = allOperationInfos
                    .Where(x => x.Id > 0) // just existing not new ones
                    .Select(x => x.Id)
                    .ToList();
            var existingRelations =
                operationInfoIdsForRelation.Count == 0
                    ? []
                    : await _operationInfoSeasonRepository
                        .GetBySeasonIdsAndOIIds(
                            seasons.Select(x => x.Id).ToList(),
                            operationInfoIdsForRelation,
                            ct);
            var existingRelationKeys = existingRelations?
                .Select(x => $"{x.OperationInfoId}|{x.SeasonId}")
                .ToHashSet(StringComparer.OrdinalIgnoreCase)
                ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var createdRelationsCount = 0;

            // OperationInfoSeason
            foreach (var item in items
                .Where(x => !string.IsNullOrWhiteSpace(x.OperationInfoCode)))
            {
                if (string.IsNullOrWhiteSpace(item.UnitOfMeasurement))
                    continue;
                if (!operationInfoDictionary.TryGetValue(
                        item.OperationInfoCode,
                        out var operationInfo)) 
                {
                    errors.Add(new FehrestBahaExcelImportError(
                    nameof(item.OperationInfoCode),
                    item.OperationInfoCode,
                    $"OperationInfo با کد '{item.OperationInfoCode}' یافت نشد."));
                    continue;
                }
                if (!seasonDictionary.TryGetValue(item.SeasonCode, out var season))
                {
                    errors.Add(new FehrestBahaExcelImportError(
                    nameof(item.SeasonCode),
                    item.SeasonCode,
                    $"فصل با کد '{item.SeasonCode}' یافت نشد."));
                    continue;
                }
                // Newly created OperationInfo:
                // Navigation is used so EF can resolve the FK after SaveChanges.
                if (operationInfo.Id == 0)
                {
                    operationInfo.AddOperationInfoSeason(
                        new OperationInfoSeason(
                            operationInfo,
                            season));
                    createdRelationsCount++;
                    continue;
                }
                // Existing OperationInfo:
                // Only create missing relations.
                var relationKey = $"{operationInfo.Id}|{season.Id}";
                if (existingRelationKeys.Contains(relationKey))
                    continue;
                var createRelationResult =
                    await _operationInfoSeasonLogic.CreateOperationInfoSeason(
                        new([operationInfo.Id], null, [season.Id], null), ct);
                if (createRelationResult.IsFailure)
                {
                    errors.Add(new FehrestBahaExcelImportError(
                   "OperationInfoSeason",
                   $"{item.OperationInfoCode}|{item.SeasonCode}",
                   createRelationResult.Error?.Message ?? "خطا در ساخت رابطه OperationInfo و Season."));
                    continue;
                }
                existingRelationKeys.Add(relationKey);
                createdRelationsCount++;
            }

            return new FehrestBahaExcelImportsResponse(
                true,
                newOperationInfos.Count,
                existingRelations!.Count + createdRelationsCount, errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateFehrestBahaCommand");
            return Result.Failure<FehrestBahaExcelImportsResponse>(
                SharedErrors.UnknownError)!;
        }
    }


}

