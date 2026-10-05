using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Errors;
using Gita.Backend.Shared.Domain.Base.Results;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractTypeDetail)]
public class ContractTypeDetail : AuditableEntity<ContractTypeDetail, long>
{
    [Description(ContractCmts.ContractTypeId)]
    public long ContractTypeId { get; private set; }

    public ContractType ContractType { get; private set; } = null!;

    [Description(ContractCmts.ConsumableVolumeProductId)]
    public long? ConsumableVolumeProductId { get; private set; }

    [Description(GlobalCmts.ProjectOperationDetailId)]
    public long? ProjectOperationDetailId { get; private set; }

    [Description(ContractCmts.ProjectOperationDetailContractorServiceId)]
    public long? ProjectOperationDetailContractorServiceId { get; private set; }

    [Description(ContractCmts.Quantity)]
    public decimal Quantity { get; private set; }

    [Description(ContractCmts.UnitOfMeasurementId)]
    public long? UnitOfMeasurementId { get; private set; }

    [Description(ContractCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(ContractCmts.FixedAmount)]
    public decimal? FixedAmount { get; private set; }

    [Description(ContractCmts.TechnicalSpecifications)]
    public string? TechnicalSpecifications { get; private set; }

    [Description(ContractCmts.ExpectedDeliverables)]
    public string? ExpectedDeliverables { get; private set; }

    [Description(ContractCmts.DetailDuration)]
    public decimal? Duration { get; private set; }

    [Description(ContractCmts.DurationUnit)]
    public ContractDurationUnit? DurationUnit { get; private set; }

    [Description(ContractCmts.IsSubjectToAdjustment)]
    public bool IsSubjectToAdjustment { get; private set; }

    [Description(ContractCmts.ContractTypeDetailAdjustment)]
    public ContractTypeDetailAdjustment? Adjustment { get; private set; }

    #region Constructors

    private ContractTypeDetail()
    {
    }

    public ContractTypeDetail(
        ContractType contractType,
        long sourceId,
        decimal quantity,
        long? unitOfMeasurementId,
        decimal? unitPrice,
        decimal? fixedAmount,
        string? technicalSpecifications,
        string? expectedDeliverables,
        decimal? duration,
        ContractDurationUnit? durationUnit,
        bool isSubjectToAdjustment)
    {
        SetContractType(contractType);
        SetSource(contractType.Kind, sourceId);
        SetQuantity(quantity);
        SetUnitOfMeasurementId(unitOfMeasurementId);
        SetUnitPrice(unitPrice);
        SetFixedAmount(fixedAmount);
        SetIsSubjectToAdjustment(isSubjectToAdjustment);

        SetTypeSpecificTerms(
            contractType.Kind,
            technicalSpecifications,
            expectedDeliverables,
            duration,
            durationUnit);

        ValidateCommercialTerms(contractType.Kind, contractType.PricingMethod);
    }

    #endregion

    #region Commands

    public void Update(
        decimal quantity,
        long? unitOfMeasurementId,
        decimal? unitPrice,
        decimal? fixedAmount,
        string? technicalSpecifications,
        string? expectedDeliverables,
        decimal? duration,
        ContractDurationUnit? durationUnit)
    {
        SetQuantity(quantity);
        SetUnitOfMeasurementId(unitOfMeasurementId);
        SetUnitPrice(unitPrice);
        SetFixedAmount(fixedAmount);

        SetTypeSpecificTerms(
            ContractType.Kind,
            technicalSpecifications,
            expectedDeliverables,
            duration,
            durationUnit);

        ValidateCommercialTerms(
            ContractType.Kind,
            ContractType.PricingMethod);
    }

    public void ConfigureAdjustment(ContractTypeDetailAdjustmentTerms terms)
    {
        if (!IsSubjectToAdjustment)
            throw new InvalidOperationException(
                "Adjustment cannot be configured for an ineligible ContractTypeDetail.");

        if (Adjustment is null || Adjustment.IsDeleted)
        {
            Adjustment = new ContractTypeDetailAdjustment(this, terms);
            return;
        }

        Adjustment.Update(terms);
    }

    public void RemoveAdjustment()
    {
        if (Adjustment is not null && !Adjustment.IsDeleted)
            Adjustment.SoftDelete();
    }

    public Result ChangeAdjustmentEligibility(bool isSubjectToAdjustment)
    {
        if (!isSubjectToAdjustment &&
            Adjustment is { IsDeleted: false })
        {
            return Result.Failure(
                ContractErrors.ContractTypeDetailAdjustmentEligibilityInvalid);
        }

        SetIsSubjectToAdjustment(isSubjectToAdjustment);
        return Result.Success();
    }

    public void Delete()
    {
        RemoveAdjustment();
        SoftDelete();
    }

    public decimal CalculateAmount()
    {
        return ContractFinancialMath.CalculateContractTypeDetailAmount(
            ContractType.PricingMethod,
            Quantity,
            UnitPrice,
            FixedAmount,
            Duration)!.Value;
    }

    #endregion

    #region Private Helpers

    private void SetContractType(ContractType value)
    {
        ContractType = Guard.Against.Null(value, nameof(value));
        ContractTypeId = value.Id;
    }

    private void SetSource(ContractTypeKind kind, long sourceId)
    {
        sourceId = Guard.Against.NegativeOrZero(sourceId, nameof(sourceId));

        switch (kind)
        {
            case ContractTypeKind.Procurement:
                ConsumableVolumeProductId = sourceId;
                break;

            case ContractTypeKind.Construction:
                ProjectOperationDetailId = sourceId;
                break;

            case ContractTypeKind.Engineering:
            case ContractTypeKind.Services:
                ProjectOperationDetailContractorServiceId = sourceId;
                break;

            default:
                throw new InvalidOperationException(
                    $"ContractTypeDetail source is not supported for ContractType Kind {kind}.");
        }
    }

    private void SetQuantity(decimal value)
        => Quantity = Guard.Against.NegativeOrZero(
            ContractFinancialMath.NormalizeQuantity(value),
            nameof(value));

    private void SetUnitOfMeasurementId(long? value)
        => UnitOfMeasurementId = value.HasValue
            ? Guard.Against.NegativeOrZero(value.Value, nameof(value))
            : null;

    private void SetUnitPrice(decimal? value)
        => UnitPrice = value.HasValue
            ? Guard.Against.NegativeOrZero(
                ContractFinancialMath.NormalizeMoney(value.Value),
                nameof(value))
            : null;

    private void SetIsSubjectToAdjustment(bool value)
        => IsSubjectToAdjustment = value;

    private void SetFixedAmount(decimal? value)
        => FixedAmount = value.HasValue
            ? Guard.Against.NegativeOrZero(
                ContractFinancialMath.NormalizeMoney(value.Value),
                nameof(value))
            : null;

    private void SetTypeSpecificTerms(
        ContractTypeKind kind,
        string? technicalSpecifications,
        string? expectedDeliverables,
        decimal? duration,
        ContractDurationUnit? durationUnit)
    {
        if (duration.HasValue != durationUnit.HasValue)
            throw new InvalidOperationException(
                "Duration and DurationUnit must be supplied together.");

        if (kind == ContractTypeKind.Procurement)
        {
            if (string.IsNullOrWhiteSpace(technicalSpecifications))
                throw new InvalidOperationException(
                    "TechnicalSpecifications is required for Procurement details.");

            if (!string.IsNullOrWhiteSpace(expectedDeliverables) ||
                duration.HasValue)
                throw new InvalidOperationException(
                    "ExpectedDeliverables and Duration are not valid for Procurement details.");

            TechnicalSpecifications = technicalSpecifications;
            ExpectedDeliverables = null;
            Duration = null;
            DurationUnit = null;

            return;
        }

        if (kind is ContractTypeKind.Engineering or ContractTypeKind.Services)
        {
            if (!string.IsNullOrWhiteSpace(technicalSpecifications))
                throw new InvalidOperationException(
                    "TechnicalSpecifications is not valid for Engineering or Services details.");

            if (string.IsNullOrWhiteSpace(expectedDeliverables))
                throw new InvalidOperationException(
                    "ExpectedDeliverables is required for Engineering and Services details.");

            TechnicalSpecifications = null;
            ExpectedDeliverables = expectedDeliverables;
            Duration = duration.HasValue
                ? Guard.Against.NegativeOrZero(
                    ContractFinancialMath.NormalizeDuration(duration.Value),
                    nameof(duration))
                : null;
            DurationUnit = durationUnit.HasValue
                ? Guard.Against.EnumOutOfRange(
                    durationUnit.Value,
                    nameof(durationUnit))
                : null;

            return;
        }

        if (!string.IsNullOrWhiteSpace(technicalSpecifications) ||
            !string.IsNullOrWhiteSpace(expectedDeliverables) ||
            duration.HasValue)
        {
            throw new InvalidOperationException(
                "Type-specific fields are not valid for Construction details.");
        }

        TechnicalSpecifications = null;
        ExpectedDeliverables = null;
        Duration = null;
        DurationUnit = null;
    }

    private void ValidateCommercialTerms(
        ContractTypeKind kind,
        PricingMethod pricingMethod)
    {
        if (pricingMethod is PricingMethod.UnitPrice or PricingMethod.CostPlus or PricingMethod.TimeAndMaterial &&
            !UnitPrice.HasValue)
        {
            throw new InvalidOperationException(
                "UnitPrice is required for UnitPrice, CostPlus, and TimeAndMaterial contract details.");
        }

        if (pricingMethod == PricingMethod.LumpSum)
        {
            if (!FixedAmount.HasValue)
                throw new InvalidOperationException(
                    "FixedAmount is required for LumpSum contract details.");
        }
        else if (FixedAmount.HasValue)
        {
            throw new InvalidOperationException(
                "FixedAmount is only valid for LumpSum contract details.");
        }

        if (kind is ContractTypeKind.Procurement or ContractTypeKind.Construction)
        {
            if (!UnitOfMeasurementId.HasValue)
                throw new InvalidOperationException(
                    "UnitOfMeasurementId is required for Procurement and Construction details.");

            return;
        }

        if (pricingMethod != PricingMethod.LumpSum && !UnitOfMeasurementId.HasValue)
        {
            throw new InvalidOperationException(
                "UnitOfMeasurementId is required for non-LumpSum Engineering and Services details.");
        }

    }

    #endregion
}
