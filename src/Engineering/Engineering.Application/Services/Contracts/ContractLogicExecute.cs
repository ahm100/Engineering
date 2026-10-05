using Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentIndexState;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentReferenceState;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractGuaranteeStatus;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractStatus;
using Engineering.Application.Services.Contracts.Contracts.ContractChanges;
using Engineering.Application.Services.Contracts.Contracts.ContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;
using Engineering.Application.Services.Contracts.Contracts.CreateContract;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentIndex;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentReference;
using Engineering.Application.Services.Contracts.Contracts.CreateContractChange;
using Engineering.Application.Services.Contracts.Contracts.CreateContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.CreateContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.CreateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.CreateContractSummaryChange;
using Engineering.Application.Services.Contracts.Contracts.CreateContractType;
using Engineering.Application.Services.Contracts.Contracts.CreateContractTypeDetail;
using Engineering.Application.Services.Contracts.Contracts.DeleteContract;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractChange;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractType;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractTypeDetail;
using Engineering.Application.Services.Contracts.Contracts.FinalizeContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferenceById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;
using Engineering.Application.Services.Contracts.Contracts.GetContractById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChanges;
using Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationById;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;
using Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;
using Engineering.Application.Services.Contracts.Contracts.GetContractStructure;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;
using Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;
using Engineering.Application.Services.Contracts.Contracts.UpdateContract;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentIndex;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentReference;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractChange;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractSummaryChange;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractType;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractTypeDetail;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Application.Services.Contracts.Models.ContractChanges;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdNoIncluding;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Gita.Backend.Shared.Domain.Base;
using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;
using ContractTypeDetailEntity = Engineering.Domain.Entities.Contracts.ContractTypeDetail;
using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;

namespace Engineering.Application.Services.Contracts;

public partial class ContractLogic
{
    #region ContractChange Commands and Queries

    private async Task<Result<ContractChange>> CreateContractChangeExecute(
        CreateContractChangeRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithChangesForMutation(request.ContractId, validCompanyId, ct);
            if (contract is null)
                return Result.Failure<ContractChange>(ContractErrors.ContractNotFound)!;

            var lifecycleResult = contract.EnsureContractChangeCanBeCreated();

            if (lifecycleResult.IsFailure)
                return lifecycleResult.Failure<ContractChange>()!;

            if (await _contractChangeRepository.IsContractChangeNumberDuplicate(
                    request.ContractId, request.Number, null, validCompanyId, ct))
                return Result.Failure<ContractChange>(ContractErrors.ContractChangeNumberAlreadyExists)!;

            var mutationContext = await _contractChangeRepository.GetContractChangeMutationContext(
                request.ContractId,
                null,
                validCompanyId,
                ct);

            if (request.Date < contract.StartDate ||
                mutationContext.LatestChangeDate.HasValue &&
                request.Date < mutationContext.LatestChangeDate.Value)
                return Result.Failure<ContractChange>(ContractErrors.ContractChangeDateInvalid)!;

            var itemTermsResult = await ResolveContractChangeItemTerms(
                contract,
                request.Items,
                null,
                validCompanyId,
                ct);

            if (itemTermsResult.IsBad())
                return itemTermsResult.Failure<ContractChange>()!;

            var itemTerms = itemTermsResult.Value;
            var financialChangeAmount = ContractFinancialMath.NormalizeMoney(
                _contractChangeRepository.CalculateContractChangeAmount(itemTerms!));
            var previousContractAmount = contract.CalculateCurrentLegalAmount();
            var proposedFinalAmount = ContractFinancialMath.NormalizeMoney(
                previousContractAmount + financialChangeAmount);
            var proposedDuration = contract.CalculateCurrentDuration() + (request.DurationChange ?? 0);

            var stateError = ValidateProposedContractChangeState(
                contract,
                proposedFinalAmount,
                proposedDuration);

            if (stateError is not null)
                return Result.Failure<ContractChange>(stateError)!;

            return contract.AddContractChange(new ContractChangeTerms(
                request.Type,
                request.Number,
                request.Date,
                request.Subject,
                request.DurationChange,
                previousContractAmount,
                financialChangeAmount,
                proposedFinalAmount,
                request.Urls,
                itemTerms!));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractChange>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractChangeExecute(
        UpdateContractChangeRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithChangesForMutation(request.ContractId, validCompanyId, ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var mutationContext = await _contractChangeRepository.GetContractChangeMutationContext(
                request.ContractId,
                request.Id,
                validCompanyId,
                ct);

            if (!mutationContext.TargetExists)
                return Result.Failure<bool>(ContractErrors.ContractChangeNotFound);

            if (mutationContext.LatestChangeId != request.Id)
                return Result.Failure<bool>(ContractErrors.ContractChangeOnlyLatestCanBeModified);

            if (mutationContext.TargetMode != ContractChangeMode.Detailed)
                return Result.Failure<bool>(ContractErrors.ContractChangeModeInvalid);

            if (await _contractChangeRepository.IsContractChangeNumberDuplicate(
                    request.ContractId,
                    request.Number,
                    request.Id,
                    validCompanyId,
                    ct))
            {
                return Result.Failure<bool>(ContractErrors.ContractChangeNumberAlreadyExists);
            }

            if (request.Date < contract.StartDate ||
                mutationContext.PreviousChangeDate.HasValue &&
                request.Date < mutationContext.PreviousChangeDate.Value)
            {
                return Result.Failure<bool>(ContractErrors.ContractChangeDateInvalid);
            }

            var itemTermsResult = await ResolveContractChangeItemTerms(
                contract,
                request.Items,
                request.Id,
                validCompanyId,
                ct);

            if (itemTermsResult.IsBad())
                return itemTermsResult.Failure<bool>()!;

            var itemTerms = itemTermsResult.Value;
            var replacementAmount = ContractFinancialMath.NormalizeMoney(
                _contractChangeRepository.CalculateContractChangeAmount(itemTerms!));
            var previousContractAmount = ContractFinancialMath.NormalizeMoney(
                contract.CalculateCurrentLegalAmount() -
                mutationContext.TargetFinancialChangeAmount);
            var proposedFinalAmount = ContractFinancialMath.NormalizeMoney(
                previousContractAmount + replacementAmount);
            var proposedDuration =
                contract.CalculateCurrentDuration() -
                (mutationContext.TargetDurationChange ?? 0) +
                (request.DurationChange ?? 0);

            var stateError = ValidateProposedContractChangeState(
                contract,
                proposedFinalAmount,
                proposedDuration);

            if (stateError is not null)
                return Result.Failure<bool>(stateError);

            var updateResult = contract.UpdateContractChange(
                request.Id,
                new ContractChangeTerms(
                    request.Type,
                    request.Number,
                    request.Date,
                    request.Subject,
                    request.DurationChange,
                    previousContractAmount,
                    replacementAmount,
                    proposedFinalAmount,
                    request.Urls,
                    itemTerms!));

            if (updateResult.IsFailure)
                return updateResult.Failure<bool>()!;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> DeleteContractChangeExecute(
        DeleteContractChangeRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithChangesForMutation(request.ContractId, validCompanyId, ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var mutationContext = await _contractChangeRepository.GetContractChangeMutationContext(
                request.ContractId,
                request.Id,
                validCompanyId,
                ct);

            if (!mutationContext.TargetExists)
                return Result.Failure<bool>(ContractErrors.ContractChangeNotFound);

            if (mutationContext.LatestChangeId != request.Id)
                return Result.Failure<bool>(ContractErrors.ContractChangeOnlyLatestCanBeModified);

            var omittedContexts = await _contractChangeRepository.GetContractChangeOmittedItemContexts(
                request.ContractId,
                request.Id,
                validCompanyId,
                ct);
            var capacityTransitions =
                _contractChangeRepository.BuildOmittedCapacityTransitions(
                    omittedContexts,
                    [],
                    []);
            var sourceContexts = await GetContractChangeSourceContexts(
                contract.ProjectId,
                contract.ContractPartyId,
                _contractChangeRepository.GetCapacityTransitionSourceKeys(
                    capacityTransitions),
                validCompanyId,
                ct);
            var capacityStatus =
                _contractChangeRepository.ValidateContractChangeCapacityTransitions(
                    capacityTransitions,
                    sourceContexts);
            var capacityError =
                MapContractChangeCapacityValidationStatus(capacityStatus);

            if (capacityError is not null)
                return Result.Failure<bool>(capacityError);

            var proposedFinalAmount = ContractFinancialMath.NormalizeMoney(
                contract.CalculateCurrentLegalAmount() -
                mutationContext.TargetFinancialChangeAmount);
            var proposedDuration =
                contract.CalculateCurrentDuration() -
                (mutationContext.TargetDurationChange ?? 0);

            var stateError = ValidateProposedContractChangeState(
                contract,
                proposedFinalAmount,
                proposedDuration);

            if (stateError is not null)
                return Result.Failure<bool>(stateError);

            contract.RemoveContractChange(request.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<ContractChangeItemTerms>>> ResolveContractChangeItemTerms(
        ContractEntity contract,
        IReadOnlyCollection<ContractChangeItemRequest>? requests,
        long? excludedContractChangeId,
        long validCompanyId,
        CT ct)
    {
        var values = requests ?? [];

        var requestProjection =
            _contractChangeRepository.GetContractChangeRequestProjection(values);

        if (requestProjection.HasDuplicateItems)
        {
            return Result.Failure<List<ContractChangeItemTerms>>(
                ContractErrors.ContractChangeItemDuplicate)!;
        }

        var detailIds = requestProjection.ContractTypeDetailIds;
        var sourceTypeIds = requestProjection.SourceContractTypeIds;
        var sourceIds = requestProjection.SourceIds;

        var detailContexts = await _contractTypeDetailRepository.GetContractChangeDetailResolutionContexts(
            contract.Id,
            excludedContractChangeId,
            detailIds,
            validCompanyId,
            ct);
        var typeContexts = await _contractChangeRepository.GetContractChangeTypeContexts(
            contract.Id,
            sourceTypeIds,
            validCompanyId,
            ct);
        var baselineConstructionSourceIds = await _contractTypeDetailRepository.GetBaselineConstructionSourceIds(
            contract.Id,
            sourceIds,
            validCompanyId,
            ct);
        var sourceHistoryContexts = await _contractChangeRepository.GetContractChangeSourceHistoryContexts(
            contract.Id,
            excludedContractChangeId,
            sourceTypeIds,
            sourceIds,
            validCompanyId,
            ct);

        var replacementContractTypeDetailIds =
            requestProjection.ContractTypeDetailIds;
        var replacementSourceIds =
            requestProjection.ReplacementSourceKeys;
        var omittedContexts = excludedContractChangeId.HasValue
            ? await _contractChangeRepository.GetContractChangeOmittedItemContexts(
                contract.Id,
                excludedContractChangeId.Value,
                validCompanyId,
                ct)
            : [];
        var capacityDeltas =
            new List<(ContractTypeKind Kind, long SourceId, decimal Delta)>(
                _contractChangeRepository.BuildOmittedCapacityTransitions(
                    omittedContexts,
                    replacementContractTypeDetailIds,
                    replacementSourceIds));
        var requestedSources =
            new List<(ContractTypeKind Kind, long SourceId)>(
                _contractChangeRepository.GetCapacityTransitionSourceKeys(
                    capacityDeltas));

        foreach (var request in values)
        {
            if (request.ContractTypeDetailId.HasValue)
            {
                var detailContext = _contractChangeRepository.FindContractChangeDetailContext(
                    detailContexts,
                    request.ContractTypeDetailId.Value);

                if (detailContext is null)
                {
                    return Result.Failure<List<ContractChangeItemTerms>>(
                        ContractErrors.ContractChangeItemInvalid)!;
                }

                if (detailContext.PricingMethod != PricingMethod.LumpSum)
                    requestedSources.Add((detailContext.Kind, detailContext.SourceId));

                continue;
            }

            if (request.SourceItem is not null)
                requestedSources.Add((ContractTypeKind.Construction, request.SourceItem.SourceId));
        }

        var sourceContexts = await GetContractChangeSourceContexts(
            contract.ProjectId,
            contract.ContractPartyId,
            requestedSources,
            validCompanyId,
            ct);
        var result = new List<ContractChangeItemTerms>(values.Count);

        foreach (var request in values)
        {
            if (request.ContractTypeDetailId.HasValue)
            {
                var detailContext = _contractChangeRepository.FindContractChangeDetailContext(
                    detailContexts,
                    request.ContractTypeDetailId.Value);

                if (detailContext is null)
                {
                    return Result.Failure<List<ContractChangeItemTerms>>(
                        ContractErrors.ContractChangeItemInvalid)!;
                }

                if (detailContext.PricingMethod == PricingMethod.CostPlus)
                {
                    return Result.Failure<List<ContractChangeItemTerms>>(
                        ContractErrors.ContractPricingMethodCostPlusDisabled)!;
                }

                var detailPreviousValue = detailContext.PriorNewValue ??
                    (detailContext.PricingMethod == PricingMethod.LumpSum
                        ? detailContext.BaselineFixedAmount!.Value
                        : detailContext.BaselineQuantity);

                if (request.NewValue == detailPreviousValue)
                {
                    return Result.Failure<List<ContractChangeItemTerms>>(
                        ContractErrors.ContractChangeItemNoEffectiveChange)!;
                }

                if (detailContext.PricingMethod is PricingMethod.UnitPrice or PricingMethod.TimeAndMaterial)
                {
                    if (!detailContext.UnitPrice.HasValue || detailContext.UnitPrice.Value <= 0 ||
                        detailContext.SourceId <= 0)
                    {
                        return Result.Failure<List<ContractChangeItemTerms>>(
                            ContractErrors.ContractChangeItemInvalid)!;
                    }

                    var detailSourceContext = _contractChangeRepository.FindContractChangeSourceContext(
                        sourceContexts,
                        detailContext.Kind,
                        detailContext.SourceId);

                    if (detailSourceContext is null)
                    {
                        return Result.Failure<List<ContractChangeItemTerms>>(
                            ContractErrors.ContractChangeItemInvalid)!;
                    }

                    var detailCurrentEffectiveValue =
                        detailContext.CurrentNewValue ?? detailContext.BaselineQuantity;
                    capacityDeltas.Add((
                        NormalizeContractChangeCapacityKind(detailContext.Kind),
                        detailContext.SourceId,
                        request.NewValue - detailCurrentEffectiveValue));
                }

                result.Add(new ContractChangeItemTerms(
                    detailContext.ContractTypeDetailId,
                    null,
                    null,
                    detailContext.PricingMethod,
                    detailPreviousValue,
                    request.NewValue,
                    detailContext.UnitOfMeasurementId,
                    detailContext.PricingMethod == PricingMethod.LumpSum
                        ? null
                        : detailContext.UnitPrice));
                continue;
            }

            var sourceRequest = request.SourceItem!;
            var typeContext = _contractChangeRepository.FindContractChangeTypeContext(
                typeContexts,
                sourceRequest.ContractTypeId);

            if (typeContext is null)
                return Result.Failure<List<ContractChangeItemTerms>>(ContractErrors.ContractTypeNotFound)!;

            if (typeContext.PricingMethod == PricingMethod.CostPlus)
            {
                return Result.Failure<List<ContractChangeItemTerms>>(
                    ContractErrors.ContractPricingMethodCostPlusDisabled)!;
            }

            if (typeContext.Kind != ContractTypeKind.Construction ||
                typeContext.PricingMethod is not (PricingMethod.UnitPrice or PricingMethod.TimeAndMaterial) ||
                baselineConstructionSourceIds.Contains(sourceRequest.SourceId))
            {
                return Result.Failure<List<ContractChangeItemTerms>>(
                    ContractErrors.ContractChangeSourceItemNotSupported)!;
            }

            var historyContext = _contractChangeRepository.FindContractChangeSourceHistoryContext(
                sourceHistoryContexts,
                sourceRequest.ContractTypeId,
                sourceRequest.SourceId);
            var sourceResolutionContext = _contractChangeRepository.FindContractChangeSourceContext(
                sourceContexts,
                ContractTypeKind.Construction,
                sourceRequest.SourceId);

            if (sourceResolutionContext is null)
                return Result.Failure<List<ContractChangeItemTerms>>(ContractErrors.ContractChangeItemInvalid)!;

            var sourcePreviousValue = historyContext?.PriorNewValue ?? 0m;

            if (request.NewValue == sourcePreviousValue)
            {
                return Result.Failure<List<ContractChangeItemTerms>>(
                    ContractErrors.ContractChangeItemNoEffectiveChange)!;
            }

            var unitPrice = historyContext?.PriorUnitPrice ?? sourceResolutionContext.EstimatedUnitPrice;
            var unitOfMeasurementId =
                historyContext?.PriorUnitOfMeasurementId ?? sourceResolutionContext.UnitOfMeasurementId;

            if (!unitPrice.HasValue || unitPrice.Value <= 0)
            {
                return Result.Failure<List<ContractChangeItemTerms>>(
                    ContractErrors.ContractChangeSourceUnitPriceNotFound)!;
            }

            if (!unitOfMeasurementId.HasValue)
                return Result.Failure<List<ContractChangeItemTerms>>(ContractErrors.ContractChangeItemInvalid)!;

            var sourceCurrentEffectiveValue = historyContext?.CurrentNewValue ?? 0m;
            capacityDeltas.Add((
                ContractTypeKind.Construction,
                sourceRequest.SourceId,
                request.NewValue - sourceCurrentEffectiveValue));

            result.Add(new ContractChangeItemTerms(
                null,
                typeContext.ContractTypeId,
                sourceRequest.SourceId,
                typeContext.PricingMethod,
                sourcePreviousValue,
                request.NewValue,
                unitOfMeasurementId,
                unitPrice));
        }

        var capacityStatus =
            _contractChangeRepository.ValidateContractChangeCapacityTransitions(
                capacityDeltas,
                sourceContexts);
        var capacityError =
            MapContractChangeCapacityValidationStatus(capacityStatus);

        if (capacityError is not null)
            return Result.Failure<List<ContractChangeItemTerms>>(capacityError)!;

        return result;
    }

    private Task<List<ContractChangeSourceContextModel>> GetContractChangeSourceContexts(
        long projectId,
        long contractPartyId,
        IReadOnlyCollection<(ContractTypeKind Kind, long SourceId)> requestedSources,
        long validCompanyId,
        CT ct)
    {
        return _contractChangeRepository.GetContractChangeSourceContexts(
            projectId,
            contractPartyId,
            requestedSources,
            validCompanyId,
            ct);
    }

    private static Error? ValidateProposedContractChangeState(
        ContractEntity contract,
        decimal proposedFinalAmount,
        int proposedDuration)
    {
        if (proposedFinalAmount < 0)
            return ContractErrors.ContractChangeWouldMakeContractAmountNegative;

        if (proposedDuration <= 0)
            return ContractErrors.ContractChangeDurationInvalid;

        if (contract.FinancialInformation?.ContractCeilingAmount is decimal ceiling &&
            proposedFinalAmount > ceiling)
        {
            return ContractErrors.ContractFinancialCeilingAmountCannotBeLessThanCurrentContractAmount;
        }

        return null;
    }

    private async Task<Result<GetContractChangeByIdResponse?>> GetContractChangeByIdExecute(
        GetContractChangeByIdRequest request,
        long validCompanyId,
        CT ct)
    {
        var response = await _contractChangeRepository.GetContractChangeById(
            request.ContractId,
            request.Id,
            validCompanyId,
            ct);
        return response is null
            ? Result.Failure<GetContractChangeByIdResponse>(ContractErrors.ContractChangeNotFound)
            : response;
    }

    private async Task<Result<GetContractChangesResponse?>> GetContractChangesExecute(
        GetContractChangesRequest request,
        long validCompanyId,
        CT ct)
    {
        var (data, rowCount) = await _contractChangeRepository.GetContractChanges(
            request.ContractId,
            request.Type,
            request.DateFrom,
            request.DateTo,
            request.FilterData,
            validCompanyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            ct);
        return new GetContractChangesResponse(data, rowCount);
    }

    private async Task<Result<GetContractChangeAvailableItemsResponse?>>
        GetContractChangeAvailableItemsExecute(
            GetContractChangeAvailableItemsRequest request,
            long validCompanyId,
            CT ct)
    {
        var response = await _contractChangeRepository.GetContractChangeAvailableItems(
            request.ContractId,
            request.ContractTypeId,
            request.ProjectOperationId,
            request.FilterData,
            validCompanyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            ct);

        if (response is null)
            return Result.Failure<GetContractChangeAvailableItemsResponse>(ContractErrors.ContractTypeNotFound);

        if (response.PricingMethod == PricingMethod.CostPlus)
        {
            return Result.Failure<GetContractChangeAvailableItemsResponse>(
                ContractErrors.ContractPricingMethodCostPlusDisabled);
        }

        return response;
    }

    #endregion

    #region ContractFinancialInformation Commands and Queries

    private async Task<Result<ContractFinancialInformation>> CreateContractFinancialInformationExecute(
        CreateContractFinancialInformationRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<ContractFinancialInformation>(ContractErrors.ContractNotFound)!;

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<ContractFinancialInformation>()!;

            if (!_contractRepository.HasActiveContractTypes(contract))
            {
                return Result.Failure<ContractFinancialInformation>(
                    ContractErrors.ContractMustHaveAtLeastOneType)!;
            }

            if (contract.FinancialInformation is not null &&
                !contract.FinancialInformation.IsDeleted)
            {
                return Result.Failure<ContractFinancialInformation>(
                    ContractErrors.ContractFinancialInformationAlreadyExists)!;
            }

            var adjustmentEligibilityResult =
                contract.EnsureAdjustmentEligibilityIsValid(
                    request.IsSubjectToAdjustment);

            if (adjustmentEligibilityResult.IsFailure)
            {
                return adjustmentEligibilityResult
                    .Failure<ContractFinancialInformation>()!;
            }

            var currency = await _currencyRepository.GetById(request.CurrencyId, ct);

            if (currency is null)
            {
                return Result.Failure<ContractFinancialInformation>(
                    ContractErrors.ContractFinancialCurrencyNotFound)!;
            }

            var calculatedInitialAmount = contract.CalculateInitialAmount();
            var calculatedCurrentContractAmount = contract.CalculateFinalContractAmount();

            if (_contractRepository.RequiresContractCeilingAmount(contract) &&
                !request.ContractCeilingAmount.HasValue)
            {
                return Result.Failure<ContractFinancialInformation>(
                    ContractErrors.ContractFinancialCeilingAmountIsRequired)!;
            }

            var calculatedAmountsError = ValidateCalculatedFinancialAmounts(
                calculatedInitialAmount,
                calculatedCurrentContractAmount,
                request.HasPrepayment,
                request.ContractCeilingAmount);

            if (calculatedAmountsError is not null)
                return Result.Failure<ContractFinancialInformation>(calculatedAmountsError)!;

            return contract.AddFinancialInformation(
                calculatedInitialAmount,
                calculatedCurrentContractAmount,
                request.CurrencyId,
                request.HasPrepayment,
                request.IsSubjectToAdjustment,
                request.ContractCeilingAmount,
                request.AdjustmentLimitValue,
                request.AdjustmentLimitType,
                request.PrepaymentPercentage,
                request.PrepaymentAmortizationMethod,
                request.PrepaymentAmortizationValue,
                request.PrepaymentStartStatusStatementNumber,
                request.PrepaymentStartProgressPercentage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractFinancialInformation>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractFinancialInformationExecute(
        UpdateContractFinancialInformationRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<bool>()!;

            if (!_contractRepository.HasActiveContractTypes(contract))
                return Result.Failure<bool>(ContractErrors.ContractMustHaveAtLeastOneType);

            if (contract.FinancialInformation is null ||
                contract.FinancialInformation.IsDeleted)
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractFinancialInformationNotFound);
            }

            var adjustmentEligibilityResult =
                contract.EnsureAdjustmentEligibilityIsValid(
                    request.IsSubjectToAdjustment);

            if (adjustmentEligibilityResult.IsFailure)
                return adjustmentEligibilityResult.Failure<bool>()!;

            var currency = await _currencyRepository.GetById(request.CurrencyId, ct);

            if (currency is null)
                return Result.Failure<bool>(ContractErrors.ContractFinancialCurrencyNotFound);

            var calculatedInitialAmount = contract.CalculateInitialAmount();
            var calculatedCurrentContractAmount = contract.CalculateFinalContractAmount();

            if (_contractRepository.RequiresContractCeilingAmount(contract) &&
                !request.ContractCeilingAmount.HasValue)
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractFinancialCeilingAmountIsRequired);
            }

            var calculatedAmountsError = ValidateCalculatedFinancialAmounts(
                calculatedInitialAmount,
                calculatedCurrentContractAmount,
                request.HasPrepayment,
                request.ContractCeilingAmount);

            if (calculatedAmountsError is not null)
                return Result.Failure<bool>(calculatedAmountsError);

            var updateResult = contract.UpdateFinancialInformation(
                calculatedInitialAmount,
                calculatedCurrentContractAmount,
                request.CurrencyId,
                request.HasPrepayment,
                request.IsSubjectToAdjustment,
                request.ContractCeilingAmount,
                request.AdjustmentLimitValue,
                request.AdjustmentLimitType,
                request.PrepaymentPercentage,
                request.PrepaymentAmortizationMethod,
                request.PrepaymentAmortizationValue,
                request.PrepaymentStartStatusStatementNumber,
                request.PrepaymentStartProgressPercentage);

            if (updateResult.IsFailure)
                return updateResult.Failure<bool>()!;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractFinancialInformationResponse?>>
        GetContractFinancialInformationExecute(
            GetContractFinancialInformationRequest request,
            long validCompanyId,
            CT ct)
    {
        var response = await _contractFinancialInformationRepository.GetContractFinancialInformation(
            request.ContractId,
            validCompanyId,
            ct);

        if (response is null)
        {
            return Result.Failure<GetContractFinancialInformationResponse>(
                ContractErrors.ContractFinancialInformationNotFound);
        }

        return response;
    }

    #endregion

    #region Contract Commands and Queries

    private async Task<Result<ContractEntity>> CreateContractExecute(
        CreateContractRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var projectRes = await _mediator.Send(new GetProjectByIdNoIncludingQuery(request.ProjectId), ct);
            if (projectRes.IsBad())
                return projectRes.Failure<ContractEntity>()!;

            if (projectRes.Value!.CompanyId != validCompanyId)
                return Result.Failure<ContractEntity>(ContractErrors.ProjectDoesNotBelongToCompany)!;

            var thirdPartyRes = await _mediator.Send(new GetThirdPartyByIdQuery(request.ContractPartyId), ct);

            if (thirdPartyRes.IsBad() || thirdPartyRes.Value is null)
                return Result.Failure<ContractEntity>(ContractErrors.ContractPartyNotFound)!;

            var contract = new ContractEntity(
                validCompanyId,
                request.ProjectId,
                request.ContractPartyId,
                request.FaTitle,
                request.EnTitle,
                request.Description,
                request.StartDate,
                request.Duration,
                request.DurationUnit);

            if (request.Urls.HasAny())
                contract.AddDocuments(request.Urls!);

            await _contractRepository.CreateContract(contract, ct);

            return contract;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractEntity>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractExecute(
        UpdateContractRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = request.Urls is null
                ? await _contractRepository.GetContract(request.Id, validCompanyId, ct)
                : await _contractRepository.GetContractWithDocuments(request.Id, validCompanyId, ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<bool>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.Id, validCompanyId, ct))
                return Result.Failure<bool>(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            var projectRes = await _mediator.Send(new GetProjectByIdNoIncludingQuery(request.ProjectId), ct);

            if (projectRes.IsBad())
                return projectRes.Failure<bool>()!;

            if (projectRes.Value!.CompanyId != validCompanyId)
                return Result.Failure<bool>(ContractErrors.ProjectDoesNotBelongToCompany);

            var thirdPartyRes = await _mediator.Send(new GetThirdPartyByIdQuery(request.ContractPartyId), ct);

            if (thirdPartyRes.IsBad() || thirdPartyRes.Value is null)
                return Result.Failure<bool>(ContractErrors.ContractPartyNotFound);

            contract.UpdateReferenceInfo(request.FaTitle, request.EnTitle, request.Description);
            contract.ChangeProject(request.ProjectId);
            contract.ChangeContractParty(request.ContractPartyId);
            contract.UpdateSchedule(request.StartDate, request.Duration, request.DurationUnit);

            if (request.Urls is not null)
                contract.ReplaceDocuments(request.Urls);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> DeleteContractExecute(
        DeleteContractRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractForDelete(request.Id, validCompanyId, ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            contract.Delete();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> ChangeContractStatusExecute(
        ChangeContractStatusRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.Id,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var statusResult = request.Operation switch
            {
                ContractStatusTransitionType.SubmitForReview =>
                    contract.SubmitForReview(),
                ContractStatusTransitionType.ReturnToDraft =>
                    contract.ReturnToDraft(),
                ContractStatusTransitionType.Approve =>
                    contract.Approve(),
                ContractStatusTransitionType.Suspend =>
                    contract.Suspend(
                        request.EffectiveDate.GetValueOrDefault(),
                        request.SuspensionDurationMonths.GetValueOrDefault(),
                        request.Reason!,
                        request.Description,
                        request.Urls ?? []),
                ContractStatusTransitionType.Resume =>
                    contract.Resume(),
                ContractStatusTransitionType.Finish =>
                    contract.Finish(
                        request.EffectiveDate.GetValueOrDefault(),
                        request.Reason!,
                        request.Description,
                        request.Urls ?? []),
                ContractStatusTransitionType.Terminate =>
                    contract.Terminate(
                        request.EffectiveDate.GetValueOrDefault(),
                        request.Reason!,
                        request.Description,
                        request.Urls ?? []),
                _ => Result.Failure(ContractErrors.ContractStatusOperationInvalid)
            };

            if (statusResult.IsFailure)
                return statusResult.Failure<bool>();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> UpdateContractStructureExecute(
        UpdateContractStructureRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<bool>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(
                    request.ContractId,
                    validCompanyId,
                    ct))
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractBaselineCannotChangeAfterContractChange);
            }

            var projection =
                _contractRepository.GetContractStructureMutationProjection(
                    contract,
                    request);

            if (projection.ContainsCostPlus)
                return Result.Failure<bool>(
                    ContractErrors.ContractPricingMethodCostPlusDisabled);

            if (projection.HasUnknownContractType)
                return Result.Failure<bool>(ContractErrors.ContractTypeNotFound);

            if (projection.HasActiveDetailsConflict)
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractTypeStructureCannotChangeWithDetails);
            }

            var financialError = ValidateFinancialInformationForPricingMethods(
                contract.FinancialInformation,
                projection.RequiresContractCeilingAmount);

            if (financialError is not null)
                return Result.Failure<bool>(financialError);

            foreach (var contractTypeId in projection.TypeIdsToRemove)
                contract.RemoveContractType(contractTypeId);

            foreach (var item in request.Items)
            {
                if (item.Id.HasValue)
                {
                    contract.UpdateContractType(
                        item.Id.Value,
                        item.Kind,
                        item.PricingMethod);

                    continue;
                }

                contract.AddContractType(
                    item.Kind,
                    item.PricingMethod);
            }

            if (contract.FinancialInformation is not null &&
                !contract.FinancialInformation.IsDeleted)
            {
                var calculatedAmountsError = ValidateCalculatedFinancialAmounts(
                    contract.CalculateInitialAmount(),
                    contract.CalculateFinalContractAmount(),
                    contract.FinancialInformation.HasPrepayment,
                    contract.FinancialInformation.ContractCeilingAmount);

                if (calculatedAmountsError is not null)
                    return Result.Failure<bool>(calculatedAmountsError);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractByIdResponse?>> GetContractByIdExecute(
        GetContractByIdRequest request,
        long validCompanyId,
        CT ct)
    {
        var response = await _contractRepository.GetContractByIdForResponse(request.Id, validCompanyId, ct);

        if (response is null)
            return Result.Failure<GetContractByIdResponse>(ContractErrors.ContractNotFound);

        var thirdPartyRes = await _mediator.Send(new GetThirdPartyByIdQuery(response.ContractPartyId), ct);

        if (thirdPartyRes.IsBad() || thirdPartyRes.Value is null)
            return Result.Failure<GetContractByIdResponse>(ContractErrors.ContractPartyNotFound);

        response.ContractPartyName = thirdPartyRes.Value.FullName;

        var responseList = new List<GetContractByIdResponse> { response };
        await responseList.SetFullName(_mediator, ct);

        return response;
    }

    private async Task<Result<GetFilteredContractsResponse?>> GetFilteredContractsExecute(
        GetFilteredContractsRequest request,
        long validCompanyId,
        CT ct)
    {
        var (values, rowCount) = await _contractRepository.GetFilteredContracts(
            request.ContractNumber,
            request.FaTitle,
            request.EnTitle,
            request.ProjectId,
            request.ContractPartyId,
            request.Status,
            request.DurationUnit,
            request.StartDateFrom,
            request.StartDateTo,
            request.EndDateFrom,
            request.EndDateTo,
            request.FilterData,
            validCompanyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            ct);

        if (!values.HasAny())
            return new GetFilteredContractsResponse([], 0);

        await EnrichContractReadModelsAsync(
            values,
            oo => oo.ContractPartyId,
            (model, name) => model.ContractPartyName = name,
            ct);

        return new GetFilteredContractsResponse(values, rowCount);
    }

    private async Task<Result<GetContractsByStatusResponse?>> GetContractsByStatusExecute(
        GetContractsByStatusRequest request,
        long validCompanyId,
        CT ct)
    {
        var (values, rowCount) = await _contractRepository.GetContractsByStatus(
            request.Status,
            validCompanyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            ct);

        if (!values.HasAny())
            return new GetContractsByStatusResponse([], 0);

        await EnrichContractReadModelsAsync(
            values,
            oo => oo.ContractPartyId,
            (model, name) => model.ContractPartyName = name,
            ct);

        return new GetContractsByStatusResponse(values, rowCount);
    }

    private async Task<Result<GetContractStructureResponse?>> GetContractStructureExecute(
        GetContractStructureRequest request,
        long validCompanyId,
        CT ct)
    {
        var response = await _contractRepository.GetContractStructure(request.ContractId, validCompanyId, ct);

        if (response is null)
            return Result.Failure<GetContractStructureResponse>(ContractErrors.ContractNotFound);

        return response;
    }

    #endregion

    #region ContractType Commands and Queries

    private async Task<Result<ContractTypeEntity>> CreateContractTypeExecute(
        CreateContractTypeRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            if (request.PricingMethod == PricingMethod.CostPlus)
                return Result.Failure<ContractTypeEntity>(ContractErrors.ContractPricingMethodCostPlusDisabled)!;

            var contract = await _contractRepository.GetContractWithTypesAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<ContractTypeEntity>(ContractErrors.ContractNotFound)!;

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<ContractTypeEntity>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, validCompanyId, ct))
            {
                return Result.Failure<ContractTypeEntity>(
                    ContractErrors.ContractBaselineCannotChangeAfterContractChange)!;
            }

            if (_contractRepository.HasContractTypeKind(contract, request.Kind))
                return Result.Failure<ContractTypeEntity>(ContractErrors.ContractTypeKindIsDuplicate)!;

            var financialError = ValidateFinancialInformationForPricingMethods(
                contract.FinancialInformation,
                _contractRepository.HasPricingMethodRequiringCeiling(
                    contract,
                    request.PricingMethod));

            if (financialError is not null)
                return Result.Failure<ContractTypeEntity>(financialError)!;

            var contractType = contract.AddContractType(
                request.Kind,
                request.PricingMethod);

            return contractType;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractTypeEntity>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractTypeExecute(
        UpdateContractTypeRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            if (request.PricingMethod == PricingMethod.CostPlus)
                return Result.Failure<bool>(ContractErrors.ContractPricingMethodCostPlusDisabled);

            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<bool>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, validCompanyId, ct))
                return Result.Failure<bool>(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            var contractType = _contractRepository.GetContractTypeForMutation(
                contract,
                request.Id);

            if (contractType is null)
                return Result.Failure<bool>(ContractErrors.ContractTypeNotFound);

            if (_contractRepository.HasContractTypeKind(
                    contract,
                    request.Kind,
                    request.Id))
            {
                return Result.Failure<bool>(ContractErrors.ContractTypeKindIsDuplicate);
            }

            var hasActiveDetails =
                _contractRepository.HasActiveContractTypeDetails(contractType);

            if (hasActiveDetails &&
                (contractType.Kind != request.Kind ||
                 contractType.PricingMethod != request.PricingMethod))
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractTypeStructureCannotChangeWithDetails);
            }

            var financialError = ValidateFinancialInformationForPricingMethods(
                contract.FinancialInformation,
                _contractRepository.HasPricingMethodRequiringCeiling(
                    contract,
                    request.PricingMethod,
                    request.Id));

            if (financialError is not null)
                return Result.Failure<bool>(financialError);

            contract.UpdateContractType(
                request.Id,
                request.Kind,
                request.PricingMethod);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> DeleteContractTypeExecute(
        DeleteContractTypeRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<bool>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, validCompanyId, ct))
                return Result.Failure<bool>(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            if (_contractRepository.GetContractTypeForMutation(contract, request.Id) is null)
                return Result.Failure<bool>(ContractErrors.ContractTypeNotFound);

            if (contract.ContractTypes.Count == 1)
                return Result.Failure<bool>(ContractErrors.ContractMustHaveAtLeastOneType);

            contract.RemoveContractType(request.Id);

            if (contract.FinancialInformation is not null && !contract.FinancialInformation.IsDeleted)
            {
                var calculatedAmountsError = ValidateCalculatedFinancialAmounts(
                    contract.CalculateInitialAmount(),
                    contract.CalculateFinalContractAmount(),
                    contract.FinancialInformation.HasPrepayment,
                    contract.FinancialInformation.ContractCeilingAmount);

                if (calculatedAmountsError is not null)
                    return Result.Failure<bool>(calculatedAmountsError);

            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractTypeByIdResponse?>> GetContractTypeByIdExecute(
        GetContractTypeByIdRequest request,
        long validCompanyId,
        CT ct)
    {
        var response = await _contractTypeRepository.GetContractTypeById(
            request.ContractId,
            request.Id,
            validCompanyId,
            ct);

        if (response is null)
            return Result.Failure<GetContractTypeByIdResponse>(ContractErrors.ContractTypeNotFound);

        return response;
    }

    #endregion

    #region ContractTypeDetail Commands and Queries

    private async Task<Result<ContractTypeDetailEntity>> CreateContractTypeDetailExecute(
        CreateContractTypeDetailRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<ContractTypeDetailEntity>(ContractErrors.ContractNotFound)!;

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<ContractTypeDetailEntity>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, validCompanyId, ct))
            {
                return Result.Failure<ContractTypeDetailEntity>(
                    ContractErrors.ContractBaselineCannotChangeAfterContractChange)!;
            }

            var contractType = _contractRepository.GetContractTypeForMutation(
                contract,
                request.ContractTypeId);

            if (contractType is null)
                return Result.Failure<ContractTypeDetailEntity>(ContractErrors.ContractTypeNotFound)!;

            if (contractType.PricingMethod == PricingMethod.CostPlus)
            {
                return Result.Failure<ContractTypeDetailEntity>(
                    ContractErrors.ContractPricingMethodCostPlusDisabled)!;
            }

            if (HasActiveContractTypeDetailSource(contractType, request.SourceId))
                return Result.Failure<ContractTypeDetailEntity>(

                    ContractErrors.ContractTypeDetailSourceIsDuplicate)!;

            if (!HasValidContractTypeDetailTerms(
                contractType,
                request.UnitOfMeasurementId,
                request.UnitPrice,
                request.FixedAmount,
                request.TechnicalSpecifications,
                request.ExpectedDeliverables,
                request.Duration,
                request.DurationUnit))
            {
                return Result.Failure<ContractTypeDetailEntity>(
                    ContractErrors.ContractTypeDetailTermsInvalid)!;
            }

            var adjustmentEligibilityResult =
                contract.EnsureTypeDetailAdjustmentEligibilityIsValid(
                    request.IsSubjectToAdjustment,
                    request.Adjustment is not null);

            if (adjustmentEligibilityResult.IsFailure)
            {
                return adjustmentEligibilityResult
                    .Failure<ContractTypeDetailEntity>()!;
            }

            var adjustmentError =
                await ValidateAdjustmentDependencies(
                 request.Adjustment,
            ct);

            if (adjustmentError is not null)
            {
                return Result.Failure<ContractTypeDetailEntity>(
                    adjustmentError)!;
            }

            var adjustmentTerms = GetAdjustmentTerms(request.Adjustment);

            var source = await ResolveContractTypeDetailSource(
                contractType,
                request.SourceId,
                contract.ProjectId,
                contract.ContractPartyId,
                null,
                validCompanyId,
                ct);

            if (source is null)
                return Result.Failure<ContractTypeDetailEntity>(

                    ContractErrors.ContractTypeDetailSourceInvalid)!;

            if (contractType.Kind is ContractTypeKind.Engineering or ContractTypeKind.Services &&
                source.ContractorId.HasValue &&
                source.ContractorId.Value != contract.ContractPartyId)
            {
                return Result.Failure<ContractTypeDetailEntity>(
                    ContractErrors.ContractTypeDetailContractPartyMismatch)!;
            }

            if (source.AvailableQuantity <= 0)
                return Result.Failure<ContractTypeDetailEntity>(

                    ContractErrors.ContractTypeDetailSourceHasNoRemainingQuantity)!;

            if (request.Quantity > source.AvailableQuantity)
                return Result.Failure<ContractTypeDetailEntity>(

                    ContractErrors.ContractTypeDetailQuantityExceeded)!;

            var unitOfMeasurementId =
                contractType.Kind is ContractTypeKind.Procurement or ContractTypeKind.Construction
                    ? source.UnitOfMeasurementId
                    : request.UnitOfMeasurementId;

            if (contractType.Kind is ContractTypeKind.Procurement or ContractTypeKind.Construction &&
                !unitOfMeasurementId.HasValue)
            {
                return Result.Failure<ContractTypeDetailEntity>(
                    ContractErrors.ContractTypeDetailSourceInvalid)!;
            }

            var unitPrice = request.UnitPrice;

            if (contractType.PricingMethod == PricingMethod.CostPlus)
            {
                unitPrice ??= source.EstimatedUnitPrice;

                if (!unitPrice.HasValue)
                    return Result.Failure<ContractTypeDetailEntity>(

                        ContractErrors.ContractTypeDetailEstimateUnitPriceNotFound)!;
            }

            var detailResult = contract.AddContractTypeDetail(
                request.ContractTypeId,
                request.SourceId,
                request.Quantity,
                unitOfMeasurementId,
                unitPrice,
                request.FixedAmount,
                string.IsNullOrWhiteSpace(request.TechnicalSpecifications)
                    ? null
                    : request.TechnicalSpecifications,
                string.IsNullOrWhiteSpace(request.ExpectedDeliverables)
                    ? null
                    : request.ExpectedDeliverables,
                request.Duration,
                request.DurationUnit,
                request.IsSubjectToAdjustment,
                adjustmentTerms);

            if (detailResult.IsBad())
                return detailResult.Failure<ContractTypeDetailEntity>()!;

            var detail = detailResult.Value;

            if (contract.FinancialInformation is not null &&
                !contract.FinancialInformation.IsDeleted)
            {
                var calculatedAmountsError = ValidateCalculatedFinancialAmounts(
                    contract.CalculateInitialAmount(),
                    contract.CalculateFinalContractAmount(),
                    contract.FinancialInformation.HasPrepayment,
                    contract.FinancialInformation.ContractCeilingAmount);

                if (calculatedAmountsError is not null)
                    return Result.Failure<ContractTypeDetailEntity>(calculatedAmountsError)!;

            }

            return detail;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractTypeDetailEntity>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractTypeDetailExecute(
        UpdateContractTypeDetailRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<bool>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, validCompanyId, ct))
                return Result.Failure<bool>(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            var contractType = _contractRepository.GetContractTypeForMutation(
                contract,
                request.ContractTypeId);

            if (contractType is null)
                return Result.Failure<bool>(ContractErrors.ContractTypeNotFound);

            if (contractType.PricingMethod == PricingMethod.CostPlus)
                return Result.Failure<bool>(ContractErrors.ContractPricingMethodCostPlusDisabled);

            var detail = _contractTypeDetailRepository.GetContractTypeDetailForMutation(
                contractType,
                request.Id);

            if (detail is null)
                return Result.Failure<bool>(ContractErrors.ContractTypeDetailNotFound);

            if (!HasValidContractTypeDetailTerms(
                    contractType,
                    request.UnitOfMeasurementId,
                    request.UnitPrice,
                    request.FixedAmount,
                    request.TechnicalSpecifications,
                    request.ExpectedDeliverables,
                    request.Duration,
                    request.DurationUnit))
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractTypeDetailTermsInvalid);
            }

            var adjustmentEligibilityResult =
                contract.EnsureTypeDetailAdjustmentUpdateEligibilityIsValid(
                    request.ContractTypeId,
                    request.Id,
                    request.IsSubjectToAdjustment,
                    request.Adjustment is not null);

            if (adjustmentEligibilityResult.IsFailure)
                return adjustmentEligibilityResult.Failure<bool>()!;

            var adjustmentError =
                await ValidateAdjustmentDependencies(
                    request.Adjustment,
                    ct);

            if (adjustmentError is not null)
                return Result.Failure<bool>(adjustmentError);

            var adjustmentTerms = GetAdjustmentTerms(request.Adjustment);

            var sourceId = GetContractTypeDetailSourceId(contractType, detail);

            if (!sourceId.HasValue)
                return Result.Failure<bool>(

                    ContractErrors.ContractTypeDetailSourceInvalid);

            var source = await ResolveContractTypeDetailSource(
                contractType,
                sourceId.Value,
                contract.ProjectId,
                contract.ContractPartyId,
                detail.Id,
                validCompanyId,
                ct);

            if (source is null)
                return Result.Failure<bool>(

                    ContractErrors.ContractTypeDetailSourceInvalid);

            if (contractType.Kind is ContractTypeKind.Engineering or ContractTypeKind.Services &&
                source.ContractorId.HasValue &&
                source.ContractorId.Value != contract.ContractPartyId)
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractTypeDetailContractPartyMismatch);
            }

            if (source.AvailableQuantity <= 0)
                return Result.Failure<bool>(

                    ContractErrors.ContractTypeDetailSourceHasNoRemainingQuantity);

            if (request.Quantity > source.AvailableQuantity)
                return Result.Failure<bool>(

                    ContractErrors.ContractTypeDetailQuantityExceeded);

            var unitOfMeasurementId =
                contractType.Kind is ContractTypeKind.Procurement or ContractTypeKind.Construction
                    ? source.UnitOfMeasurementId
                    : request.UnitOfMeasurementId;

            if (contractType.Kind is ContractTypeKind.Procurement or ContractTypeKind.Construction &&
                !unitOfMeasurementId.HasValue)
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractTypeDetailSourceInvalid);
            }

            var unitPrice = request.UnitPrice;

            if (contractType.PricingMethod == PricingMethod.CostPlus)
            {
                unitPrice ??= detail.UnitPrice;
                unitPrice ??= source.EstimatedUnitPrice;

                if (!unitPrice.HasValue)
                    return Result.Failure<bool>(

                        ContractErrors.ContractTypeDetailEstimateUnitPriceNotFound);
            }

            var updateResult = contract.UpdateContractTypeDetail(
                request.ContractTypeId,
                request.Id,
                request.Quantity,
                unitOfMeasurementId,
                unitPrice,
                request.FixedAmount,
                string.IsNullOrWhiteSpace(request.TechnicalSpecifications)
                    ? null
                    : request.TechnicalSpecifications,
                string.IsNullOrWhiteSpace(request.ExpectedDeliverables)
                    ? null
                    : request.ExpectedDeliverables,
                request.Duration,
                request.DurationUnit,
                request.IsSubjectToAdjustment,
                adjustmentTerms);

            if (updateResult.IsFailure)
                return updateResult.Failure<bool>()!;

            if (contract.FinancialInformation is not null &&
                !contract.FinancialInformation.IsDeleted)
            {
                var calculatedAmountsError = ValidateCalculatedFinancialAmounts(
                    contract.CalculateInitialAmount(),
                    contract.CalculateFinalContractAmount(),
                    contract.FinancialInformation.HasPrepayment,
                    contract.FinancialInformation.ContractCeilingAmount);

                if (calculatedAmountsError is not null)
                    return Result.Failure<bool>(calculatedAmountsError);

            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> DeleteContractTypeDetailExecute(
        DeleteContractTypeDetailRequest request,
        long validCompanyId,
        CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId,
                validCompanyId,
                ct);

            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            var baselineEditabilityResult = contract.EnsureBaselineIsEditable();

            if (baselineEditabilityResult.IsFailure)
                return baselineEditabilityResult.Failure<bool>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, validCompanyId, ct))
                return Result.Failure<bool>(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            var contractType = _contractRepository.GetContractTypeForMutation(
                contract,
                request.ContractTypeId);

            if (contractType is null)
                return Result.Failure<bool>(ContractErrors.ContractTypeNotFound);

            if (_contractTypeDetailRepository.GetContractTypeDetailForMutation(
                    contractType,
                    request.Id) is null)
                return Result.Failure<bool>(

                    ContractErrors.ContractTypeDetailNotFound);

            contract.RemoveContractTypeDetail(
                request.ContractTypeId,
                request.Id);

            if (contract.FinancialInformation is not null && !contract.FinancialInformation.IsDeleted)
            {
                var calculatedAmountsError = ValidateCalculatedFinancialAmounts(
                    contract.CalculateInitialAmount(),
                    contract.CalculateFinalContractAmount(),
                    contract.FinancialInformation.HasPrepayment,
                    contract.FinancialInformation.ContractCeilingAmount);

                if (calculatedAmountsError is not null)
                    return Result.Failure<bool>(calculatedAmountsError);

            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractTypeDetailByIdResponse?>>
    GetContractTypeDetailByIdExecute(
        GetContractTypeDetailByIdRequest request,
        long validCompanyId,
        CT ct)
    {
        var response = await _contractTypeDetailRepository.GetContractTypeDetailById(
            request.ContractId,
            request.ContractTypeId,
            request.Id,
            validCompanyId,
            ct);

        if (response is null)
            return Result.Failure<GetContractTypeDetailByIdResponse>(

                ContractErrors.ContractTypeDetailNotFound);

        return response;
    }

    private async Task<Result<GetContractTypeDetailsResponse?>>
    GetContractTypeDetailsExecute(
        GetContractTypeDetailsRequest request,
        long validCompanyId,
        CT ct)
    {
        var response = await _contractTypeRepository.GetContractTypeDetails(
            request.ContractId,
            request.ContractTypeId,
            validCompanyId,
            ct);

        if (response is null)
            return Result.Failure<GetContractTypeDetailsResponse>(

                ContractErrors.ContractTypeNotFound);

        return response;
    }

    private async Task<Result<GetAvailableContractTypeDetailSourcesResponse?>>
    GetAvailableContractTypeDetailSourcesExecute(
        GetAvailableContractTypeDetailSourcesRequest request,
        long validCompanyId,
        CT ct)
    {
        var response = await _contractTypeDetailRepository.GetAvailableContractTypeDetailSources(
            request.ContractId,
            request.ContractTypeId,
            request.ProjectOperationId,
            request.FilterData,
            validCompanyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            ct);

        if (response is null)
            return Result.Failure<GetAvailableContractTypeDetailSourcesResponse>(

                ContractErrors.ContractTypeNotFound);

        if (response.PricingMethod == PricingMethod.CostPlus)
        {
            return Result.Failure<GetAvailableContractTypeDetailSourcesResponse>(
                ContractErrors.ContractPricingMethodCostPlusDisabled);
        }

        return response;
    }

    #endregion

    #region ContractAdjustmentReference Commands and Queries

    private async Task<Error?> ValidateAdjustmentDependencies(
        ContractTypeDetailAdjustmentRequest? adjustment,
        CT ct)
    {
        if (adjustment is null)
            return null;

        if (adjustment.Type ==
            ContractTypeDetailAdjustmentType.Currency)
        {
            var currency = await _currencyRepository.GetById(
                adjustment.Currency!.CurrencyId,
                ct);

            if (currency is null)
                return ContractErrors.ContractFinancialCurrencyNotFound;

        }

        if (adjustment.Type ==
            ContractTypeDetailAdjustmentType.PriceIndex)
        {
            var priceIndex = adjustment.PriceIndex!;

            var isValid =
                await _contractAdjustmentIndexRepository.HasActiveIndex(
                    priceIndex.ReferenceId,
                    priceIndex.IndexId,
                    ct);

            if (!isValid)
            {
                return ContractErrors
                    .ContractTypeDetailAdjustmentPriceIndexInvalid;
            }
        }

        return null;
    }

    private async Task<Result<ContractAdjustmentReference>> CreateContractAdjustmentReferenceExecute(
        CreateContractAdjustmentReferenceRequest request,
        CT ct)
    {
        try
        {
            if (await _contractAdjustmentReferenceRepository.IsContractAdjustmentReferenceCodeDuplicate(
                    request.Code,
                    null,
                    ct))
            {
                return Result.Failure<ContractAdjustmentReference>(
                    ContractErrors.ContractAdjustmentReferenceCodeAlreadyExists)!;
            }

            var entity = new ContractAdjustmentReference(
                request.Code,
                request.FaTitle,
                request.EnTitle,
                request.Description);

            await _contractAdjustmentReferenceRepository.CreateContractAdjustmentReference(entity, ct);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractAdjustmentReference>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractAdjustmentReferenceExecute(
        UpdateContractAdjustmentReferenceRequest request,
        CT ct)
    {
        try
        {
            var entity = await _contractAdjustmentReferenceRepository.GetContractAdjustmentReference(request.Id, ct);

            if (entity is null)
                return Result.Failure<bool>(ContractErrors.ReferenceNotFound);

            if (await _contractAdjustmentReferenceRepository.IsContractAdjustmentReferenceCodeDuplicate(
                    request.Code,
                    request.Id,
                    ct))
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractAdjustmentReferenceCodeAlreadyExists);
            }

            entity.Update(
                request.Code,
                request.FaTitle,
                request.EnTitle,
                request.Description);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> ChangeContractAdjustmentReferenceStateExecute(
        ChangeContractAdjustmentReferenceStateRequest request,
        CT ct)
    {
        try
        {
            var entity = await _contractAdjustmentReferenceRepository.GetContractAdjustmentReference(request.Id, ct);

            if (entity is null)
                return Result.Failure<bool>(ContractErrors.ReferenceNotFound);

            if (request.IsActive)
                entity.Activate();
            else
                entity.Deactivate();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<ContractAdjustmentIndex>> CreateContractAdjustmentIndexExecute(
        CreateContractAdjustmentIndexRequest request,
        CT ct)
    {
        try
        {
            var reference = await _contractAdjustmentReferenceRepository.GetContractAdjustmentReference(request.ReferenceId, ct);

            if (reference is null)
            {
                return Result.Failure<ContractAdjustmentIndex>(
                    ContractErrors.ReferenceNotFound)!;
            }

            if (await _contractAdjustmentIndexRepository.IsContractAdjustmentIndexCodeDuplicate(
                    request.ReferenceId,
                    request.Code,
                    null,
                    ct))
            {
                return Result.Failure<ContractAdjustmentIndex>(
                    ContractErrors.ContractAdjustmentIndexCodeAlreadyExists)!;
            }

            return reference.AddIndex(
                request.Code,
                request.FaTitle,
                request.EnTitle,
                request.Description);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractAdjustmentIndex>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractAdjustmentIndexExecute(
        UpdateContractAdjustmentIndexRequest request,
        CT ct)
    {
        try
        {
            var reference = await _contractAdjustmentReferenceRepository.GetContractAdjustmentReferenceWithIndex(
                request.ReferenceId,
                request.Id,
                ct);

            if (reference is null)
                return Result.Failure<bool>(ContractErrors.ReferenceNotFound);

            if (reference.Indexes.Count == 0)
                return Result.Failure<bool>(ContractErrors.IndexNotFound);

            if (await _contractAdjustmentIndexRepository.IsContractAdjustmentIndexCodeDuplicate(
                    request.ReferenceId,
                    request.Code,
                    request.Id,
                    ct))
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractAdjustmentIndexCodeAlreadyExists);
            }

            reference.UpdateIndex(
                request.Id,
                request.Code,
                request.FaTitle,
                request.EnTitle,
                request.Description);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> ChangeContractAdjustmentIndexStateExecute(
        ChangeContractAdjustmentIndexStateRequest request,
        CT ct)
    {
        try
        {
            var reference = await _contractAdjustmentReferenceRepository.GetContractAdjustmentReferenceWithIndex(
                request.ReferenceId,
                request.Id,
                ct);

            if (reference is null)
                return Result.Failure<bool>(ContractErrors.ReferenceNotFound);

            if (reference.Indexes.Count == 0)
                return Result.Failure<bool>(ContractErrors.IndexNotFound);

            if (request.IsActive)
                reference.ActivateIndex(request.Id);
            else
                reference.DeactivateIndex(request.Id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractAdjustmentReferenceByIdResponse?>> GetContractAdjustmentReferenceByIdExecute(
        GetContractAdjustmentReferenceByIdRequest request,
        CT ct)
    {
        try
        {
            var response = await _contractAdjustmentReferenceRepository.GetContractAdjustmentReferenceById(request.Id, ct);

            return response is null
                ? Result.Failure<GetContractAdjustmentReferenceByIdResponse>(
                    ContractErrors.ReferenceNotFound)
                : response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractAdjustmentReferenceByIdResponse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractAdjustmentReferencesResponse?>> GetContractAdjustmentReferencesExecute(
        GetContractAdjustmentReferencesRequest request,
        CT ct)
    {
        try
        {
            var (data, rowCount) = await _contractAdjustmentReferenceRepository.GetContractAdjustmentReferences(
                request.IsActive,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetContractAdjustmentReferencesResponse(data, rowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractAdjustmentReferencesResponse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractAdjustmentIndexByIdResponse?>> GetContractAdjustmentIndexByIdExecute(
        GetContractAdjustmentIndexByIdRequest request,
        CT ct)
    {
        try
        {
            if (!await _contractAdjustmentReferenceRepository.ContractAdjustmentReferenceExists(request.ReferenceId, ct))
            {
                return Result.Failure<GetContractAdjustmentIndexByIdResponse>(
                    ContractErrors.ReferenceNotFound);
            }

            var response = await _contractAdjustmentIndexRepository.GetContractAdjustmentIndexById(
                request.ReferenceId,
                request.Id,
                ct);

            return response is null
                ? Result.Failure<GetContractAdjustmentIndexByIdResponse>(
                    ContractErrors.IndexNotFound)
                : response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractAdjustmentIndexByIdResponse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractAdjustmentIndexesResponse?>> GetContractAdjustmentIndexesExecute(
        GetContractAdjustmentIndexesRequest request,
        CT ct)
    {
        try
        {
            if (!await _contractAdjustmentReferenceRepository.ContractAdjustmentReferenceExists(request.ReferenceId, ct))
            {
                return Result.Failure<GetContractAdjustmentIndexesResponse>(
                    ContractErrors.ReferenceNotFound);
            }

            var (data, rowCount) = await _contractAdjustmentIndexRepository.GetContractAdjustmentIndexes(
                request.ReferenceId,
                request.IsActive,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetContractAdjustmentIndexesResponse(data, rowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractAdjustmentIndexesResponse>(SharedErrors.UnknownError);
        }
    }

    #endregion

    #region ContractAdjustmentConfiguration Commands and Queries

    private async Task<Result<ContractAdjustmentConfiguration>> CreateContractAdjustmentConfigurationExecute(
        CreateContractAdjustmentConfigurationRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure<ContractAdjustmentConfiguration>(ContractErrors.ContractNotFound)!;

            var baselineResult = contract.EnsureBaselineIsEditable();
            if (baselineResult.IsFailure)
                return baselineResult.Failure<ContractAdjustmentConfiguration>()!;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, companyId, ct))
                return Result.Failure<ContractAdjustmentConfiguration>(

                    ContractErrors.ContractBaselineCannotChangeAfterContractChange)!;
            var dependencyError = await ValidateAdjustmentDependencies(request.Adjustment, ct);
            if (dependencyError is not null)
                return Result.Failure<ContractAdjustmentConfiguration>(dependencyError)!;

            return contract.AddAdjustmentConfiguration(
                request.Method, GetAdjustmentTerms(request.Adjustment)!, request.Scope.WholeContract,
                request.Scope.ContractTypeKinds ?? [], request.Scope.ContractTypeDetailIds ?? []);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractAdjustmentConfiguration>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result> UpdateContractAdjustmentConfigurationExecute(
        UpdateContractAdjustmentConfigurationRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure(ContractErrors.ContractNotFound);

            var baselineResult = contract.EnsureBaselineIsEditable();
            if (baselineResult.IsFailure)
                return baselineResult;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, companyId, ct))
                return Result.Failure(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            var dependencyError = await ValidateAdjustmentDependencies(request.Adjustment, ct);
            if (dependencyError is not null)
                return Result.Failure(dependencyError);

            return contract.UpdateAdjustmentConfiguration(
                request.Id, GetAdjustmentTerms(request.Adjustment)!, request.Scope.WholeContract,
                request.Scope.ContractTypeKinds ?? [], request.Scope.ContractTypeDetailIds ?? []);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure(SharedErrors.UnknownError);
        }
    }

    private async Task<Result> DeleteContractAdjustmentConfigurationExecute(
        DeleteContractAdjustmentConfigurationRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithTypesDetailsAndFinancialInformation(
                request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure(ContractErrors.ContractNotFound);

            var baselineResult = contract.EnsureBaselineIsEditable();
            if (baselineResult.IsFailure)
                return baselineResult;

            if (await _contractChangeRepository.HasActiveContractChanges(request.ContractId, companyId, ct))
                return Result.Failure(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            return contract.RemoveAdjustmentConfiguration(request.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetContractAdjustmentConfigurationResponse?>> GetContractAdjustmentConfigurationExecute(
        GetContractAdjustmentConfigurationRequest request, long companyId, CT ct)
    {
        try
        {
            var response = await _contractAdjustmentConfigurationRepository.GetContractAdjustmentConfiguration(
                request.ContractId, companyId, ct);
            return response is null
                ? Result.Failure<GetContractAdjustmentConfigurationResponse>(
                    ContractErrors.ContractAdjustmentConfigurationNotFound)
                : response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractAdjustmentConfigurationResponse>(SharedErrors.UnknownError);
        }
    }

    #endregion

    #region Contract Commands

    private async Task<Result<ContractEntity>> CreateContractRegistrationExecute(
        CreateContractRegistrationRequest request, long companyId, CT ct)
    {
        try
        {
            if (!ContractRegistrationTypeCode.TryParse(request.ContractTypeCode, out var kinds))
                return Result.Failure<ContractEntity>(ContractErrors.ContractRegistrationTypeInvalid)!;

            if (request.PricingMethod == PricingMethod.CostPlus)
                return Result.Failure<ContractEntity>(ContractErrors.ContractPricingMethodCostPlusDisabled)!;

            if (await _contractRepository.IsContractNumberDuplicate(request.ContractNumber, null, ct))
                return Result.Failure<ContractEntity>(ContractErrors.ContractNumberAlreadyExists)!;

            if (request.Status != ContractStatus.Draft)
                return Result.Failure<ContractEntity>(

                    ContractErrors.ContractRegistrationStatusMustBeDraft)!;

            var project = await _mediator.Send(new GetProjectByIdNoIncludingQuery(request.ProjectId), ct);
            if (project.IsBad())
                return project.Failure<ContractEntity>()!;

            if (project.Value!.CompanyId != companyId)
                return Result.Failure<ContractEntity>(ContractErrors.ProjectDoesNotBelongToCompany)!;

            var party = await _mediator.Send(new GetThirdPartyByIdQuery(request.ContractPartyId), ct);
            if (party.IsBad() || party.Value is null)
                return Result.Failure<ContractEntity>(ContractErrors.ContractPartyNotFound)!;

            if (await _currencyRepository.GetById(request.Financial.CurrencyId, ct) is null)
                return Result.Failure<ContractEntity>(ContractErrors.ContractFinancialCurrencyNotFound)!;

            var contract = new ContractEntity(companyId, request.ProjectId, request.ContractPartyId,
                request.FaTitle, request.EnTitle, request.Description, request.StartDate,
                request.Duration, request.DurationUnit);
            contract.AssignRegisteredContractNumber(request.ContractNumber);
            var structure = contract.SynchronizeRegistrationStructure(kinds, request.PricingMethod);
            if (structure.IsFailure)
                return structure.Failure<ContractEntity>()!;

            if (request.EndDate.HasValue && request.EndDate.Value != contract.EndDate)
                return Result.Failure<ContractEntity>(ContractErrors.ContractRegistrationEndDateMismatch)!;

            if (_contractRepository.RequiresContractCeilingAmount(contract) && !request.Financial.ContractCeilingAmount.HasValue)
                return Result.Failure<ContractEntity>(ContractErrors.ContractFinancialCeilingAmountIsRequired)!;

            var amountError = ValidateCalculatedFinancialAmounts(request.Financial.InitialAmount,
                request.Financial.InitialAmount, request.Financial.HasPrepayment,
                request.Financial.ContractCeilingAmount);
            if (amountError is not null)
                return Result.Failure<ContractEntity>(amountError)!;

            var financial = AddOrUpdateRegisteredFinancial(contract, request.Financial);
            if (financial.IsFailure)
                return financial.Failure<ContractEntity>()!;

            if (request.Urls.HasAny()) contract.AddDocuments(request.Urls!);
            var status = contract.PrepareImportedRegistration();
            if (status.IsFailure)
                return status.Failure<ContractEntity>()!;

            await _contractRepository.EnsureContractNumberSequenceIsAfter(request.ContractNumber, ct);
            await _contractRepository.CreateContract(contract, ct);
            return contract;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractEntity>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<bool>> UpdateContractRegistrationExecute(
        UpdateContractRegistrationRequest request, long companyId, CT ct)
    {
        try
        {
            if (!ContractRegistrationTypeCode.TryParse(request.ContractTypeCode, out var kinds))
                return Result.Failure<bool>(ContractErrors.ContractRegistrationTypeInvalid);

            if (request.PricingMethod == PricingMethod.CostPlus)
                return Result.Failure<bool>(ContractErrors.ContractPricingMethodCostPlusDisabled);

            if (request.Status != ContractStatus.Draft)
                return Result.Failure<bool>(ContractErrors.ContractRegistrationStatusMustBeDraft);

            var contract = await _contractRepository.GetContractForRegistrationMutation(request.Id, companyId, ct);
            if (contract is null)
                return Result.Failure<bool>(ContractErrors.ContractNotFound);

            if (request.EndDate.HasValue && request.EndDate.Value !=
                CalculateRequestedEndDate(request.StartDate, request.Duration, request.DurationUnit))
                return Result.Failure<bool>(ContractErrors.ContractRegistrationEndDateMismatch);

            var registrationProjection =
                _contractRepository.GetContractRegistrationMutationProjection(
                    contract,
                    request);

            var structureChanged = registrationProjection.StructureChanged;
            var headerChanged = registrationProjection.HeaderChanged;
            var documentsChanged = registrationProjection.DocumentsChanged;
            var financialChanged = registrationProjection.FinancialChanged;
            var baselineChanged =
                headerChanged ||
                structureChanged ||
                documentsChanged ||
                financialChanged;

            if (baselineChanged)
            {
                var editable = contract.EnsureBaselineIsEditable();
                if (editable.IsFailure)
                    return editable.Failure<bool>()!;

                if (await _contractChangeRepository.HasActiveContractChanges(request.Id, companyId, ct))
                    return Result.Failure<bool>(ContractErrors.ContractBaselineCannotChangeAfterContractChange);

            }

            var contractNumberChanged = contract.ContractNumber != request.ContractNumber;
            if (contractNumberChanged &&
                await _contractRepository.IsContractNumberDuplicate(request.ContractNumber, request.Id, ct))
                return Result.Failure<bool>(ContractErrors.ContractNumberAlreadyExists);

            if (contract.ProjectId != request.ProjectId)
            {
                var project = await _mediator.Send(new GetProjectByIdNoIncludingQuery(request.ProjectId), ct);
                if (project.IsBad())
                    return project.Failure<bool>()!;

                if (project.Value!.CompanyId != companyId)
                    return Result.Failure<bool>(ContractErrors.ProjectDoesNotBelongToCompany);

            }
            if (contract.ContractPartyId != request.ContractPartyId)
            {
                var party = await _mediator.Send(new GetThirdPartyByIdQuery(request.ContractPartyId), ct);
                if (party.IsBad() || party.Value is null)
                    return Result.Failure<bool>(ContractErrors.ContractPartyNotFound);

            }

            if (headerChanged)
            {
                if (contract.ContractNumber != request.ContractNumber) contract.AssignRegisteredContractNumber(request.ContractNumber);
                if (contract.FaTitle != request.FaTitle || contract.EnTitle != NormalizeOptional(request.EnTitle) || contract.Description != request.Description)
                    contract.UpdateReferenceInfo(request.FaTitle, request.EnTitle, request.Description);
                if (contract.ProjectId != request.ProjectId) contract.ChangeProject(request.ProjectId);
                if (contract.ContractPartyId != request.ContractPartyId) contract.ChangeContractParty(request.ContractPartyId);
                if (contract.StartDate != request.StartDate || contract.Duration != request.Duration || contract.DurationUnit != request.DurationUnit)
                    contract.UpdateSchedule(request.StartDate, request.Duration, request.DurationUnit);
            }

            if (structureChanged)
            {
                var structure = contract.SynchronizeRegistrationStructure(kinds, request.PricingMethod);
                if (structure.IsFailure)
                    return structure.Failure<bool>()!;

            }
            if (registrationProjection.RequiresContractCeilingAmount &&
                !request.Financial.ContractCeilingAmount.HasValue)
            {
                return Result.Failure<bool>(
                    ContractErrors.ContractFinancialCeilingAmountIsRequired);
            }

            if (financialChanged)
            {
                if (await _currencyRepository.GetById(request.Financial.CurrencyId, ct) is null)
                    return Result.Failure<bool>(ContractErrors.ContractFinancialCurrencyNotFound);

                var amountError = ValidateCalculatedFinancialAmounts(
                    request.Financial.InitialAmount,
                    request.Financial.InitialAmount + registrationProjection.ActiveFinancialChangeAmount,
                    request.Financial.HasPrepayment,
                    request.Financial.ContractCeilingAmount);
                if (amountError is not null)
                    return Result.Failure<bool>(amountError);

                var financial = AddOrUpdateRegisteredFinancial(contract, request.Financial);
                if (financial.IsFailure)
                    return financial.Failure<bool>()!;

            }
            if (documentsChanged) contract.ReplaceDocuments(request.Urls!);

            if (contractNumberChanged)
                await _contractRepository.EnsureContractNumberSequenceIsAfter(request.ContractNumber, ct);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<FinalizeContractRegistrationResponse?>> FinalizeContractRegistrationExecute(
        FinalizeContractRegistrationRequest request, long companyId, CT ct)
    {
        var contract = await _contractRepository.GetContractForRegistrationMutation(request.Id, companyId, ct);
        if (contract is null)
            return Result.Failure<FinalizeContractRegistrationResponse>(ContractErrors.ContractNotFound);

        var result = contract.FinalizeImportedRegistration();
        if (result.IsFailure)
            return result.Failure<FinalizeContractRegistrationResponse>()!;

        return new FinalizeContractRegistrationResponse(contract.Status, true);
    }

    private async Task<Result<GetContractRegistrationByIdResponse?>> GetContractRegistrationByIdExecute(
        GetContractRegistrationByIdRequest request, long companyId, CT ct)
    {
        var response = await _contractRepository.GetContractRegistrationById(request.Id, companyId, ct);
        if (response is null)
            return Result.Failure<GetContractRegistrationByIdResponse>(ContractErrors.ContractNotFound);

        if (!response.IsRegistrationStructureCompatible)
            return Result.Failure<GetContractRegistrationByIdResponse>(

                ContractErrors.ContractStructureIsNotCompatibleWithRegistrationView);
        var party = await _mediator.Send(new GetThirdPartyByIdQuery(response.ContractPartyId), ct);
        if (party.IsBad() || party.Value is null)
            return Result.Failure<GetContractRegistrationByIdResponse>(ContractErrors.ContractPartyNotFound);

        response.ContractPartyName = party.Value.FullName;
        return response;
    }

    private async Task<Result<GetContractRegistrationGridResponse?>> GetContractRegistrationGridExecute(
        GetContractRegistrationGridRequest request, long companyId, CT ct)
    {
        var (data, count) = await _contractRepository.GetContractRegistrationGrid(
            request.ContractNumber, request.ContractNumberFrom, request.ContractNumberTo,
            request.Status, request.FilterData, companyId, request.OrderBy,
            request.PageIndex, request.PageSize, ct);
        if (!data.HasAny())
            return new GetContractRegistrationGridResponse(data, count);

        await EnrichContractRegistrationGridPartyNamesAsync(
            data,
            ct);

        return new GetContractRegistrationGridResponse(data, count);

    }

    private static Result AddOrUpdateRegisteredFinancial(
        ContractEntity contract, ContractRegistrationFinancialRequest request)
    {
        if (contract.FinancialInformation is null || contract.FinancialInformation.IsDeleted)
        {
            var added = contract.AddRegisteredFinancialInformation(
                request.InitialAmount, request.CurrencyId, request.HasPrepayment,
                request.IsSubjectToAdjustment, request.ContractCeilingAmount,
                request.AdjustmentLimitValue, request.AdjustmentLimitType,
                request.PrepaymentPercentage, request.PrepaymentAmortizationMethod,
                request.PrepaymentAmortizationValue, request.PrepaymentStartStatusStatementNumber,
                request.PrepaymentStartProgressPercentage);
            return added.IsBad() ? Result.Failure(added.Error!) : Result.Success();
        }

        if (!contract.FinancialInformation.RegisteredInitialAmount.HasValue &&
            contract.CalculateInitialAmount() == ContractFinancialMath.NormalizeMoney(request.InitialAmount))
        {
            return contract.UpdateFinancialInformation(
                contract.CalculateInitialAmount(), contract.CalculateFinalContractAmount(),
                request.CurrencyId, request.HasPrepayment, request.IsSubjectToAdjustment,
                request.ContractCeilingAmount, request.AdjustmentLimitValue,
                request.AdjustmentLimitType, request.PrepaymentPercentage,
                request.PrepaymentAmortizationMethod, request.PrepaymentAmortizationValue,
                request.PrepaymentStartStatusStatementNumber,
                request.PrepaymentStartProgressPercentage);
        }

        return contract.UpdateRegisteredFinancialInformation(
            request.InitialAmount, request.CurrencyId, request.HasPrepayment,
            request.IsSubjectToAdjustment, request.ContractCeilingAmount,
            request.AdjustmentLimitValue, request.AdjustmentLimitType,
            request.PrepaymentPercentage, request.PrepaymentAmortizationMethod,
            request.PrepaymentAmortizationValue, request.PrepaymentStartStatusStatementNumber,
            request.PrepaymentStartProgressPercentage);
    }

    #endregion

    #region ContractChange Commands

    private async Task<Result<ContractSummaryChangeCreateExecutionResult>> CreateContractSummaryChangeExecute(
        CreateContractSummaryChangeRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithChangesForMutation(request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure<ContractSummaryChangeCreateExecutionResult>(ContractErrors.ContractNotFound)!;

            var lifecycle = contract.EnsureSummaryContractChangeCanBeCreated();
            if (lifecycle.IsFailure)
                return lifecycle.Failure<ContractSummaryChangeCreateExecutionResult>()!;

            if (await _contractChangeRepository.IsContractChangeNumberDuplicate(request.ContractId, request.Number, null, companyId, ct))
                return Result.Failure<ContractSummaryChangeCreateExecutionResult>(ContractErrors.ContractChangeNumberAlreadyExists)!;

            var context = await _contractChangeRepository.GetContractChangeMutationContext(request.ContractId, null, companyId, ct);
            if (request.Date < contract.StartDate || context.LatestChangeDate.HasValue && request.Date < context.LatestChangeDate.Value)
                return Result.Failure<ContractSummaryChangeCreateExecutionResult>(ContractErrors.ContractChangeDateInvalid)!;

            var previousAmount = contract.CalculateCurrentLegalAmount();
            var finalAmount = ContractFinancialMath.NormalizeMoney(previousAmount + request.FinancialChangeAmount);
            var totalDurationChange = contract.CalculateCurrentDuration() - contract.Duration + (request.DurationChange ?? 0);
            var finalDuration = contract.Duration + totalDurationChange;
            var finalEndDate = contract.CalculateEndDateWithDurationChange(totalDurationChange);
            var stateError = ValidateProposedContractChangeState(contract, finalAmount, finalDuration);
            if (stateError is not null)
                return Result.Failure<ContractSummaryChangeCreateExecutionResult>(stateError)!;

            if (request.NewContractAmount.HasValue &&
                ContractFinancialMath.NormalizeMoney(request.NewContractAmount.Value) != finalAmount)
                return Result.Failure<ContractSummaryChangeCreateExecutionResult>(ContractErrors.ContractRegistrationNewAmountMismatch)!;
            if (request.NewEndDate.HasValue && request.NewEndDate.Value != finalEndDate)
                return Result.Failure<ContractSummaryChangeCreateExecutionResult>(ContractErrors.ContractRegistrationNewEndDateMismatch)!;

            var result = contract.AddSummaryContractChange(new ContractSummaryChangeTerms(
                request.Type, request.Number, request.Date, request.Subject, request.DurationChange,
                previousAmount, ContractFinancialMath.NormalizeMoney(request.FinancialChangeAmount),
                finalAmount, request.Urls ?? []));
            if (result.IsBad())
                return result.Failure<ContractSummaryChangeCreateExecutionResult>()!;

            return new ContractSummaryChangeCreateExecutionResult(
                result.Value!, finalAmount, finalEndDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractSummaryChangeCreateExecutionResult>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<UpdateContractSummaryChangeResponse?>> UpdateContractSummaryChangeExecute(
        UpdateContractSummaryChangeRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithChangesForMutation(request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractNotFound);

            var context = await _contractChangeRepository.GetContractChangeMutationContext(request.ContractId, request.Id, companyId, ct);
            if (!context.TargetExists)
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractChangeNotFound);

            if (context.LatestChangeId != request.Id)
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractChangeOnlyLatestCanBeModified);

            if (context.TargetMode != ContractChangeMode.Summary)
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractChangeModeInvalid);

            if (await _contractChangeRepository.IsContractChangeNumberDuplicate(request.ContractId, request.Number, request.Id, companyId, ct))
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractChangeNumberAlreadyExists);

            if (request.Date < contract.StartDate || context.PreviousChangeDate.HasValue && request.Date < context.PreviousChangeDate.Value)
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractChangeDateInvalid);

            var previousAmount = ContractFinancialMath.NormalizeMoney(
                contract.CalculateCurrentLegalAmount() - context.TargetFinancialChangeAmount);
            var finalAmount = ContractFinancialMath.NormalizeMoney(previousAmount + request.FinancialChangeAmount);
            var totalDurationChange = contract.CalculateCurrentDuration() - contract.Duration -
                                      (context.TargetDurationChange ?? 0) + (request.DurationChange ?? 0);
            var finalDuration = contract.Duration + totalDurationChange;
            var finalEndDate = contract.CalculateEndDateWithDurationChange(totalDurationChange);
            var stateError = ValidateProposedContractChangeState(contract, finalAmount, finalDuration);
            if (stateError is not null)
                return Result.Failure<UpdateContractSummaryChangeResponse>(stateError);

            if (request.NewContractAmount.HasValue &&
                ContractFinancialMath.NormalizeMoney(request.NewContractAmount.Value) != finalAmount)
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractRegistrationNewAmountMismatch);
            if (request.NewEndDate.HasValue && request.NewEndDate.Value != finalEndDate)
                return Result.Failure<UpdateContractSummaryChangeResponse>(ContractErrors.ContractRegistrationNewEndDateMismatch);

            var result = contract.UpdateSummaryContractChange(request.Id, new ContractSummaryChangeTerms(
                request.Type, request.Number, request.Date, request.Subject, request.DurationChange,
                previousAmount, ContractFinancialMath.NormalizeMoney(request.FinancialChangeAmount),
                finalAmount, request.Urls ?? []));
            if (result.IsFailure)
                return result.Failure<UpdateContractSummaryChangeResponse>()!;

            return new UpdateContractSummaryChangeResponse(finalAmount, finalEndDate, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateContractSummaryChangeResponse>(SharedErrors.UnknownError);
        }
    }

    #endregion

    #region ContractGuarantee Commands

    private async Task<Result<ContractGuarantee>> CreateContractGuaranteeExecute(
        CreateContractGuaranteeRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithGuarantees(request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure<ContractGuarantee>(ContractErrors.ContractNotFound)!;

            var guarantee = contract.AddGuarantee(request.Type, request.Amount, request.Percentage,
                request.Number, request.IssueDate, request.ExpiryDate, request.FileUrl);
            return guarantee;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractGuarantee>(ContractErrors.ContractGuaranteePercentageInvalid)!;
        }
        catch (ArgumentException ex) when (ex.ParamName == "expiryDate")
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractGuarantee>(ContractErrors.ContractGuaranteeDatesInvalid)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractGuarantee>(SharedErrors.UnknownError)!;
        }
    }

    private async Task<Result<UpdateContractGuaranteeResponse?>> UpdateContractGuaranteeExecute(
        UpdateContractGuaranteeRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithGuarantees(request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure<UpdateContractGuaranteeResponse>(ContractErrors.ContractNotFound);

            if (!_contractGuaranteeRepository.HasActiveGuarantee(contract, request.Id))
                return Result.Failure<UpdateContractGuaranteeResponse>(ContractErrors.ContractGuaranteeNotFound);

            contract.UpdateGuarantee(request.Id, request.Type, request.Amount, request.Percentage,
                request.Number, request.IssueDate, request.ExpiryDate, request.FileUrl);
            return new UpdateContractGuaranteeResponse(true);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateContractGuaranteeResponse>(ContractErrors.ContractGuaranteePercentageInvalid);
        }
        catch (ArgumentException ex) when (ex.ParamName == "expiryDate")
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateContractGuaranteeResponse>(ContractErrors.ContractGuaranteeDatesInvalid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateContractGuaranteeResponse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<ChangeContractGuaranteeStatusResponse?>> ChangeContractGuaranteeStatusExecute(
        ChangeContractGuaranteeStatusRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithGuarantees(request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure<ChangeContractGuaranteeStatusResponse>(ContractErrors.ContractNotFound);

            if (!_contractGuaranteeRepository.HasActiveGuarantee(contract, request.Id))
                return Result.Failure<ChangeContractGuaranteeStatusResponse>(ContractErrors.ContractGuaranteeNotFound);

            contract.ChangeGuaranteeStatus(request.Id, request.Status);
            return new ChangeContractGuaranteeStatusResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ChangeContractGuaranteeStatusResponse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<DeleteContractGuaranteeResponse?>> DeleteContractGuaranteeExecute(
        DeleteContractGuaranteeRequest request, long companyId, CT ct)
    {
        try
        {
            var contract = await _contractRepository.GetContractWithGuarantees(request.ContractId, companyId, ct);
            if (contract is null)
                return Result.Failure<DeleteContractGuaranteeResponse>(ContractErrors.ContractNotFound);

            if (!_contractGuaranteeRepository.HasActiveGuarantee(contract, request.Id))
                return Result.Failure<DeleteContractGuaranteeResponse>(ContractErrors.ContractGuaranteeNotFound);

            contract.RemoveGuarantee(request.Id);
            return new DeleteContractGuaranteeResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteContractGuaranteeResponse>(SharedErrors.UnknownError);
        }
    }

    #endregion

}
