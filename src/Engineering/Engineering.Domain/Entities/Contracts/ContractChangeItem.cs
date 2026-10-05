using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractChangeItem)]
public class ContractChangeItem : AuditableEntity<ContractChangeItem, long>
{
    public long ContractChangeId { get; private set; }
    public ContractChange ContractChange { get; private set; } = null!;

    public long? ContractTypeDetailId { get; private set; }
    public ContractTypeDetail? ContractTypeDetail { get; private set; }

    public long? ContractTypeId { get; private set; }
    public ContractType? ContractType { get; private set; }

    [Description(GlobalCmts.ProjectOperationDetailId)]
    public long? ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail? ProjectOperationDetail { get; private set; }

    [Description(GlobalCmts.PricingMethod)]
    public PricingMethod PricingMethod { get; private set; }

    [Description(ContractCmts.PreviousValue)]
    public decimal PreviousValue { get; private set; }

    [Description(ContractCmts.NewValue)]
    public decimal NewValue { get; private set; }

    [Description(ContractCmts.UnitOfMeasurementId)]
    public long? UnitOfMeasurementId { get; private set; }

    [Description(ContractCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(ContractCmts.ChangeAmount)]
    public decimal ChangeAmount { get; private set; }

    private ContractChangeItem()
    {
    }

    public ContractChangeItem(
        ContractChange contractChange,
        ContractChangeItemTerms terms)
    {
        SetContractChange(contractChange);
        SetTerms(terms);
    }

    public static decimal CalculateChangeAmount(
        PricingMethod pricingMethod,
        decimal previousValue,
        decimal newValue,
        decimal? unitPrice)
        => ContractFinancialMath.CalculateContractChangeAmount(
            pricingMethod,
            previousValue,
            newValue,
            unitPrice);

    private void SetContractChange(ContractChange value)
    {
        ContractChange = Guard.Against.Null(value, nameof(value));
        ContractChangeId = value.Id;
    }

    private void SetTerms(ContractChangeItemTerms terms)
    {
        terms = Guard.Against.Null(terms, nameof(terms));

        var isExisting = terms.ContractTypeDetailId.HasValue &&
                         !terms.ContractTypeId.HasValue &&
                         !terms.ProjectOperationDetailId.HasValue;
        var isSource = !terms.ContractTypeDetailId.HasValue &&
                       terms.ContractTypeId.HasValue &&
                       terms.ProjectOperationDetailId.HasValue;

        if (isExisting == isSource)
            throw new InvalidOperationException("Exactly one ContractChangeItem origin is required.");

        ContractTypeDetailId = terms.ContractTypeDetailId.HasValue
            ? Guard.Against.NegativeOrZero(terms.ContractTypeDetailId.Value, nameof(terms.ContractTypeDetailId))
            : null;
        ContractTypeId = terms.ContractTypeId.HasValue
            ? Guard.Against.NegativeOrZero(terms.ContractTypeId.Value, nameof(terms.ContractTypeId))
            : null;
        ProjectOperationDetailId = terms.ProjectOperationDetailId.HasValue
            ? Guard.Against.NegativeOrZero(terms.ProjectOperationDetailId.Value, nameof(terms.ProjectOperationDetailId))
            : null;

        PricingMethod = Guard.Against.EnumOutOfRange(terms.PricingMethod, nameof(terms.PricingMethod));

        if (PricingMethod == PricingMethod.CostPlus)
            throw new InvalidOperationException("CostPlus ContractChange items are disabled.");

        if (terms.PreviousValue < 0 || terms.NewValue < 0)
            throw new InvalidOperationException("ContractChange item values cannot be negative.");

        var previousValue = ContractFinancialMath.NormalizeChangeValue(terms.PreviousValue);
        var newValue = ContractFinancialMath.NormalizeChangeValue(terms.NewValue);

        if (previousValue == newValue)
            throw new InvalidOperationException("ContractChange item must represent an effective change.");

        PreviousValue = previousValue;
        NewValue = newValue;
        UnitOfMeasurementId = terms.UnitOfMeasurementId.HasValue
            ? Guard.Against.NegativeOrZero(
                terms.UnitOfMeasurementId.Value,
                nameof(terms.UnitOfMeasurementId))
            : null;

        if (PricingMethod == PricingMethod.LumpSum)
        {
            if (isSource)
                throw new InvalidOperationException("Source-based LumpSum changes are not supported.");

            UnitPrice = null;
            ChangeAmount = CalculateChangeAmount(
                PricingMethod,
                PreviousValue,
                NewValue,
                null);
            return;
        }

        if (PricingMethod is not (PricingMethod.UnitPrice or PricingMethod.TimeAndMaterial))
            throw new InvalidOperationException("ContractChange PricingMethod is not supported.");

        UnitPrice = Guard.Against.NegativeOrZero(
            ContractFinancialMath.NormalizeMoney(terms.UnitPrice ?? 0m),
            nameof(terms.UnitPrice));
        ChangeAmount = CalculateChangeAmount(
            PricingMethod,
            PreviousValue,
            NewValue,
            UnitPrice);
    }
}
