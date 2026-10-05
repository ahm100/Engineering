using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Errors;
using Gita.Backend.Shared.Domain.Base.Results;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractChange)]
public class ContractChange : AuditableEntity<ContractChange, long>
{
    #region Properties

    public long ContractId { get; private set; }
    public Contract Contract { get; private set; } = null!;

    [Description(ContractCmts.ContractChangeMode)]
    public ContractChangeMode Mode { get; private set; }

    [Description(ContractCmts.ContractChangeType)]
    public ContractChangeType Type { get; private set; }

    [Description(ContractCmts.ContractChangeNumber)]
    public string Number { get; private set; } = string.Empty;

    [Description(GlobalCmts.Date)]
    public DateTime Date { get; private set; }

    [Description(ContractCmts.ContractChangeSubject)]
    public string Subject { get; private set; } = string.Empty;

    [Description(ContractCmts.ContractChangeDuration)]
    public int? DurationChange { get; private set; }

    [Description(ContractCmts.PreviousContractAmount)]
    public decimal PreviousContractAmount { get; private set; }

    [Description(ContractCmts.FinancialChangeAmount)]
    public decimal FinancialChangeAmount { get; private set; }

    [Description(ContractCmts.FinalContractAmount)]
    public decimal FinalContractAmount { get; private set; }

    private readonly List<ContractChangeItem> _items = [];
    public IReadOnlyList<ContractChangeItem> Items => _items;

    private readonly List<ContractChangeDocument> _documents = [];
    public IReadOnlyList<ContractChangeDocument> Documents => _documents;

    #endregion

    #region Constructors

    private ContractChange()
    {
    }

    public ContractChange(Contract contract, ContractChangeTerms terms)
    {
        SetContract(contract);
        Mode = ContractChangeMode.Detailed;
        SetHeader(terms);
        SetFinancialSnapshot(terms, false);
        AddDocuments(terms.Urls);
        AddItems(terms.Items);
    }

    public ContractChange(Contract contract, ContractSummaryChangeTerms terms)
    {
        SetContract(contract);
        Mode = ContractChangeMode.Summary;
        SetHeader(terms.Type, terms.Number, terms.Date, terms.Subject, terms.DurationChange);
        SetSummaryFinancialSnapshot(terms, false);
        AddOptionalDocuments(terms.Urls);
    }

    #endregion

    #region Commands

    public Result UpdateDetailed(ContractChangeTerms terms)
    {
        if (Mode != ContractChangeMode.Detailed)
            return Result.Failure(ContractErrors.ContractChangeModeInvalid);

        SetHeader(terms);
        SetFinancialSnapshot(terms, true);
        ReplaceDocuments(terms.Urls);
        ReplaceItems(terms.Items);
        return Result.Success();
    }

    public Result UpdateSummary(ContractSummaryChangeTerms terms)
    {
        if (Mode != ContractChangeMode.Summary)
            return Result.Failure(ContractErrors.ContractChangeModeInvalid);

        SetHeader(terms.Type, terms.Number, terms.Date, terms.Subject, terms.DurationChange);
        SetSummaryFinancialSnapshot(terms, true);
        ReplaceOptionalDocuments(terms.Urls);
        return Result.Success();
    }

    public decimal CalculateFinancialChangeAmount()
        => FinancialChangeAmount;

    public void Delete()
    {
        foreach (var item in _items.Where(oo => !oo.IsDeleted))
            item.SoftDelete();

        foreach (var document in _documents.Where(oo => !oo.IsDeleted))
            document.SoftDelete();

        SoftDelete();
    }

    #endregion

    #region Private Helpers

    private void SetContract(Contract value)
    {
        Contract = Guard.Against.Null(value, nameof(value));
        ContractId = value.Id;
    }

    private void SetHeader(ContractChangeTerms terms)
    {
        terms = Guard.Against.Null(terms, nameof(terms));
        SetHeader(terms.Type, terms.Number, terms.Date, terms.Subject, terms.DurationChange);
    }

    private void SetHeader(ContractChangeType type, string number, DateTime date,
        string subject, int? durationChange)
    {
        Type = Guard.Against.EnumOutOfRange(type, nameof(type));
        Number = RequiredText(number, 250, nameof(number));
        Date = date == default
            ? throw new ArgumentException("Date is required.", nameof(date))
            : date;
        Subject = RequiredText(subject, 250, nameof(subject));

        if (durationChange == 0)
            throw new InvalidOperationException("DurationChange cannot be zero.");

        DurationChange = durationChange;
    }

    private void SetSummaryFinancialSnapshot(ContractSummaryChangeTerms terms,
        bool preservePreviousAmount)
    {
        var previousAmount = ContractFinancialMath.NormalizeMoney(terms.PreviousContractAmount);
        var changeAmount = ContractFinancialMath.NormalizeMoney(terms.FinancialChangeAmount);
        var finalAmount = ContractFinancialMath.NormalizeMoney(terms.FinalContractAmount);

        if (preservePreviousAmount && previousAmount != PreviousContractAmount)
            throw new InvalidOperationException("PreviousContractAmount cannot change when updating a ContractChange.");
        if (finalAmount != ContractFinancialMath.NormalizeMoney(previousAmount + changeAmount))
            throw new InvalidOperationException("FinalContractAmount must equal PreviousContractAmount plus FinancialChangeAmount.");
        if (finalAmount < 0m)
            throw new InvalidOperationException("FinalContractAmount cannot be negative.");

        PreviousContractAmount = previousAmount;
        FinancialChangeAmount = changeAmount;
        FinalContractAmount = finalAmount;
    }

    private void SetFinancialSnapshot(
        ContractChangeTerms terms,
        bool preservePreviousAmount)
    {
        var previousAmount = ContractFinancialMath.NormalizeMoney(
            terms.PreviousContractAmount);
        var changeAmount = ContractFinancialMath.NormalizeMoney(
            terms.FinancialChangeAmount);
        var finalAmount = ContractFinancialMath.NormalizeMoney(
            terms.FinalContractAmount);

        if (preservePreviousAmount && previousAmount != PreviousContractAmount)
            throw new InvalidOperationException(
                "PreviousContractAmount cannot change when updating a ContractChange.");

        var calculatedChangeAmount = ContractFinancialMath.NormalizeMoney(
            terms.Items.Sum(oo => ContractChangeItem.CalculateChangeAmount(
                oo.PricingMethod,
                oo.PreviousValue,
                oo.NewValue,
                oo.UnitPrice)));

        if (changeAmount != calculatedChangeAmount)
            throw new InvalidOperationException(
                "FinancialChangeAmount must equal the sum of ContractChange item amounts.");

        if (finalAmount != ContractFinancialMath.NormalizeMoney(previousAmount + changeAmount))
            throw new InvalidOperationException(
                "FinalContractAmount must equal PreviousContractAmount plus FinancialChangeAmount.");

        if (finalAmount < 0m)
            throw new InvalidOperationException(
                "FinalContractAmount cannot be negative.");

        PreviousContractAmount = previousAmount;
        FinancialChangeAmount = changeAmount;
        FinalContractAmount = finalAmount;
    }

    private void ReplaceDocuments(IEnumerable<string> urls)
    {
        foreach (var document in _documents.Where(oo => !oo.IsDeleted))
            document.SoftDelete();

        AddDocuments(urls);
    }

    private void ReplaceOptionalDocuments(IEnumerable<string> urls)
    {
        foreach (var document in _documents.Where(oo => !oo.IsDeleted))
            document.SoftDelete();
        AddOptionalDocuments(urls);
    }

    private void AddOptionalDocuments(IEnumerable<string>? urls)
    {
        foreach (var url in urls ?? [])
            _documents.Add(new ContractChangeDocument(this, url));
    }

    private void AddDocuments(IEnumerable<string> urls)
    {
        var values = urls?.ToList() ?? [];

        if (values.Count == 0)
            throw new InvalidOperationException("At least one ContractChange document is required.");

        foreach (var url in values)
            _documents.Add(new ContractChangeDocument(this, url));
    }

    private void ReplaceItems(IEnumerable<ContractChangeItemTerms> terms)
    {
        foreach (var item in _items.Where(oo => !oo.IsDeleted))
            item.SoftDelete();

        AddItems(terms);
    }

    private void AddItems(IEnumerable<ContractChangeItemTerms> terms)
    {
        var values = terms?.ToList() ?? [];

        if (values
                .Where(oo => oo.ContractTypeDetailId.HasValue)
                .GroupBy(oo => oo.ContractTypeDetailId!.Value)
                .Any(oo => oo.Count() > 1) ||
            values
                .Where(oo => oo.ContractTypeId.HasValue && oo.ProjectOperationDetailId.HasValue)
                .GroupBy(oo => new
                {
                    ContractTypeId = oo.ContractTypeId!.Value,
                    ProjectOperationDetailId = oo.ProjectOperationDetailId!.Value
                })
                .Any(oo => oo.Count() > 1))
        {
            throw new InvalidOperationException("ContractChange contains duplicate logical items.");
        }

        foreach (var itemTerms in values)
            _items.Add(new ContractChangeItem(this, itemTerms));
    }

    private static string RequiredText(string value, int maxLength, string parameterName)
    {
        value = Guard.Against.NullOrWhiteSpace(value, parameterName);

        if (value.Length > maxLength)
            throw new ArgumentException($"{parameterName} cannot exceed {maxLength} characters.", parameterName);

        return value;
    }
    #endregion

}
