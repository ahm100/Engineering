using Engineering.Application.Services.Contracts.Contracts.ContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Application.Services.Contracts.Models.ContractChanges;
using Engineering.Application.Services.Contracts.Models.ContractTypeDetails;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Gita.Backend.Shared.Domain.Base;

namespace Engineering.Application.Services.Contracts;

using Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

public partial class ContractLogic
{
    #region Contract Helpers

    private async Task<Result<long>> ResolveCompanyId(CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);

        if (companyId is null)
            return Result.Failure<long>(GlobalErrors.InvalidCompany);

        var isCompanyValid = await CompanyValidator.IsCompanyValid(companyId, _mediator, ct);

        if (!isCompanyValid)
            return Result.Failure<long>(GlobalErrors.InvalidCompany);

        return companyId.Value;
    }

    #endregion

    #region ContractChange Helpers

    private static ContractTypeKind NormalizeContractChangeCapacityKind(
        ContractTypeKind kind)
    {
        return kind == ContractTypeKind.Services
            ? ContractTypeKind.Engineering
            : kind;
    }

    private static Error? MapContractChangeCapacityValidationStatus(
        ContractChangeCapacityValidationStatus status)
    {
        return status switch
        {
            ContractChangeCapacityValidationStatus.Valid => null,
            ContractChangeCapacityValidationStatus.SourceMissing =>
                ContractErrors.ContractChangeItemInvalid,
            ContractChangeCapacityValidationStatus.ExceedsAvailableQuantity =>
                ContractErrors.ContractChangeWouldExceedSourceQuantity,
            _ => ContractErrors.ContractChangeItemInvalid
        };
    }

    #endregion

    #region ContractFinancialInformation Helpers


    private static Error? ValidateFinancialInformationForPricingMethods(
        ContractFinancialInformation? financialInformation,
        bool pricingRequiresCeiling)
    {
        if (financialInformation is null || financialInformation.IsDeleted)
            return null;

        if (!financialInformation.ContractCeilingAmount.HasValue &&
            pricingRequiresCeiling)
        {
            return ContractErrors.ContractFinancialCeilingAmountIsRequired;
        }

        return null;
    }

    private static Error? ValidateCalculatedFinancialAmounts(
        decimal calculatedInitialAmount,
        decimal calculatedCurrentContractAmount,
        bool hasPrepayment,
        decimal? contractCeilingAmount)
    {
        if (hasPrepayment && calculatedInitialAmount <= 0)
            return ContractErrors.ContractFinancialInitialAmountMustBePositiveForPrepayment;

        if (contractCeilingAmount.HasValue &&
            contractCeilingAmount.Value < calculatedCurrentContractAmount)
        {
            return ContractErrors.ContractFinancialCeilingAmountCannotBeLessThanCurrentContractAmount;
        }

        return null;
    }

    #endregion

    #region Contract Helpers

    private async Task EnrichContractReadModelsAsync<T>(
        List<T> models,
        Func<T, long> getPartyId,
        Action<T, string?> setPartyName,
        CT ct)
        where T : IUserAuditable
    {
        var thirdPartyIds = models.Listed(getPartyId);

        var thirdParties = thirdPartyIds.HasAny()
            ? await WebServicesLogic.ThirdPartiesDataReceiver(thirdPartyIds, _mediator, ct)
            : null;

        var thirdPartyDict = new Dictionary<long, string?>();

        if (thirdParties is not null)
        {
            foreach (var party in thirdParties)
                thirdPartyDict[party.Id] = party.FullName;
        }

        foreach (var item in models)
        {
            if (thirdPartyDict.TryGetValue(getPartyId(item), out var partyName))
                setPartyName(item, partyName);
        }

        await models.SetFullName(_mediator, ct);
    }

    #endregion

    #region Contract Registration Helpers

    private async Task EnrichContractRegistrationGridPartyNamesAsync(
        List<GetContractRegistrationGridModel> models,
        CT ct)
    {
        var thirdPartyIds = new List<long>();

        foreach (var model in models)
        {
            if (!thirdPartyIds.Contains(model.ContractPartyId))
                thirdPartyIds.Add(model.ContractPartyId);
        }

        if (thirdPartyIds.Count == 0)
            return;

        var thirdParties =
            await WebServicesLogic.ThirdPartiesDataReceiver(
                thirdPartyIds,
                _mediator,
                ct);

        if (thirdParties is null)
            return;

        var thirdPartyNames = new Dictionary<long, string?>();

        foreach (var party in thirdParties)
            thirdPartyNames[party.Id] = party.FullName;

        foreach (var model in models)
        {
            if (thirdPartyNames.TryGetValue(
                    model.ContractPartyId,
                    out var partyName))
            {
                model.ContractPartyName = partyName;
            }
        }
    }

    #endregion

    #region ContractTypeDetail Helpers

    private async Task<ContractTypeDetailSourceModel?> ResolveContractTypeDetailSource(
        ContractType contractType,
        long sourceId,
        long projectId,
        long contractPartyId,
        long? excludedContractTypeDetailId,
        long validCompanyId,
        CT ct)
    {
        return contractType.Kind switch
        {
            ContractTypeKind.Procurement => await _contractTypeDetailRepository.GetProcurementContractTypeDetailSource(
                sourceId,
                projectId,
                excludedContractTypeDetailId,
                validCompanyId,
                ct),

            ContractTypeKind.Construction => await _contractTypeDetailRepository.GetConstructionContractTypeDetailSource(
                sourceId,
                projectId,
                contractPartyId,
                excludedContractTypeDetailId,
                validCompanyId,
                ct),

            ContractTypeKind.Engineering or ContractTypeKind.Services =>
                await _contractTypeDetailRepository.GetServiceContractTypeDetailSource(
                    sourceId,
                    projectId,
                    excludedContractTypeDetailId,
                    validCompanyId,
                    ct),

            _ => null
        };
    }

    private bool HasActiveContractTypeDetailSource(
        ContractType contractType,
        long sourceId)
    {
        return _contractTypeDetailRepository.HasActiveContractTypeDetailSource(
            contractType,
            sourceId);
    }

    private static bool HasValidContractTypeDetailTerms(
        ContractType contractType,
        long? unitOfMeasurementId,
        decimal? unitPrice,
        decimal? fixedAmount,
        string? technicalSpecifications,
        string? expectedDeliverables,
        decimal? duration,
        ContractDurationUnit? durationUnit)
    {
        if (duration.HasValue != durationUnit.HasValue)
            return false;

        if (contractType.PricingMethod is PricingMethod.UnitPrice or PricingMethod.TimeAndMaterial &&
            !unitPrice.HasValue)
        {
            return false;
        }

        if (contractType.PricingMethod == PricingMethod.LumpSum)
        {
            if (!fixedAmount.HasValue)
                return false;

        }
        else if (fixedAmount.HasValue)
        {
            return false;
        }

        if (contractType.Kind == ContractTypeKind.Procurement)
        {
            return !string.IsNullOrWhiteSpace(technicalSpecifications) &&
                   string.IsNullOrWhiteSpace(expectedDeliverables) &&
                   !duration.HasValue &&
                   !durationUnit.HasValue;
        }

        if (contractType.Kind == ContractTypeKind.Construction)
        {
            return string.IsNullOrWhiteSpace(technicalSpecifications) &&
                   string.IsNullOrWhiteSpace(expectedDeliverables) &&
                   !duration.HasValue &&
                   !durationUnit.HasValue;
        }

        if (contractType.Kind is ContractTypeKind.Engineering or ContractTypeKind.Services)
        {
            if (!string.IsNullOrWhiteSpace(technicalSpecifications) ||
                string.IsNullOrWhiteSpace(expectedDeliverables))
            {
                return false;
            }

            if (contractType.PricingMethod != PricingMethod.LumpSum &&
                !unitOfMeasurementId.HasValue)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    private static long? GetContractTypeDetailSourceId(
        ContractType contractType,
        ContractTypeDetail detail)
    {
        return contractType.Kind switch
        {
            ContractTypeKind.Procurement => detail.ConsumableVolumeProductId,
            ContractTypeKind.Construction => detail.ProjectOperationDetailId,
            ContractTypeKind.Engineering or ContractTypeKind.Services =>
                detail.ProjectOperationDetailContractorServiceId,
            _ => null
        };
    }

    #endregion

    #region ContractAdjustmentConfiguration Helpers

    private static ContractTypeDetailAdjustmentTerms? GetAdjustmentTerms(
        ContractTypeDetailAdjustmentRequest? adjustment)
    {
        if (adjustment is null)
            return null;

        return adjustment.Type switch
        {
            ContractTypeDetailAdjustmentType.PriceIndex =>
                new ContractTypeDetailAdjustmentTerms(
                    adjustment.Type,
                    adjustment.PriceIndex!.BaseYear,
                    adjustment.PriceIndex.BasePeriod,
                    adjustment.PriceIndex.IndexId,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null),

            ContractTypeDetailAdjustmentType.Currency =>
                new ContractTypeDetailAdjustmentTerms(
                    adjustment.Type,
                    null,
                    null,
                    null,
                    adjustment.Currency!.BaseDate,
                    adjustment.Currency.BaseRate,
                    adjustment.Currency.CurrencyId,
                    adjustment.Currency.ReferenceType,
                    string.IsNullOrWhiteSpace(adjustment.Currency.CustomReference)
                        ? null
                        : adjustment.Currency.CustomReference,
                    null,
                    null,
                    null,
                    null),

            ContractTypeDetailAdjustmentType.Other =>
                new ContractTypeDetailAdjustmentTerms(
                    adjustment.Type,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    adjustment.Other!.Basis,
                    adjustment.Other.Reference,
                    adjustment.Other.Index,
                    adjustment.Other.Description),

            _ => throw new InvalidOperationException(
                $"ContractTypeDetail adjustment Type {adjustment.Type} is not supported.")
        };
    }
    #endregion

    #region Contract Helpers

    private static bool HasRegisteredFinancialChanges(
        Contract contract,
        ContractRegistrationFinancialRequest value)
    {
        var financial = contract.FinancialInformation;
        var effectiveInitial = contract.CalculateInitialAmount();
        return financial is null || financial.IsDeleted ||
            effectiveInitial != ContractFinancialMath.NormalizeMoney(value.InitialAmount) ||
            financial.CurrencyId != value.CurrencyId ||
            financial.HasPrepayment != value.HasPrepayment ||
            financial.IsSubjectToAdjustment != value.IsSubjectToAdjustment ||
            financial.ContractCeilingAmount != value.ContractCeilingAmount ||
            financial.AdjustmentLimitValue != value.AdjustmentLimitValue ||
            financial.AdjustmentLimitType != value.AdjustmentLimitType ||
            financial.PrepaymentPercentage != value.PrepaymentPercentage ||
            financial.PrepaymentAmortizationMethod != value.PrepaymentAmortizationMethod ||
            financial.PrepaymentAmortizationValue != value.PrepaymentAmortizationValue ||
            financial.PrepaymentStartStatusStatementNumber != value.PrepaymentStartStatusStatementNumber ||
            financial.PrepaymentStartProgressPercentage != value.PrepaymentStartProgressPercentage;
    }

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value;

        private static DateTime CalculateRequestedEndDate(
        DateTime startDate,
        int duration,
        ContractDurationUnit unit)
    {
        var calendar = new System.Globalization.PersianCalendar();
        return unit == ContractDurationUnit.Month
            ? calendar.AddMonths(startDate, duration)
            : startDate.AddDays(duration);
    }

    #endregion

}
