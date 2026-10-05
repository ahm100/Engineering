using Engineering.Application.Services.Adjustments.Contracts;
using Engineering.Application.Services.Adjustments.Contracts.CreateAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.DeleteAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;
using Engineering.Application.Services.Adjustments.Contracts.UpdateAdjustmentIndex;
using Engineering.Domain.Entities.Adjustment.Enums;
using Engineering.Domain.Entities.Adjustments;
using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.Seasons;
using Engineering.Domain.Errors.Adjustments;

namespace Engineering.Application.Services.Adjustments;

public partial class AdjustmentLogic
{
    private async Task<Result<AdjustmentExcelImportsResponse>> CreateAdjustmentCommand(
    List<AdjustmentIndexExcelModel> indexes, List<AdjustmentIndexValueExcelModel> values, long companyId, long yearId, string? notificationFileName, CT ct)
    {
        try
        {
            var errors = new List<AdjustmentImportError>();
            var seasonAdjustments = 0;
            var branchAdjustments = 0;
            var categoryCodes = indexes
                .Select(x => x.CategoryCode)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            var branchCodes = indexes
                .Select(x => x.BranchCode)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            var seasonCodes = indexes
                .Where(x => !string.IsNullOrWhiteSpace(x.SeasonCode))
                .Select(x => x.SeasonCode!)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var adjustmentReference =
            await _adjustmentReferenceRepository.GetByTitle(
                "سازمان برنامه و بودجه",
                ct);

            if (adjustmentReference is null)
            {
                errors.Add(new AdjustmentImportError("adjustmentReference", "", "adjustmentReference not found."));
                return new AdjustmentExcelImportsResponse(false, 0, 0, errors);
            }
            var adjustmentReferenceId = adjustmentReference.Id;

            var branches =
                await _branchRepository.GetForAdjustmentImport(categoryCodes, branchCodes, companyId, ct);

            var branchDictionary = branches.ToDictionary(
                x => $"{x.Category.CategoryCode}|{x.BranchCode}", x => x, StringComparer.OrdinalIgnoreCase);

            var seasons = seasonCodes.Count == 0
                ? []
                : await _seasonRepository.GetByCodesIncludeNavigations(categoryCodes,branchCodes, seasonCodes, companyId,ct);

            var seasonDictionary = seasons.ToDictionary(x =>
                    $"{x.Branch.Category.CategoryCode}|" + $"{x.Branch.BranchCode}|" + $"{x.SeasonCode}",
                x => x, StringComparer.OrdinalIgnoreCase);

            var valuesDictionary = values
                .GroupBy(x => x.IndexName, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(x => x.Key, x => x.ToList(), StringComparer.OrdinalIgnoreCase);

            var newIndexes = new List<AdjustmentIndex>();

            foreach (var item in indexes)
            {
                var branchKey =
                    $"{item.CategoryCode}|{item.BranchCode}";

                if (!branchDictionary.TryGetValue(branchKey, out var branch))
                {
                    errors.Add(new AdjustmentImportError("Branch", branchKey, "Branch not found."));
                    continue;
                }
                Season? season = null;
                if (!string.IsNullOrWhiteSpace(item.SeasonCode))
                {
                    var seasonKey = $"{item.CategoryCode}|" + $"{item.BranchCode}|" + $"{item.SeasonCode}";
                    if (!seasonDictionary.TryGetValue(seasonKey, out season))
                    {
                        continue;
                    }
                }

                var code = CreateAdjustmentIndexCode(item.CategoryCode, item.BranchCode, item.SeasonCode);

                var adjustmentIndex = new AdjustmentIndex(
                    adjustmentReferenceId,
                     yearId,
                    branch,
                    season,
                    code,
                    item.IndexName,
                    null,
                    notificationFileName);

                if (!item.IsActive)
                    adjustmentIndex.Deactivate();

                if (!valuesDictionary.TryGetValue(item.IndexName, out var indexValues))
                {
                    errors.Add(new AdjustmentImportError("IndexName", item.IndexName, "Adjustment values not found."));
                    continue;
                }

                foreach (var value in indexValues)
                {
                    var adjustmentIndexValue = new AdjustmentIndexValue(
                        adjustmentIndex,
                        value.YearName,
                        ParseAdjustmentIndexType(value.Type),
                        value.Value,
                        null,
                        null,
                        null,
                        ParseAdjustmentPeriod(value.Period),
                        true);

                    adjustmentIndex.AddValue(adjustmentIndexValue);
                }

                newIndexes.Add(adjustmentIndex);

                if (season == null)
                    branchAdjustments++;
                else
                    seasonAdjustments++;
            }

            if (newIndexes.Count > 0)
            {
                await _adjustmentIndexRepository.AddRangeAsync(newIndexes, ct);
            }

            return new AdjustmentExcelImportsResponse(
                errors.Count == 0,
                seasonAdjustments,
                branchAdjustments,
                errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error in CreateAdjustmentCommand");

            return Result.Failure<AdjustmentExcelImportsResponse>(
                SharedErrors.UnknownError)!;
        }
    }

    private static string CreateAdjustmentIndexCode(
    string categoryCode,
    string branchCode,
    string? seasonCode)
    {
        return string.IsNullOrWhiteSpace(seasonCode)
            ? $"ADJ-{categoryCode}-{branchCode}-00"
            : $"ADJ-{categoryCode}-{branchCode}-{seasonCode}";
    }

    private static ContractAdjustmentPeriod ParseAdjustmentPeriod(
        int period)
    {
        return period switch
        {
            1 => ContractAdjustmentPeriod.FirstQuarter,
            2 => ContractAdjustmentPeriod.SecondQuarter,
            3 => ContractAdjustmentPeriod.ThirdQuarter,
            4 => ContractAdjustmentPeriod.FourthQuarter,

            _ => throw new InvalidOperationException(
                $"Invalid adjustment period: {period}")
        };
    }

    private static AdjustmentIndexType ParseAdjustmentIndexType(
        string type)
    {
        return type.Trim() switch
        {
            "موقت" => AdjustmentIndexType.Temporary,
            "قطعی" => AdjustmentIndexType.Final,
            "میانگین" => AdjustmentIndexType.Average,

            _ => throw new InvalidOperationException(
                $"Invalid adjustment index type: {type}")
        };
    }
    private async Task<Result<AdjustmentIndex?>>
    CreateAdjustmentIndexCommand(CreateAdjustmentIndexRequest request, CT ct)
    {
        try
        {
            var adjustmentReference =
                await _adjustmentReferenceRepository.GetById(request.AdjustmentReferenceId, ct);

            if (adjustmentReference is null)
                return Result.Failure<AdjustmentIndex>(AdjustmentErrors.AdjustmentReferenceNotFound);

            var branches = await _branchRepository.GetsBranchByIds([request.BranchId], ct);
            var branch = branches.FirstOrDefault();
            if (branch is null)
                return Result.Failure<AdjustmentIndex>(
                    SharedErrors.UnknownError)!;

            if (branch is null)
                return Result.Failure<AdjustmentIndex>(
                    BranchErrors.BranchNotFound);

            Season? season = null;
            if (request.SeasonId.HasValue)
            {
                season = await _seasonRepository.FindById(request.SeasonId.Value, ct);
                if (season is null)
                    return Result.Failure<AdjustmentIndex>(SeasonErrors.SeasonNotFound);

                if (season.BranchId != branch.Id)
                    return Result.Failure<AdjustmentIndex>(SeasonErrors.SeasonBranchNotFound);
            }
            var duplicate =
                await _adjustmentIndexRepository.ExistsByReferenceAndCode(request.AdjustmentReferenceId, request.Code, null, ct);

            if (duplicate)
                return Result.Failure<AdjustmentIndex>(AdjustmentErrors.DuplicateCode);

            var entity = new AdjustmentIndex(
                adjustmentReference.Id,
                request.YearId,
                branch,
                season,
                request.Code,
                request.Title,
                request.Description,
                request.DocumentFile);

            foreach (var value in request.Values)
            {
                var adjustmentIndexValue = new AdjustmentIndexValue(
                        entity,
                        value.YearName,
                        value.Type,
                        value.Value,
                        null,
                        null,
                        null,
                        value.Period,
                        true);
                entity.AddValue(adjustmentIndexValue);
            }
            await _adjustmentIndexRepository.Create(entity, ct);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateAdjustmentIndexCommand");
            return Result.Failure<AdjustmentIndex>(
                SharedErrors.UnknownError);
        }
    }

    private async Task<Result<UpdateAdjustmentIndexResponse?>> UpdateAdjustmentIndexCommand(UpdateAdjustmentIndexRequest request, CT ct)
    {
        try
        {
            var entity = await _adjustmentIndexRepository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<UpdateAdjustmentIndexResponse>(
                    AdjustmentErrors.AdjustmentIndexNotFound);

            var adjustmentReference =
                await _adjustmentReferenceRepository.GetById(
                    request.AdjustmentReferenceId,
                    ct);

            if (adjustmentReference is null)
                return Result.Failure<UpdateAdjustmentIndexResponse>(AdjustmentErrors.AdjustmentReferenceNotFound);

            var branch = await _branchRepository.FindById(request.BranchId, ct);
            if (branch is null)
                return Result.Failure<UpdateAdjustmentIndexResponse>(BranchErrors.BranchWithIdNotFound);
            Season? season = null;
            if (request.SeasonId.HasValue)
            {
                season = await _seasonRepository.FindById(request.SeasonId.Value, ct);
                if (season is null)
                    return Result.Failure<UpdateAdjustmentIndexResponse>(SeasonErrors.SeasonNotFound);

                if (season.BranchId != branch.Id)
                    return Result.Failure<UpdateAdjustmentIndexResponse>(SeasonErrors.SeasonBranchNotFound);
            }

            var duplicate =
                await _adjustmentIndexRepository.ExistsByReferenceAndCode(request.AdjustmentReferenceId, request.Code, request.Id, ct);

            if (duplicate)
                return Result.Failure<UpdateAdjustmentIndexResponse>(AdjustmentErrors.DuplicateCode);

            entity.Update(
                request.YearId,
                branch,
                season,
                request.Code,
                request.Title,
                request.Description,
                request.DocumentFile);

            if (request.IsActive)
                entity.Activate();
            else
                entity.Deactivate();

            foreach (var valueRequest in request.Values)
            {
                if (!valueRequest.Id.HasValue)
                {
                    var adjustmentIndexValue = new AdjustmentIndexValue(
                      entity,
                      valueRequest.YearName,
                      valueRequest.Type,
                      valueRequest.Value,
                       valueRequest.Coefficient,
                       valueRequest.NotificationNumber,
                       valueRequest.NotificationDate,
                      valueRequest.Period,
                      valueRequest.IsActive);
                    entity.AddValue(adjustmentIndexValue);

                    continue;
                }

                var value =
                    entity.Values.FirstOrDefault(x => x.Id == valueRequest.Id.Value);

                if (value is null)
                    return Result.Failure<UpdateAdjustmentIndexResponse>(AdjustmentErrors.AdjustmentIndexValueNotFound);

                value.Update(
                    valueRequest.YearName,
                    valueRequest.Type,
                    valueRequest.Value,
                    valueRequest.Coefficient,
                    valueRequest.NotificationNumber,
                    valueRequest.NotificationDate,
                    valueRequest.Period,
                    valueRequest.IsActive);
            }

            await _adjustmentIndexRepository.Update(entity);
            return new UpdateAdjustmentIndexResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateAdjustmentIndexCommand");
            return Result.Failure<UpdateAdjustmentIndexResponse>(
                SharedErrors.UnknownError);
        }
    }

    private async Task<Result<DeleteAdjustmentIndexResponse?>>
    DeleteAdjustmentIndexCommand(DeleteAdjustmentIndexRequest request, CT ct)
    {
        try
        {
            var entity = await _adjustmentIndexRepository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteAdjustmentIndexResponse>(AdjustmentErrors.AdjustmentIndexNotFound);
            entity.SoftDelete();
            await _adjustmentIndexRepository.Update(entity);
            return new DeleteAdjustmentIndexResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteAdjustmentIndexCommand");
            return Result.Failure<DeleteAdjustmentIndexResponse>(
                SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetAdjustmentIndexByIdResponse?>> GetAdjustmentIndexByIdCommand(GetAdjustmentIndexByIdRequest request, CT ct)
    {
        try
        {
            var result = await _adjustmentIndexRepository.GetByIdForApi(request.Id, ct);

            if (result is null)
                return Result.Failure<GetAdjustmentIndexByIdResponse>(
                    AdjustmentErrors.AdjustmentIndexNotFound);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAdjustmentIndexByIdCommand");
            return Result.Failure<GetAdjustmentIndexByIdResponse>(
                SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetAdjustmentIndexesResponse?>>
    GetAdjustmentIndexesCommand(GetAdjustmentIndexesRequest request, CT ct)
    {
        try
        {
            request = request with
            {
                PageIndex = request.PageIndex ?? 1,
                PageSize = request.PageSize ?? 20
            };

            return await _adjustmentIndexRepository.GetAdjustmentIndexes(request, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error in GetAdjustmentIndexesCommand");

            return Result.Failure<GetAdjustmentIndexesResponse>(
                SharedErrors.UnknownError);
        }
    }
    //
}
