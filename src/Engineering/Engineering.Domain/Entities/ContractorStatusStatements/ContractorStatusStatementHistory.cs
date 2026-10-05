using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

public class ContractorStatusStatementHistory : AuditableEntity<ContractorStatusStatementHistory>
{

    #region Properties

    [Description(GlobalCmts.Code)]
    public string Code { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(GlobalCmts.Status)]
    public CSSStatus Status { get; private set; } = CSSStatus.New;

    [Description(CSSCmts.FinalTotalAmount)]
    public decimal FinalTotalAmount { get; private set; }

    [Description(CSSCmts.TotalPercentageDoingJobWell)]
    public decimal TotalPercentageDoingJobWell { get; private set; }

    [Description(CSSCmts.TotalDoingJobWellAmount)]
    public decimal TotalDoingJobWellAmount { get; private set; }

    [Description(CSSCmts.TotalPercentageAdvancePayment)]
    public decimal TotalPercentageAdvancePayment { get; private set; }

    [Description(CSSCmts.TotalAdvancePaymentAmount)]
    public decimal TotalAdvancePaymentAmount { get; private set; }

    [Description(CSSCmts.TotalDailyLatenessPenalty)]
    public decimal TotalDailyLatenessPenalty { get; private set; }

    [Description(CSSCmts.TotalWorkDonePercent)]
    public decimal TotalWorkDonePercent { get; private set; }

    [Description(CSSCmts.TotalWorkDeliveryPercent)]
    public decimal TotalWorkDeliveryPercent { get; private set; }

    [Description(CSSCmts.TotalWorkCompletionPercent)]
    public decimal TotalWorkCompletionPercent { get; private set; }

    [Description(CSSCmts.ProductsAmount)]
    public decimal? ProductsAmount { get; private set; } = 0;

    [Description(CSSCmts.FinesAmount)]
    public decimal? FinesAmount { get; private set; } = 0;

    [Description(CSSCmts.RewardsAmount)]
    public decimal? RewardsAmount { get; private set; } = 0;

    [Description(CSSCmts.CostOversAmount)]
    public decimal? CostOversAmount { get; private set; } = 0;

    [Description(CSSCmts.ThirdPartiesAmount)]
    public decimal? ThirdPartiesAmount { get; private set; } = 0;

    [Description(CSSCmts.PaymentedAmount)]
    public decimal? PaymentedAmount { get; private set; } = 0;

    [Description(CSSCmts.Description)]
    public string? Description { get; private set; }

    [Description(CSSCmts.ManagmentDescription)]
    public string? ManagmentDescription { get; private set; }

    [Description(CSSCmts.ContractorStatusStatement)]
    public long ContractorStatusStatementId { get; private set; }
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }

    #endregion

    #region Constructors

    public ContractorStatusStatementHistory(
        ContractorStatusStatement contractorStatusStatement,
        string code,
        DateTime startDate,
        DateTime endDate,
        CSSStatus status,
        decimal finalTotalAmount,
        decimal totalPercentageDoingJobWell,
        decimal totalDoingJobWellAmount,
        decimal totalAdvancePaymentAmount,
        decimal totalPercentageAdvancePayment,
        decimal totalDailyLatenessPenalty,
        decimal totalWorkDonePercent,
        decimal totalWorkDeliveryPercent,
        decimal totalWorkCompletionPercent,
        decimal? productsAmount,
        decimal? finesAmount,
        decimal? rewardsAmount,
        decimal? costOversAmount,
        decimal? thirdPartiesAmount,
        decimal? paymentedAmount,
        string? description) : this()
    {
        SetContractorStatusStatement(contractorStatusStatement);
        SetCode(code);
        SetStatus(status);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetFinalTotalAmount(finalTotalAmount);
        SetTotalPercentageDoingJobWell(totalPercentageDoingJobWell);
        SetTotalDoingJobWellAmount(totalDoingJobWellAmount);
        SetTotalPercentageAdvancePayment(totalPercentageAdvancePayment);
        SetTotalAdvancePaymentAmount(totalAdvancePaymentAmount);
        SetTotalDailyLatenessPenalty(totalDailyLatenessPenalty);
        SetTotalWorkDonePercent(totalWorkDonePercent);
        SetTotalWorkDeliveryPercent(totalWorkDeliveryPercent);
        SetTotalWorkCompletionPercent(totalWorkCompletionPercent);
        SetProductsAmount(productsAmount);
        SetFinesAmount(finesAmount);
        SetRewardsAmount(rewardsAmount);
        SetCostOversAmount(costOversAmount);
        SetThirdPartiesAmount(thirdPartiesAmount);
        SetPaymentedAmount(paymentedAmount);
        SetDescription(description);
    }

    public void SetContractorStatusStatement(ContractorStatusStatement value)
    {
        ContractorStatusStatement = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCode(string value)
    {
        Code = Guard.Against.Null(value, nameof(value));
    }

    public void SetStatus(CSSStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetFinalTotalAmount(decimal value)
    {
        FinalTotalAmount = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalPercentageDoingJobWell(decimal value)
    {
        TotalPercentageDoingJobWell = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalDoingJobWellAmount(decimal value)
    {
        TotalDoingJobWellAmount = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalPercentageAdvancePayment(decimal value)
    {
        TotalPercentageAdvancePayment = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalAdvancePaymentAmount(decimal value)
    {
        TotalAdvancePaymentAmount = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalDailyLatenessPenalty(decimal value)
    {
        TotalDailyLatenessPenalty = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalWorkDonePercent(decimal value)
    {
        TotalWorkDonePercent = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalWorkDeliveryPercent(decimal value)
    {
        TotalWorkDeliveryPercent = Guard.Against.Null(value, nameof(value));
    }

    public void SetTotalWorkCompletionPercent(decimal value)
    {
        TotalWorkCompletionPercent = Guard.Against.Null(value, nameof(value));
    }

    public void SetProductsAmount(decimal? value)
    {
        ProductsAmount = value;
    }

    public void SetFinesAmount(decimal? value)
    {
        FinesAmount = value;
    }

    public void SetRewardsAmount(decimal? value)
    {
        RewardsAmount = value;
    }

    public void SetCostOversAmount(decimal? value)
    {
        CostOversAmount = value;
    }

    public void SetThirdPartiesAmount(decimal? value)
    {
        ThirdPartiesAmount = value;
    }

    public void SetPaymentedAmount(decimal? value)
    {
        PaymentedAmount = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementHistory()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
