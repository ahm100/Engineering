using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Domain.Entities.RequestMachineryStatusStatements;

/// <summary>
/// درخواست ماشین الات
/// </summary>
public class RequestMachineryStatusStatement : AuditableEntity<RequestMachineryStatusStatement>
{
    #region Fields
    #endregion

    #region Properties

    [Description(RequestMachineryStatusStatementCmts.ContractorId)]
    public long ContractorId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.FromDate)]
    public DateTime? FromDate { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.ToDate)]
    public DateTime? ToDate { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.PaymentDate)]
    public DateTime? PaymentDate { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.TotalRequestedCount)]
    public decimal TotalRequestedCount { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.TotalFinalPrice)]
    public decimal TotalFinalPrice { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.ContractorPrice)]
    public decimal? ContractorPrice { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.Status)]
    public RequestMachineryStatusStatementStatus Status { get; private set; } = RequestMachineryStatusStatementStatus.New;

    [Description(RequestMachineryStatusStatementCmts.BankAccountId)]
    public long? BankAccountId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.IBAN)]
    public string? IBAN { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.Description)]
    public string? Description { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.PaymentOrderId)]
    public long? PaymentOrderId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.CostCategoryId)]
    public long? CostCategoryId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.CostGroupId)]
    public long? CostGroupId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.DocumentTypeId)]
    public long? DocumentTypeId { get; private set; }

    [Description(RequestMachineryStatusStatementCmts.PreferentialTypeId)]
    public long? PreferentialTypeId { get; private set; }

    [Description(GlobalCmts.Season)]
    public long? SeasonId { get; set; }
    public Season? Season { get; set; }

    #endregion

    public RequestMachineryStatusStatement(
     long contractorId,
     DateTime? fromDate,
     DateTime? toDate,
     DateTime? paymentDate,
     decimal totalRequestedCount,
     decimal totalFinalPrice,
     decimal? contractorPrice,
     long? bankAccountId,
     string? iBAN,
     string? description,
     long? costCategoryId,
     long? costGroupId,
     long? documentTypeId,
     long? preferentialTypeId,
     long? companyId) : this()
    {
        SetContractorId(contractorId);
        SetTotalRequestCount(totalRequestedCount);
        SetTotalFinalPrice(totalFinalPrice);
        SetContractorPrice(contractorPrice);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetPaymentDate(paymentDate);
        SetDescription(description);
        SetBankAccountId(bankAccountId);
        SetIBAN(iBAN);
        SetCompanyId(companyId);
        SetCostGroupId(costGroupId);
        SetDocumentTypeId(documentTypeId);
        SetPreferentialTypeId(preferentialTypeId);
        SetCostCategoryId(costCategoryId);
    }

    public void SetData(
     long contractorId,
     DateTime? fromDate,
     DateTime? toDate,
     DateTime? paymentDate,
     decimal totalRequestedCount,
     decimal totalFinalPrice,
     decimal? contractorPrice,
     long? bankAccountId,
     string? iBAN,
     string? description,
     long? costCategoryId,
     long? costGroupId,
     long? documentTypeId,
     long? preferentialTypeId,
     long? companyId)
    {
        SetContractorId(contractorId);
        SetTotalRequestCount(totalRequestedCount);
        SetTotalFinalPrice(totalFinalPrice);
        SetContractorPrice(contractorPrice);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetPaymentDate(paymentDate);
        SetDescription(description);
        SetBankAccountId(bankAccountId);
        SetIBAN(iBAN);
        SetCompanyId(companyId);
        SetCostGroupId(costGroupId);
        SetDocumentTypeId(documentTypeId);
        SetPreferentialTypeId(preferentialTypeId);
        SetCostCategoryId(costCategoryId);
    }

    public void ChangeStatus(RequestMachineryStatusStatementStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetContractorId(long value)
    {
        ContractorId = value;
    }

    public void SetCostGroupId(long? value)
    {
        CostGroupId = value;
    }

    public void SetDocumentTypeId(long? value)
    {
        DocumentTypeId = value;
    }

    public void SetPreferentialTypeId(long? value)
    {
        PreferentialTypeId = value;
    }

    public void SetCostCategoryId(long? value)
    {
        CostCategoryId = value;
    }

    public void SetPaymentDate(DateTime? value)
    {
        PaymentDate = value;
    }

    public void SetBankAccountId(long? value)
    {
        BankAccountId = value;
    }

    public void SetPaymentOrderId(long? value)
    {
        PaymentOrderId = value;
    }

    public void SetTotalRequestCount(decimal value)
    {
        TotalRequestedCount = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetTotalFinalPrice(decimal value)
    {
        TotalFinalPrice = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetContractorPrice(decimal? value)
    {
        ContractorPrice = value;
    }

    public void SetFromDate(DateTime? value)
    {
        FromDate = value;
    }

    public void SetToDate(DateTime? value)
    {
        ToDate = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetIBAN(string? value)
    {
        IBAN = value;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetSeason(Season? value)
    {
        Season = value;
        SeasonId = value?.Id;
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<RequestMachineryStatusStatementDetail> _requestMachineryStatusStatementDetails;
    public IReadOnlyList<RequestMachineryStatusStatementDetail> RequestMachineryStatusStatementDetails => _requestMachineryStatusStatementDetails;

    private RequestMachineryStatusStatement()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _requestMachineryStatusStatementDetails = [];
    }

    #endregion
}