using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

public class ContractorStatusStatementPayment : AuditableEntity<ContractorStatusStatementPayment, long>
{
    [Description(CSSCmts.CalculatedAmount)]
    public decimal CalculatedAmount { get; private set; }

    [Description(CSSCmts.PayableAmount)]
    public decimal PayableAmount { get; private set; }

    [Description(CSSCmts.PaymentedAmount)]
    public decimal PaymentedAmount { get; private set; } = 0;

    [Description(CSSCmts.UserAmount)]
    public decimal UserAmount { get; private set; }

    [Description(CSSCmts.UserDescription)]
    public string? UserDescription { get; private set; }

    [Description(CSSCmts.Status)]
    public CSSPaymentStatus Status { get; private set; } = CSSPaymentStatus.Unpaid;

    [Description(CSSCmts.ProjectManagerAmount)]
    public decimal? ProjectManagerAmount { get; private set; } = 0;

    [Description(CSSCmts.ProjectManagerDescription)]
    public string? ProjectManagerDescription { get; private set; }

    [Description(CSSCmts.ManagementAmount)]
    public decimal? ManagementAmount { get; private set; } = 0;

    [Description(CSSCmts.ManagementDescription)]
    public string? ManagementDescription { get; private set; }

    [Description(CSSCmts.PrimaryManagerAmount)]
    public decimal? PrimaryManagerAmount { get; private set; } = 0;

    [Description(CSSCmts.PrimaryManagerDescription)]
    public string? PrimaryManagerDescription { get; private set; }

    [Description(CSSCmts.FinalManagerAmount)]
    public decimal? FinalManagerAmount { get; private set; } = 0;

    [Description(CSSCmts.FinalManagerDescription)]
    public string? FinalManagerDescription { get; private set; }

    [Description(CSSCmts.PaymentAmount)]
    public decimal? PaymentAmount { get; private set; } = 0;

    [Description(CSSCmts.PaymentDescription)]
    public string? PaymentDescription { get; private set; }

    [Description(CSSCmts.PaymentOrderId)]
    public long? PaymentOrderId { get; private set; }

    [Description(CSSCmts.PaymentDate)]
    public DateTime? PaymentDate { get; private set; }

    [Description(CSSCmts.TreasuryPaid)]
    public decimal? TreasuryPaid { get; private set; } = 0;

    [Description(CSSCmts.ConfirmedBankAccountId)]
    public long? ConfirmedBankAccountId { get; private set; }

    [Description(GlobalCmts.ContractorStatusStatement)]
    public long ContractorStatusStatementId { get; private set; }
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }

    public ContractorStatusStatementPayment(
        ContractorStatusStatement status,
        decimal calculatedAmount,
        decimal paymentedAmount,
        decimal payableAmount,
        decimal userAmount,
        string? userDescription
        ) : this()
    {
        SetContractorStatusStatement(status);
        SetCalculatedAmount(calculatedAmount);
        SetPaymentedAmount(paymentedAmount);
        SetPayableAmount(payableAmount);
        SetUserAmount(userAmount);
        SetUserDescription(userDescription);

        SetStatus(CSSPaymentStatus.Unpaid);
    }

    public void Update(
        decimal calculatedAmount,
        decimal payableAmount,
        decimal userAmount,
        string? userDescription
        )
    {
        SetCalculatedAmount(calculatedAmount);
        SetPayableAmount(payableAmount);
        SetUserAmount(userAmount);
        SetUserDescription(userDescription);
    }


    public void SetProjectManager(
        decimal amount,
        string? description)
    {
        SetProjectManagerAmount(amount);
        SetProjectManagerDescription(description);
    }

    public void SetManagement(
        decimal amount,
        string? description)
    {
        SetManagementAmount(amount);
        SetManagementDescription(description);
    }

    public void SetPrimaryManager(
        decimal amount,
        string? description)
    {
        SetPrimaryManagerAmount(amount);
        SetPrimaryManagerDescription(description);
    }

    public void SetFinalManager(
        decimal amount,
        string? description)
    {
        SetFinalManagerAmount(amount);
        SetFinalManagerDescription(description);
    }

    public void SetPaymentOrder(
        long paymentOrderId,
        decimal amount,
        List<string>? urls,
        string? description)
    {
        SetPaymentOrderId(paymentOrderId);
        SetManagementAmount(amount);
        SetPaymentDescription(description);
        AddDocuments(urls);
        SetStatus(CSSPaymentStatus.AwaitingPayment);
    }

    public void SetTreasuryPay(
        CSSStatus status,
        decimal amount)
    {
        SetTreasuryPaid(amount);

        SetPaymentDate(DateTime.Now);

        if (status == CSSStatus.Paid)
            SetStatus(CSSPaymentStatus.Paid);
        else if (status == CSSStatus.RejectPaid)
            SetStatus(CSSPaymentStatus.RejectPayment);
        else if (status == CSSStatus.IncompletelyPaid)
            SetStatus(CSSPaymentStatus.PartlyPaid);
    }

    private void SetCalculatedAmount(decimal value)
    {
        CalculatedAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetPaymentedAmount(decimal value)
    {
        PaymentedAmount = Guard.Against.Null(value, nameof(value));
    }

    public void SetConfirmedBankAccountId(long? value)
    {
        ConfirmedBankAccountId = value;
    }

    private void SetPayableAmount(decimal value)
    {
        PayableAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetUserAmount(decimal value)
    {
        UserAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetUserDescription(string? value)
    {
        UserDescription = value;
    }

    private void SetStatus(CSSPaymentStatus value)
    {
        //if(_)
        Status = Guard.Against.Null(value, nameof(value));
    }

    private void SetProjectManagerAmount(decimal value)
    {
        ProjectManagerAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetProjectManagerDescription(string? value)
    {
        ProjectManagerDescription = value;
    }

    private void SetManagementAmount(decimal value)
    {
        ManagementAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetManagementDescription(string? value)
    {
        ManagementDescription = value;
    }

    private void SetPrimaryManagerAmount(decimal value)
    {
        PrimaryManagerAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetPrimaryManagerDescription(string? value)
    {
        PrimaryManagerDescription = value;
    }

    private void SetFinalManagerAmount(decimal value)
    {
        FinalManagerAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetFinalManagerDescription(string? value)
    {
        FinalManagerDescription = value;
    }

    private void SetPaymentOrderId(long? value)
    {
        PaymentOrderId = value;
    }

    private void SetPaymentDescription(string? value)
    {
        PaymentDescription = value;
    }

    private void SetTreasuryPaid(decimal? value)
    {
        TreasuryPaid = value;
    }

    private void SetPaymentDate(DateTime? value)
    {
        PaymentDate = value;
    }

    private void SetContractorStatusStatement(ContractorStatusStatement value)
    {
        ContractorStatusStatement = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void AddDocuments(List<string>? urls)
    {
        if (urls is not null && urls.Any())
            foreach (var url in urls)
                _cSSDocuments.Add(new ContractorStatusStatementDocument(
                    this.ContractorStatusStatement, this, url));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<ContractorStatusStatementDocument> _cSSDocuments;
    public IReadOnlyList<ContractorStatusStatementDocument> ContractorStatusStatementDocuments => _cSSDocuments;
    private ContractorStatusStatementPayment()
    {
        _cSSDocuments = [];
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}