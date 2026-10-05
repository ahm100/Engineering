using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

public class ContractAdjustmentConfiguration
    : AuditableEntity<ContractAdjustmentConfiguration, long>
{
    #region Properties

    public long ContractId { get; private set; }
    public Contract Contract { get; private set; } = null!;
    public ContractTypeDetailAdjustmentType Type { get; private set; }
    public int? PriceIndexBaseYear { get; private set; }
    public ContractAdjustmentPeriod? PriceIndexBasePeriod { get; private set; }
    public long? PriceIndexId { get; private set; }
    public ContractAdjustmentIndex? PriceIndex { get; private set; }
    public DateTime? CurrencyBaseDate { get; private set; }
    public decimal? CurrencyBaseRate { get; private set; }
    public long? CurrencyId { get; private set; }
    public ContractAdjustmentCurrencyReferenceType? CurrencyReferenceType { get; private set; }
    public string? CurrencyCustomReference { get; private set; }
    public string? OtherBasis { get; private set; }
    public string? OtherReference { get; private set; }
    public string? OtherIndex { get; private set; }
    public string? Description { get; private set; }

    private readonly List<ContractAdjustmentScope> _scopes = [];
    public IReadOnlyList<ContractAdjustmentScope> Scopes => _scopes;

    #endregion

    #region Constructors

    private ContractAdjustmentConfiguration()
    {
    }

    public ContractAdjustmentConfiguration(
        Contract contract,
        ContractTypeDetailAdjustmentTerms terms,
        bool wholeContract,
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        Contract = Guard.Against.Null(contract, nameof(contract));
        ContractId = contract.Id;
        ApplyTerms(terms);
        ReplaceScopes(wholeContract, kinds, detailIds);
    }

    #endregion

    #region Commands

    public void Update(
        ContractTypeDetailAdjustmentTerms terms,
        bool wholeContract,
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        ApplyTerms(terms);
        ReplaceScopes(wholeContract, kinds, detailIds);
    }

    public ContractTypeDetailAdjustmentTerms GetAdjustmentTerms() => new(
        Type,
        PriceIndexBaseYear,
        PriceIndexBasePeriod,
        PriceIndexId,
        CurrencyBaseDate,
        CurrencyBaseRate,
        CurrencyId,
        CurrencyReferenceType,
        CurrencyCustomReference,
        OtherBasis,
        OtherReference,
        OtherIndex,
        Description);

    public bool Covers(ContractTypeDetail detail)
    {
        return _scopes.Any(scope =>
            !scope.IsDeleted &&
            (scope.ScopeType == ContractAdjustmentScopeType.WholeContract ||
             scope.ScopeType == ContractAdjustmentScopeType.ContractTypeKind &&
             scope.ContractTypeKind == detail.ContractType.Kind ||
             scope.ScopeType == ContractAdjustmentScopeType.ContractTypeDetail &&
             scope.ContractTypeDetailId == detail.Id));
    }

    public bool CoversNewDetail(ContractTypeKind kind)
    {
        return _scopes.Any(scope =>
            !scope.IsDeleted &&
            (scope.ScopeType == ContractAdjustmentScopeType.WholeContract ||
             scope.ScopeType == ContractAdjustmentScopeType.ContractTypeKind &&
             scope.ContractTypeKind == kind));
    }

    public bool Overlaps(
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        return _scopes.Any(scope =>
            !scope.IsDeleted &&
            (scope.ScopeType == ContractAdjustmentScopeType.WholeContract ||
             scope.ContractTypeKind.HasValue && kinds.Contains(scope.ContractTypeKind.Value) ||
             scope.ContractTypeDetailId.HasValue && detailIds.Contains(scope.ContractTypeDetailId.Value)));
    }

    public void RemoveDetailScope(long detailId)
    {
        foreach (var scope in _scopes.Where(oo =>
                     !oo.IsDeleted &&
                     oo.ScopeType == ContractAdjustmentScopeType.ContractTypeDetail &&
                     oo.ContractTypeDetailId == detailId))
        {
            scope.SoftDelete();
        }
    }

    #endregion

    #region Private Helpers

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
                    $"Contract adjustment Type {Type} is not supported.");
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
                "PriceIndex base year, period, and index are required.");
        }

        PriceIndexBaseYear = Guard.Against.NegativeOrZero(
            terms.PriceIndexBaseYear.Value,
            nameof(terms.PriceIndexBaseYear));
        PriceIndexBasePeriod = Guard.Against.EnumOutOfRange(
            terms.PriceIndexBasePeriod.Value,
            nameof(terms.PriceIndexBasePeriod));
        PriceIndexId = Guard.Against.NegativeOrZero(
            terms.PriceIndexId.Value,
            nameof(terms.PriceIndexId));
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

    private void ReplaceScopes(
        bool wholeContract,
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        kinds = Guard.Against.Null(kinds, nameof(kinds));
        detailIds = Guard.Against.Null(detailIds, nameof(detailIds));

        var invalidScope =
            wholeContract && (kinds.Count > 0 || detailIds.Count > 0) ||
            !wholeContract && kinds.Count == 0 && detailIds.Count == 0 ||
            kinds.Count != kinds.Distinct().Count() ||
            detailIds.Count != detailIds.Distinct().Count() ||
            kinds.Any(kind => kind is not (
                ContractTypeKind.Engineering or
                ContractTypeKind.Procurement or
                ContractTypeKind.Construction or
                ContractTypeKind.Services)) ||
            detailIds.Any(id => id <= 0);

        if (invalidScope)
            throw new InvalidOperationException(
                "Contract adjustment scope is invalid.");

        foreach (var scope in _scopes.Where(oo => !oo.IsDeleted))
            scope.SoftDelete();

        if (wholeContract)
        {
            _scopes.Add(new ContractAdjustmentScope(
                this,
                ContractAdjustmentScopeType.WholeContract,
                null,
                null));
        }

        foreach (var kind in kinds)
        {
            _scopes.Add(new ContractAdjustmentScope(
                this,
                ContractAdjustmentScopeType.ContractTypeKind,
                kind,
                null));
        }

        foreach (var detailId in detailIds)
        {
            _scopes.Add(new ContractAdjustmentScope(
                this,
                ContractAdjustmentScopeType.ContractTypeDetail,
                null,
                detailId));
        }
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
        {
            throw new ArgumentException(
                $"{parameterName} cannot exceed {maxLength} characters.",
                parameterName);
        }

        return result;
    }
    #endregion

}
