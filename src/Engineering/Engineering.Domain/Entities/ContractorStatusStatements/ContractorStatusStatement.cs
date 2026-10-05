using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

[Description(CSSCmts.ContractorStatusStatement)]
public class ContractorStatusStatement : AuditableEntity<ContractorStatusStatement>
{
    [Description(GlobalCmts.Type)]
    public CSSType Type { get; private set; } = CSSType.System;
    [Description(GlobalCmts.Code)]
    public string Code { get; private set; }
    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }
    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }
    [Description(GlobalCmts.Status)]
    public CSSStatus Status { get; private set; } = CSSStatus.New;
    [Description(CSSCmts.MultiPayment)]
    public bool MultiPayment { get; private set; } = false;
    [Description(GlobalCmts.IsLast)]
    public bool IsLast { get; private set; } = false;
    [Description(GlobalCmts.ContractorId)]
    public long? ContractorId { get; private set; }
    [Description(GlobalCmts.CurrencyId)]
    public long? CurrencyId { get; private set; }

    [Description(CSSCmts.ServicedAmount)]
    public decimal ServicedAmount { get; private set; } = 0;
    [Description(CSSCmts.FixedAmount)]
    public decimal FixedAmount { get; private set; } = 0;

    [Description(CSSCmts.ProductsAmount)]
    public decimal ProductsAmount { get; private set; } = 0;

    [Description(CSSCmts.ForContractorsProductsAmount)]
    public decimal ForContractorProductsAmount { get; private set; } = 0;

    [Description(CSSCmts.RewardsAmount)]
    public decimal RewardsAmount { get; private set; } = 0;

    [Description(CSSCmts.CostOversAmount)]
    public decimal CostOversAmount { get; private set; } = 0;

    [Description(CSSCmts.TotalAdvancePaymentAmount)]
    public decimal TotalAdvancePaymentAmount { get; private set; }

    [Description(CSSCmts.FinesAmount)]
    public decimal FinesAmount { get; private set; } = 0;
    [Description(CSSCmts.DiscountPrice)]
    public decimal DiscountPrice { get; private set; } = 0;

    [Description(CSSCmts.FinalTotalAmount)]
    public decimal FinalTotalAmount { get; private set; }
    [Description(CSSCmts.PaymentedAmount)]
    public decimal PaymentedAmount { get; private set; } = 0;
    [Description(CSSCmts.CanPayableAmount)]
    public decimal CanPayableAmount { get; private set; } = 0;
    [Description(CSSCmts.PayableAmount)]
    public decimal PayableAmount { get; private set; } = 0;
    [Description(CSSCmts.RemainingAmount)]
    public decimal RemainingAmount { get; private set; } = 0;

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(GlobalCmts.LastDescription)]
    public string? LastDescription { get; private set; }
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(CSSCmts.TotalPercentageDoingJobWell)]
    public decimal TotalPercentageDoingJobWell { get; private set; }

    [Description(CSSCmts.TotalDoingJobWellAmount)]
    public decimal TotalDoingJobWellAmount { get; private set; }

    [Description(CSSCmts.TotalPercentageAdvancePayment)]
    public decimal TotalPercentageAdvancePayment { get; private set; }

    [Description(CSSCmts.TotalDailyLatenessPenalty)]
    public decimal TotalDailyLatenessPenalty { get; private set; }

    [Description(CSSCmts.TotalWorkDonePercent)]
    public decimal TotalWorkDonePercent { get; private set; }

    [Description(CSSCmts.TotalWorkDeliveryPercent)]
    public decimal TotalWorkDeliveryPercent { get; private set; }

    [Description(CSSCmts.TotalWorkCompletionPercent)]
    public decimal TotalWorkCompletionPercent { get; private set; }

    [Description(CSSCmts.ThirdPartiesAmount)]
    public decimal? ThirdPartiesAmount { get; private set; } = 0;

    [Description(CSSCmts.ProjectManagerApprovalAmount)]
    public decimal? ProjectManagerApprovalAmount { get; private set; } = 0;

    [Description(CSSCmts.ManagementApprovalAmount)]
    public decimal? ManagementApprovalAmount { get; private set; } = 0;

    [Description(CSSCmts.CreatorConfirmedAmount)]
    public decimal? CreatorConfirmedAmount { get; private set; } = 0;

    [Description(CSSCmts.ProjectManagerConfirmedAmount)]
    public decimal? ProjectManagerConfirmedAmount { get; private set; } = 0;

    [Description(CSSCmts.ManagementConfirmedAmount)]
    public decimal? ManagementConfirmedAmount { get; private set; } = 0;

    [Description(CSSCmts.PrimaryManagerConfirmedAmount)]
    public decimal? PrimaryManagerConfirmedAmount { get; private set; } = 0;

    [Description(CSSCmts.PrimaryManagerConfirmed)]
    public bool PrimaryManagerConfirmed { get; private set; } = false;

    [Description(CSSCmts.FinalManagerConfirmedAmount)]
    public decimal? FinalManagerConfirmedAmount { get; private set; } = 0;

    [Description(CSSCmts.FinalManagerConfirmed)]
    public bool FinalManagerConfirmed { get; private set; } = false;

    [Description(CSSCmts.ConfirmedPrice)]
    public decimal? ConfirmedPrice { get; private set; } = 0;

    [Description(CSSCmts.ConfirmedBankAccountId)]
    public long? ConfirmedBankAccountId { get; private set; }

    [Description(CSSCmts.ConfirmedPaymentDate)]
    public DateTime? ConfirmedPaymentDate { get; private set; }

    [Description(CSSCmts.PaymentOrderId)]
    public long? PaymentOrderId { get; private set; }

    [Description(CSSCmts.ManagmentDescription)]
    public string? ManagmentDescription { get; private set; }

    [Description(CSSCmts.ProjectManagmentDescription)]
    public string? ProjectManagmentDescription { get; private set; }

    [Description(CSSCmts.PrimaryManagerDescription)]
    public string? PrimaryManagerDescription { get; private set; }

    [Description(CSSCmts.FinalManagerDescription)]
    public string? FinalManagerDescription { get; private set; }


    [Description(CSSCmts.ConfirmedDescription)]
    public string? ConfirmedDescription { get; private set; }


    [Description(GlobalCmts.Project)]
    public Project? Project { get; set; }
    public long? ProjectId { get; set; }

    [Description(GlobalCmts.Season)]
    public Season? Season { get; set; }
    public long? SeasonId { get; set; }

    public ContractorStatusStatement(
        Project project,
        CSSType type,
        string code,
        DateTime startDate,
        DateTime endDate,
        string? description,
        string? lastDescription,
        long contractorId,
        decimal? creatorConfirmedAmount,
        long? currencyId,
        List<string>? documentUrls,
        long? companyId) : this()
    {
        SetContractorId(contractorId);
        SetProject(project);
        SetCode(code);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetCurrencyId(currencyId);
        SetCreatorConfirmedAmount(creatorConfirmedAmount);
        SetCompanyId(companyId);
        SetLastDescription(lastDescription);
        SetType(type);

        if (documentUrls != null && documentUrls.Any())
            AddDocuments(documentUrls, false);

        AddHistory();
    }

    public void ConfigPayment(
        decimal amount,
        string? description,
        List<string>? urls,
        long? paymentOrderId)
    {
        var payment = _cssPayments.FirstOrDefault(x => x.PaymentOrderId is null && x.Status == CSSPaymentStatus.Unpaid);
        if (payment is null)
        {
            payment = new ContractorStatusStatementPayment(
                this,
                FinalTotalAmount,
                PaymentedAmount,
                CanPayableAmount,
                PayableAmount,
                Description);
            _cssPayments.Add(payment);
        }

        if (Status == CSSStatus.New ||
            Status == CSSStatus.ProjectManagerResend)
        {
            _cssPayments.Where(x => x.PaymentOrderId is null && x.Status == CSSPaymentStatus.Unpaid)!
                .FirstOrDefault()!.Update(FinalTotalAmount, CanPayableAmount, PayableAmount, Description);
            return;
        }

        ApplyPaymentAction(payment, amount, description, urls, paymentOrderId);
    }

    private void ApplyPaymentAction(
        ContractorStatusStatementPayment payment,
        decimal amount,
        string? description,
        List<string>? urls,
        long? paymentOrderId)
    {
        switch (Status)
        {
            case CSSStatus.ProjectManagerConfirmed:
                payment.SetProjectManager(amount, description);
                break;

            case CSSStatus.ManagementConfirmed:
                payment.SetManagement(amount, description);
                break;

            case CSSStatus.PrimaryManagerConfirmed:
                payment.SetPrimaryManager(amount, description);
                break;

            case CSSStatus.FinalManagerConfirmed:
                payment.SetFinalManager(amount, description);
                break;

            case CSSStatus.PaymentConfirmation:
                if (!paymentOrderId.HasValue)
                    throw new ArgumentNullException(nameof(paymentOrderId), "PaymentOrderId is required for payment confirmation.");
                payment.SetPaymentOrder(paymentOrderId.Value, amount, urls, description);
                break;
        }
    }

    #region Commands

    public void SetMultiPayment(bool? value)
    {
        if (value is not null)
            MultiPayment = value.Value;
    }

    public void SetServicedAmount(decimal? value)
    {
        ServicedAmount = value ?? 0;
    }

    public void SetCode(string? value)
    {
        Code = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetCurrencyId(long? value)
    {
        CurrencyId = value;
    }

    public void SetType(CSSType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetContractorId(long value)
    {
        ContractorId = Guard.Against.Null(value, nameof(value));
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetRewardsAmount(decimal? value)
    {
        RewardsAmount = value ?? 0;
    }

    public void SetFinesAmount(decimal? value)
    {
        FinesAmount = value ?? 0;
    }

    public void SetFixedAmount(decimal? value)
    {
        FixedAmount = value ?? 0;
    }

    public void SetProductsAmount(decimal? value)
    {
        ProductsAmount = value ?? 0;
    }

    public void SetForContractorProductsAmount(decimal? value)
    {
        ForContractorProductsAmount = value ?? 0;
    }

    public void SetCostOversAmount(decimal? value)
    {
        CostOversAmount = value ?? 0;
    }

    public void SetDiscountPrice(decimal value)
    {
        DiscountPrice = value;
    }

    public void SetFinalTotalAmount(decimal oldFixedAmount, decimal oldServicedAmount)
    {
        FinalTotalAmount = (oldFixedAmount + oldServicedAmount + CostOversAmount + TotalAdvancePaymentAmount + ProductsAmount + RewardsAmount - (FinesAmount + DiscountPrice + ForContractorProductsAmount));
    }

    public void SetPaymentedAmount(decimal? value)
    {
        PaymentedAmount = value ?? 0;
    }

    public void SetCanPayableAmount()
    {
        CanPayableAmount = FinalTotalAmount - PaymentedAmount;
    }

    public void SetPayableAmount(decimal confirmedAmount)
    {
        PayableAmount = confirmedAmount;
    }

    public void SetRemainingAmount()
    {
        RemainingAmount = CanPayableAmount - PayableAmount;
    }

    public void SetRemainingAmount(decimal userAonfirm)
    {
        RemainingAmount = CanPayableAmount - userAonfirm;
    }




    public void SetCreatorConfirmedAmount(decimal? value)
    {
        CreatorConfirmedAmount = value;
    }

    public void SetCreatorConfirmedAmount()
    {
        CreatorConfirmedAmount = ThirdPartiesAmount + CostOversAmount + ProductsAmount + RewardsAmount - (FinesAmount + PaymentedAmount + DiscountPrice);
    }

    public void SetTotalPercentageDoingJobWell(decimal value)
    {
        TotalPercentageDoingJobWell = value;
    }

    public void SetTotalDoingJobWellAmount(decimal value)
    {
        TotalDoingJobWellAmount = value;
    }

    public void SetProjectManagerConfirmedAmount(decimal? value)
    {
        if (value is null || value == 0)
            value = CreatorConfirmedAmount;

        ProjectManagerConfirmedAmount = value;
    }

    public void SetManagementConfirmedAmount(decimal? value)
    {
        if (value is null || value == 0)
            value = ProjectManagerConfirmedAmount;

        ManagementConfirmedAmount = value;
    }

    public void SetTotalPercentageAdvancePayment(decimal value)
    {
        TotalPercentageAdvancePayment = value;
    }

    public void SetTotalAdvancePaymentAmount(decimal value)
    {
        TotalAdvancePaymentAmount = value;
    }

    public void SetTotalDailyLatenessPenalty(decimal value)
    {
        TotalDailyLatenessPenalty = value;
    }

    public void SetTotalWorkDonePercent(decimal value)
    {
        TotalWorkDonePercent = value;
    }

    public void SetTotalWorkDeliveryPercent(decimal value)
    {
        TotalWorkDeliveryPercent = value;
    }

    public void SetTotalWorkCompletionPercent(decimal value)
    {
        TotalWorkCompletionPercent = value;
    }

    public void SetThirdPartiesAmount(decimal? value)
    {
        ThirdPartiesAmount = value ?? 0;
    }

    public void SetManagementApprovalAmount(decimal? value)
    {
        ManagementApprovalAmount = value ?? 0;
    }

    public void ResetManagementApprovalAmount()
    {
        ManagementApprovalAmount = ThirdPartiesAmount;
    }

    public void SetProjectManagerApprovalAmount(decimal? value)
    {
        ProjectManagerApprovalAmount = value ?? 0;
    }

    public void ResetProjectManagerApprovalAmount()
    {
        ProjectManagerApprovalAmount = ThirdPartiesAmount;
    }

    public void SetConfirmedPrice(decimal? value)
    {
        ConfirmedPrice = value ?? 0;
    }

    public void SetConfirmedBankAccountId(long? value)
    {
        ConfirmedBankAccountId = value;
        _cssPayments.Where(x => x.PaymentOrderId is null && x.Status == CSSPaymentStatus.Unpaid)!
            .FirstOrDefault()!.SetConfirmedBankAccountId(value!.Value);
    }

    public void SetConfirmedPaymentDate(DateTime? value)
    {
        ConfirmedPaymentDate = value;
    }

    public void SetConfirmedDescription(string? value)
    {
        ConfirmedDescription = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    public void SetProjectManagmentDescription(string? value)
    {
        ProjectManagmentDescription = value;
    }

    public void SetManagmentDescription(string? value)
    {
        ManagmentDescription = value;
    }

    public void SetPrimaryManagerDescription(string? value)
    {
        PrimaryManagerDescription = value;
    }
    public void SetPrimaryManagerConfirmed()
    {
        PrimaryManagerConfirmed = true;
    }
    public void SetPrimaryManagerNotConfirmed()
    {
        PrimaryManagerConfirmed = false;
    }

    public void SetFinalManagerDescription(string? value)
    {
        FinalManagerDescription = value;
    }
    public void SetFinalManagerConfirmed()
    {
        FinalManagerConfirmed = true;
    }
    public void SetFinalManagerNotConfirmed()
    {
        FinalManagerConfirmed = false;
    }

    public void SetSeason(Season? value)
    {
        Season = value;
    }

    public void SetPrimaryManagerConfirmedAmount(decimal? value)
    {
        if (value is null || value == 0)
            value = ManagementConfirmedAmount;
        PrimaryManagerConfirmedAmount = value;
    }

    public void SetFinalManagerConfirmedAmount(decimal? value)
    {
        if (value is null || value == 0)
            value = ManagementConfirmedAmount;
        FinalManagerConfirmedAmount = value;
    }
    public void ChangeStatus(CSSStatus status, string? description)
    {
        ArgumentNullException.ThrowIfNull(status);

        LastDescription = description;

        if (status == CSSStatus.PrimaryManagerConfirmed || status == CSSStatus.FinalManagerConfirmed)
            this.AddManagmentsHistory(status);

        if (status != CSSStatus.PrimaryManagerConfirmed && status != CSSStatus.FinalManagerConfirmed)
        {
            Status = status;
            this.AddHistory();
        }


        if (status == CSSStatus.Paid)
            SetIsLast();
    }

    public void AddHistory()
    {
        _cSSHistories.Add(new ContractorStatusStatementHistory(
            this,
            Code,
            StartDate,
            EndDate,
            Status,
            FinalTotalAmount,
            TotalPercentageDoingJobWell,
            TotalDoingJobWellAmount,
            TotalPercentageAdvancePayment,
            TotalAdvancePaymentAmount,
            TotalDailyLatenessPenalty,
            TotalWorkDonePercent,
            TotalWorkDeliveryPercent,
            TotalWorkCompletionPercent,
            ProductsAmount,
            FinesAmount,
            RewardsAmount,
            CostOversAmount,
            ThirdPartiesAmount,
            PaymentedAmount,
            LastDescription));
    }

    public void AddManagmentsHistory(CSSStatus status)
    {
        _cSSHistories.Add(new ContractorStatusStatementHistory(
            this,
            Code,
            StartDate,
            EndDate,
            status,
            FinalTotalAmount,
            TotalPercentageDoingJobWell,
            TotalDoingJobWellAmount,
            TotalPercentageAdvancePayment,
            TotalAdvancePaymentAmount,
            TotalDailyLatenessPenalty,
            TotalWorkDonePercent,
            TotalWorkDeliveryPercent,
            TotalWorkCompletionPercent,
            ProductsAmount,
            FinesAmount,
            RewardsAmount,
            CostOversAmount,
            ThirdPartiesAmount,
            PaymentedAmount,
            LastDescription));
    }

    public void SetIsLast()
    {
        IsLast = true;
    }

    public void SetIsNotLast()
    {
        IsLast = false;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;

        if (_cSSDocuments.Any())
            _cSSDocuments.ForEach(c => c.SoftDelete());

        if (_cSSProducts.Any())
            _cSSProducts.ForEach(c => c.SoftDelete());

        if (_cSSHistories.Any())
            _cSSHistories.ForEach(c => c.SoftDelete());

        if (_cSSFines.Any())
            _cSSFines.ForEach(c => c.SoftDelete());

        if (_cSSRewards.Any())
            _cSSRewards.ForEach(c => c.SoftDelete());

        if (_cSSDiscounts.Any())
            _cSSDiscounts.ForEach(c => c.SoftDelete());

        if (_cSSCostOvers.Any())
            _cSSCostOvers.ForEach(c => c.SoftDelete());

        if (_cssPayments.Any())
            _cssPayments.ForEach(c => c.SoftDelete());

        if (_cSSDetails.Any())
            foreach (var item in _cSSDetails)
            {
                foreach (var service in item.ContractorStatusStatementServices)
                {
                    foreach (var party in service.ContractorStatusStatementServiceThirdParties)
                        party.SoftDelete();

                    foreach (var dail in service.ContractorStatusStatementServiceDailies)
                        dail.SoftDelete();

                    service.SoftDelete();
                }

                item.SoftDelete();
            }

    }

    public void AddContractorStatusStatementFines(
        RequestReward requestReward,
        DateTime registrationDate,
        decimal confirmedPrice)
    {
        _cSSFines.Add(ContractorStatusStatementFine.Create(
            this,
            requestReward,
            registrationDate,
            confirmedPrice));
    }

    public void AddContractorStatusStatementRewards(
        RequestReward requestReward,
        DateTime registrationDate,
        decimal confirmedPrice)
    {
        _cSSRewards.Add(ContractorStatusStatementReward.Create(
            this,
            requestReward,
            registrationDate,
            confirmedPrice));
    }

    public void AddDocuments(List<string>? urls, bool justAdd)
    {
        if (urls != null && urls.Count > 0)
        {
            if (justAdd)
            {
                foreach (var url in urls)
                    _cSSDocuments.Add(new ContractorStatusStatementDocument(this, null, url));
            }
            else
            {
                if (_cSSDocuments.Any())
                    _cSSDocuments.Where(x => x.ContractorStatusStatementPayment is null)
                        .ToList().ForEach(c => c.SoftDelete());

                foreach (var url in urls)
                    _cSSDocuments.Add(new ContractorStatusStatementDocument(this, null, url));
            }
        }
        else
        {
            if (_cSSDocuments.Any())
                _cSSDocuments.Where(x => x.ContractorStatusStatementPayment is null)
                    .ToList().ForEach(c => c.SoftDelete());
        }
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<ContractorStatusStatement> _cSSs;
    public IReadOnlyList<ContractorStatusStatement> ContractorStatusStatements => _cSSs;

    private List<ContractorStatusStatementDocument> _cSSDocuments;
    public IReadOnlyList<ContractorStatusStatementDocument> ContractorStatusStatementDocuments => _cSSDocuments;

    private List<ContractorStatusStatementProduct> _cSSProducts;
    public IReadOnlyList<ContractorStatusStatementProduct> ContractorStatusStatementProducts => _cSSProducts;

    private List<ContractorStatusStatementHistory> _cSSHistories;
    public IReadOnlyList<ContractorStatusStatementHistory> ContractorStatusStatementHistories => _cSSHistories;

    private List<ContractorStatusStatementDetail> _cSSDetails;
    public IReadOnlyList<ContractorStatusStatementDetail> ContractorStatusStatementDetails => _cSSDetails;

    private List<ContractorStatusStatementFine> _cSSFines;
    public IReadOnlyList<ContractorStatusStatementFine> ContractorStatusStatementFines => _cSSFines;

    private List<ContractorStatusStatementReward> _cSSRewards;
    public IReadOnlyList<ContractorStatusStatementReward> ContractorStatusStatementRewards => _cSSRewards;

    private List<ContractorStatusStatementDiscount> _cSSDiscounts;
    public IReadOnlyList<ContractorStatusStatementDiscount> ContractorStatusStatementDiscounts => _cSSDiscounts;

    private List<ContractorStatusStatementCostOver> _cSSCostOvers;
    public IReadOnlyList<ContractorStatusStatementCostOver> ContractorStatusStatementCostOvers => _cSSCostOvers;

    private List<ContractorContractHeaderVersion> _contractorContractHeaderVersions;
    public IReadOnlyList<ContractorContractHeaderVersion> ContractorContractHeaderVersions => _contractorContractHeaderVersions;

    private List<ContractorStatusStatementPayment> _cssPayments;
    public IReadOnlyList<ContractorStatusStatementPayment> ContractorStatusStatementPayments => _cssPayments;

    private ContractorStatusStatement()
    {
        _cSSs = [];
        _cSSDocuments = [];
        _cSSProducts = [];
        _cSSHistories = [];
        _cSSDetails = [];
        _cSSFines = [];
        _cSSRewards = [];
        _cSSDiscounts = [];
        _cSSCostOvers = [];
        _contractorContractHeaderVersions = [];
        _cssPayments = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
