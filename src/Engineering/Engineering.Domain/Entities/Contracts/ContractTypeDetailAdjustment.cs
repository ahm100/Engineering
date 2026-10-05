using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractTypeDetailAdjustment)]
public class ContractTypeDetailAdjustment
    : AuditableEntity<ContractTypeDetailAdjustment, long>
{
    [Description(ContractCmts.ContractTypeDetail)]
    public long ContractTypeDetailId { get; private set; }

    public ContractTypeDetail ContractTypeDetail { get; private set; } = null!;

    [Description(ContractCmts.ContractTypeDetailAdjustmentType)]
    public ContractTypeDetailAdjustmentType Type { get; private set; }

    [Description(ContractCmts.PriceIndexBaseYear)]
    public int? PriceIndexBaseYear { get; private set; }

    [Description(ContractCmts.PriceIndexBasePeriod)]
    public ContractAdjustmentPeriod? PriceIndexBasePeriod { get; private set; }

    [Description(ContractCmts.ContractAdjustmentIndexId)]
    public long? PriceIndexId { get; private set; }

    public ContractAdjustmentIndex? PriceIndex { get; private set; }

    [Description(ContractCmts.CurrencyBaseDate)]
    public DateTime? CurrencyBaseDate { get; private set; }

    [Description(ContractCmts.CurrencyBaseRate)]
    public decimal? CurrencyBaseRate { get; private set; }

    [Description(GlobalCmts.CurrencyId)]
    public long? CurrencyId { get; private set; }

    [Description(ContractCmts.CurrencyReferenceType)]
    public ContractAdjustmentCurrencyReferenceType? CurrencyReferenceType { get; private set; }

    [Description(ContractCmts.CurrencyCustomReference)]
    public string? CurrencyCustomReference { get; private set; }

    [Description(ContractCmts.OtherBasis)]
    public string? OtherBasis { get; private set; }

    [Description(ContractCmts.OtherReference)]
    public string? OtherReference { get; private set; }

    [Description(ContractCmts.OtherIndex)]
    public string? OtherIndex { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    #region Constructors

    private ContractTypeDetailAdjustment()
    {
    }

    public ContractTypeDetailAdjustment(
        ContractTypeDetail contractTypeDetail,
        ContractTypeDetailAdjustmentTerms terms)
    {
        SetContractTypeDetail(contractTypeDetail);
        ApplyTerms(terms);
    }

    #endregion

    #region Commands

    public void Update(ContractTypeDetailAdjustmentTerms terms)
        => ApplyTerms(terms);

    #endregion

    #region Private Helpers

    private void SetContractTypeDetail(ContractTypeDetail value)
    {
        ContractTypeDetail = Guard.Against.Null(value, nameof(value));
        ContractTypeDetailId = value.Id;
    }

    private void ApplyTerms(ContractTypeDetailAdjustmentTerms terms)
    {
        terms = Guard.Against.Null(terms, nameof(terms));
        Type = Guard.Against.EnumOutOfRange(terms.Type, nameof(terms.Type));

        ClearTerms();

        switch (Type)
        {
            case ContractTypeDetailAdjustmentType.PriceIndex:
                SetPriceIndexTerms(terms);
                break;

            case ContractTypeDetailAdjustmentType.Currency:
                SetCurrencyTerms(terms);
                break;

            case ContractTypeDetailAdjustmentType.Other:
                SetOtherTerms(terms);
                break;

            default:
                throw new InvalidOperationException(
                    $"ContractTypeDetail adjustment Type {Type} is not supported.");
        }
    }

    private void SetPriceIndexTerms(ContractTypeDetailAdjustmentTerms terms)
    {
        EnsureNoCurrencyTerms(terms);
        EnsureNoOtherTerms(terms);

        if (!terms.PriceIndexBaseYear.HasValue ||
            !terms.PriceIndexBasePeriod.HasValue ||
            !terms.PriceIndexId.HasValue)
        {
            throw new InvalidOperationException(
                "PriceIndex base year and period are required.");
        }

        PriceIndexId = Guard.Against.NegativeOrZero(
            terms.PriceIndexId.Value,
            nameof(terms.PriceIndexId));
        PriceIndexBaseYear = Guard.Against.NegativeOrZero(
            terms.PriceIndexBaseYear.Value,
            nameof(terms.PriceIndexBaseYear));
        PriceIndexBasePeriod = Guard.Against.EnumOutOfRange(
            terms.PriceIndexBasePeriod.Value,
            nameof(terms.PriceIndexBasePeriod));
    }

    private void SetCurrencyTerms(ContractTypeDetailAdjustmentTerms terms)
    {
        EnsureNoPriceIndexTerms(terms);
        EnsureNoOtherTerms(terms);

        if (!terms.CurrencyBaseDate.HasValue ||
            terms.CurrencyBaseDate.Value == default ||
            !terms.CurrencyBaseRate.HasValue ||
            !terms.CurrencyId.HasValue ||
            !terms.CurrencyReferenceType.HasValue)
        {
            throw new InvalidOperationException(
                "Currency base date, rate, currency, and reference type are required.");
        }

        CurrencyBaseDate = terms.CurrencyBaseDate.Value;
        CurrencyBaseRate = Guard.Against.NegativeOrZero(
            ContractFinancialMath.NormalizeRate(terms.CurrencyBaseRate.Value),
            nameof(terms.CurrencyBaseRate));
        CurrencyId = Guard.Against.NegativeOrZero(
            terms.CurrencyId.Value,
            nameof(terms.CurrencyId));
        CurrencyReferenceType = Guard.Against.EnumOutOfRange(
            terms.CurrencyReferenceType.Value,
            nameof(terms.CurrencyReferenceType));

        var requiresCustomReference = CurrencyReferenceType is
            ContractAdjustmentCurrencyReferenceType.Contractual or
            ContractAdjustmentCurrencyReferenceType.Other;

        if (requiresCustomReference)
        {
            CurrencyCustomReference = RequiredText(
                terms.CurrencyCustomReference,
                250,
                nameof(terms.CurrencyCustomReference));
        }
        else if (!string.IsNullOrWhiteSpace(terms.CurrencyCustomReference))
        {
            throw new InvalidOperationException(
                "CurrencyCustomReference is only valid for Contractual or Other references.");
        }
    }

    private void SetOtherTerms(ContractTypeDetailAdjustmentTerms terms)
    {
        EnsureNoPriceIndexTerms(terms);
        EnsureNoCurrencyTerms(terms);

        OtherBasis = RequiredText(terms.OtherBasis, 250, nameof(terms.OtherBasis));
        OtherReference = RequiredText(terms.OtherReference, 250, nameof(terms.OtherReference));
        OtherIndex = RequiredText(terms.OtherIndex, 250, nameof(terms.OtherIndex));
        Description = RequiredText(terms.Description, 1500, nameof(terms.Description));
    }

    private static void EnsureNoPriceIndexTerms(ContractTypeDetailAdjustmentTerms terms)
    {
        if (terms.PriceIndexBaseYear.HasValue ||
            terms.PriceIndexBasePeriod.HasValue ||
            terms.PriceIndexId.HasValue)
        {
            throw new InvalidOperationException(
                "PriceIndex terms are not valid for this adjustment type.");
        }
    }

    private static void EnsureNoCurrencyTerms(ContractTypeDetailAdjustmentTerms terms)
    {
        if (terms.CurrencyBaseDate.HasValue ||
            terms.CurrencyBaseRate.HasValue ||
            terms.CurrencyId.HasValue ||
            terms.CurrencyReferenceType.HasValue ||
            !string.IsNullOrWhiteSpace(terms.CurrencyCustomReference))
        {
            throw new InvalidOperationException(
                "Currency terms are not valid for this adjustment type.");
        }
    }

    private static void EnsureNoOtherTerms(ContractTypeDetailAdjustmentTerms terms)
    {
        if (!string.IsNullOrWhiteSpace(terms.OtherBasis) ||
            !string.IsNullOrWhiteSpace(terms.OtherReference) ||
            !string.IsNullOrWhiteSpace(terms.OtherIndex) ||
            !string.IsNullOrWhiteSpace(terms.Description))
        {
            throw new InvalidOperationException(
                "Other adjustment terms are not valid for this adjustment type.");
        }
    }

    private void ClearTerms()
    {
        PriceIndexBaseYear = null;
        PriceIndexBasePeriod = null;
        PriceIndexId = null;
        PriceIndex = null;
        CurrencyBaseDate = null;
        CurrencyBaseRate = null;
        CurrencyId = null;
        CurrencyReferenceType = null;
        CurrencyCustomReference = null;
        OtherBasis = null;
        OtherReference = null;
        OtherIndex = null;
        Description = null;
    }

    private static string RequiredText(
        string? value,
        int maxLength,
        string parameterName)
    {
        var result = Guard.Against.NullOrWhiteSpace(value, parameterName);

        if (result.Length > maxLength)
            throw new ArgumentException(
                $"{parameterName} cannot exceed {maxLength} characters.",
                parameterName);

        return result;
    }

    #endregion
}
