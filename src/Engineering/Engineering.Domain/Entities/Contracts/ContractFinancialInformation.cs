using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractFinancialInformation)]
public class ContractFinancialInformation
    : AuditableEntity<ContractFinancialInformation, long>
{
    [Description(GlobalCmts.ContractId)]
    public long ContractId { get; private set; }
    public Contract Contract { get; private set; } = null!;

    [Description(GlobalCmts.CurrencyId)]
    public long CurrencyId { get; private set; }

    [Description(ContractCmts.HasPrepayment)]
    public bool HasPrepayment { get; private set; }

    [Description(ContractCmts.IsSubjectToAdjustment)]
    public bool IsSubjectToAdjustment { get; private set; }

    [Description(ContractCmts.RegisteredInitialAmount)]
    public decimal? RegisteredInitialAmount { get; private set; }

    [Description(ContractCmts.ContractCeilingAmount)]
    public decimal? ContractCeilingAmount { get; private set; }

    [Description(ContractCmts.AdjustmentLimitValue)]
    public decimal? AdjustmentLimitValue { get; private set; }

    [Description(ContractCmts.AdjustmentLimitType)]
    public ContractAdjustmentLimitType? AdjustmentLimitType { get; private set; }

    [Description(ContractCmts.PrepaymentPercentage)]
    public decimal? PrepaymentPercentage { get; private set; }

    [Description(ContractCmts.PrepaymentAmortizationMethod)]
    public PrepaymentAmortizationMethod? PrepaymentAmortizationMethod { get; private set; }

    [Description(ContractCmts.PrepaymentAmortizationValue)]
    public decimal? PrepaymentAmortizationValue { get; private set; }

    [Description(ContractCmts.PrepaymentStartStatusStatementNumber)]
    public int? PrepaymentStartStatusStatementNumber { get; private set; }

    [Description(ContractCmts.PrepaymentStartProgressPercentage)]
    public decimal? PrepaymentStartProgressPercentage { get; private set; }

    #region Constructors

    private ContractFinancialInformation()
    {
    }

    public ContractFinancialInformation(
        Contract contract,
        decimal calculatedInitialAmount,
        decimal calculatedCurrentContractAmount,
        long currencyId,
        bool hasPrepayment,
        bool isSubjectToAdjustment,
        decimal? contractCeilingAmount,
        decimal? adjustmentLimitValue,
        ContractAdjustmentLimitType? adjustmentLimitType,
        decimal? prepaymentPercentage,
        PrepaymentAmortizationMethod? prepaymentAmortizationMethod,
        decimal? prepaymentAmortizationValue,
        int? prepaymentStartStatusStatementNumber,
        decimal? prepaymentStartProgressPercentage,
        decimal? registeredInitialAmount = null)
    {
        SetContract(contract);
        SetCurrencyId(currencyId);
        SetIsSubjectToAdjustment(isSubjectToAdjustment);
        SetRegisteredInitialAmount(registeredInitialAmount);
        SetContractCeilingAmount(contractCeilingAmount);
        ValidateCalculatedAmounts(
            calculatedInitialAmount,
            calculatedCurrentContractAmount);
        SetAdjustmentLimit(adjustmentLimitValue, adjustmentLimitType);

        SetPrepaymentTerms(
            calculatedInitialAmount,
            hasPrepayment,
            prepaymentPercentage,
            prepaymentAmortizationMethod,
            prepaymentAmortizationValue,
            prepaymentStartStatusStatementNumber,
            prepaymentStartProgressPercentage);
    }

    #endregion

    #region Commands

    public void Update(
        decimal calculatedInitialAmount,
        decimal calculatedCurrentContractAmount,
        long currencyId,
        bool hasPrepayment,
        bool isSubjectToAdjustment,
        decimal? contractCeilingAmount,
        decimal? adjustmentLimitValue,
        ContractAdjustmentLimitType? adjustmentLimitType,
        decimal? prepaymentPercentage,
        PrepaymentAmortizationMethod? prepaymentAmortizationMethod,
        decimal? prepaymentAmortizationValue,
        int? prepaymentStartStatusStatementNumber,
        decimal? prepaymentStartProgressPercentage,
        decimal? registeredInitialAmount = null)
    {
        SetCurrencyId(currencyId);
        SetIsSubjectToAdjustment(isSubjectToAdjustment);
        SetRegisteredInitialAmount(registeredInitialAmount);
        SetContractCeilingAmount(contractCeilingAmount);
        ValidateCalculatedAmounts(
            calculatedInitialAmount,
            calculatedCurrentContractAmount);
        SetAdjustmentLimit(adjustmentLimitValue, adjustmentLimitType);

        SetPrepaymentTerms(
            calculatedInitialAmount,
            hasPrepayment,
            prepaymentPercentage,
            prepaymentAmortizationMethod,
            prepaymentAmortizationValue,
            prepaymentStartStatusStatementNumber,
            prepaymentStartProgressPercentage);
    }

    #endregion

    #region Private Helpers

    private void SetContract(Contract value)
    {
        Contract = Guard.Against.Null(value, nameof(value));
        ContractId = value.Id;
    }

    private void SetCurrencyId(long value)
        => CurrencyId = Guard.Against.NegativeOrZero(value, nameof(value));

    private void SetIsSubjectToAdjustment(bool value)
        => IsSubjectToAdjustment = value;

    private void SetRegisteredInitialAmount(decimal? value)
        => RegisteredInitialAmount = value.HasValue
            ? Guard.Against.NegativeOrZero(
                ContractFinancialMath.NormalizeMoney(value.Value),
                nameof(value))
            : null;

    private void SetContractCeilingAmount(decimal? value)
        => ContractCeilingAmount = value.HasValue
            ? Guard.Against.NegativeOrZero(
                ContractFinancialMath.NormalizeMoney(value.Value),
                nameof(value))
            : null;

    private void SetAdjustmentLimit(
        decimal? value,
        ContractAdjustmentLimitType? type)
    {
        if (!value.HasValue && !type.HasValue)
        {
            AdjustmentLimitValue = null;
            AdjustmentLimitType = null;
            return;
        }

        if (!value.HasValue || !type.HasValue)
            throw new InvalidOperationException(
                "AdjustmentLimitValue and AdjustmentLimitType must be provided together.");

        AdjustmentLimitType = Guard.Against.EnumOutOfRange(
            type.Value,
            nameof(type));

        if (AdjustmentLimitType == ContractAdjustmentLimitType.Percentage &&
            value.Value > 100m)
        {
            throw new InvalidOperationException(
                "Percentage AdjustmentLimitValue cannot be greater than 100.");
        }

        AdjustmentLimitValue = Guard.Against.NegativeOrZero(
            ContractFinancialMath.NormalizeMoney(value.Value),
            nameof(value));
    }

    private void SetPrepaymentTerms(
        decimal calculatedInitialAmount,
        bool hasPrepayment,
        decimal? prepaymentPercentage,
        PrepaymentAmortizationMethod? amortizationMethod,
        decimal? amortizationValue,
        int? startStatusStatementNumber,
        decimal? startProgressPercentage)
    {
        HasPrepayment = hasPrepayment;

        if (!hasPrepayment)
        {
            if (prepaymentPercentage.HasValue ||
                amortizationMethod.HasValue ||
                amortizationValue.HasValue ||
                startStatusStatementNumber.HasValue ||
                startProgressPercentage.HasValue)
            {
                throw new InvalidOperationException(
                    "Prepayment terms are not valid when contract is not subject to prepayment.");
            }

            PrepaymentPercentage = null;
            PrepaymentAmortizationMethod = null;
            PrepaymentAmortizationValue = null;
            PrepaymentStartStatusStatementNumber = null;
            PrepaymentStartProgressPercentage = null;
            return;
        }

        if (calculatedInitialAmount <= 0)
            throw new InvalidOperationException(
                "CalculatedInitialAmount must be positive when contract is subject to prepayment.");

        if (!prepaymentPercentage.HasValue ||
            !amortizationMethod.HasValue ||
            !amortizationValue.HasValue)
        {
            throw new InvalidOperationException(
                "Prepayment percentage, amortization method and amortization value are required.");
        }

        if (prepaymentPercentage.Value > 100m)
            throw new InvalidOperationException(
                "PrepaymentPercentage cannot be greater than 100.");

        PrepaymentPercentage = Guard.Against.NegativeOrZero(
            ContractFinancialMath.NormalizePercentage(prepaymentPercentage.Value),
            nameof(prepaymentPercentage));

        PrepaymentAmortizationMethod = Guard.Against.EnumOutOfRange(
            amortizationMethod.Value,
            nameof(amortizationMethod));

        PrepaymentAmortizationValue = Guard.Against.NegativeOrZero(
            ContractFinancialMath.NormalizeMoney(amortizationValue.Value),
            nameof(amortizationValue));

        switch (amortizationMethod.Value)
        {
            case Enums.PrepaymentAmortizationMethod.FixedPercentagePerStatusStatement:
                if (!startStatusStatementNumber.HasValue || startProgressPercentage.HasValue)
                    throw new InvalidOperationException(
                        "Status statement number is required for status-statement amortization.");

                if (amortizationValue.Value > 100m)
                    throw new InvalidOperationException(
                        "Percentage amortization value cannot be greater than 100.");

                PrepaymentStartStatusStatementNumber =
                    Guard.Against.NegativeOrZero(
                        startStatusStatementNumber.Value,
                        nameof(startStatusStatementNumber));

                PrepaymentStartProgressPercentage = null;
                break;

            case Enums.PrepaymentAmortizationMethod.FixedAmountPerStatusStatement:
                if (!startStatusStatementNumber.HasValue || startProgressPercentage.HasValue)
                    throw new InvalidOperationException(
                        "Status statement number is required for status-statement amortization.");

                PrepaymentStartStatusStatementNumber =
                    Guard.Against.NegativeOrZero(
                        startStatusStatementNumber.Value,
                        nameof(startStatusStatementNumber));

                PrepaymentStartProgressPercentage = null;
                break;

            case Enums.PrepaymentAmortizationMethod.AfterSpecificProgressPercentage:
                if (!startProgressPercentage.HasValue || startStatusStatementNumber.HasValue)
                    throw new InvalidOperationException(
                        "Start progress percentage is required for progress-based amortization.");

                if (startProgressPercentage.Value > 100m)
                    throw new InvalidOperationException(
                        "PrepaymentStartProgressPercentage cannot be greater than 100.");

                PrepaymentStartProgressPercentage =
                    Guard.Against.NegativeOrZero(
                        ContractFinancialMath.NormalizePercentage(
                            startProgressPercentage.Value),
                        nameof(startProgressPercentage));

                PrepaymentStartStatusStatementNumber = null;
                break;

            default:
                throw new InvalidOperationException(
                    "Invalid prepayment amortization method.");
        }
    }

    private void ValidateCalculatedAmounts(
        decimal calculatedInitialAmount,
        decimal calculatedCurrentContractAmount)
    {
        if (calculatedInitialAmount < 0 || calculatedCurrentContractAmount < 0)
            throw new ArgumentOutOfRangeException(
                nameof(calculatedInitialAmount),
                "Calculated contract amounts cannot be negative.");

        if (ContractCeilingAmount.HasValue &&
            ContractCeilingAmount.Value < calculatedCurrentContractAmount)
        {
            throw new InvalidOperationException(
                "ContractCeilingAmount cannot be less than CalculatedCurrentContractAmount.");
        }
    }

    #endregion
}
