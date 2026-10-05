using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.CostCenters;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.ContractorContracts;
[Description(CCCmts.ContractorContractHeader)]
public class ContractorContractHeader : AuditableEntity<ContractorContractHeader, long>
{
    [Description(GlobalCmts.ContractorId)]
    public long ContractorId { get; private set; }
    [Description(CCCmts.ContractorContractStatus)]
    public ContractorContractStatus Status { get; private set; } = ContractorContractStatus.New;
    [Description(CCCmts.CurrencyId)]
    public long CurrencyId { get; private set; }
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.CostCenterId)]
    public long? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }



    [NotMapped]
    [Description(CCCmts.MinStartDate)]
    public DateTime? StartDate => _contractorContracts.Min(x => x.StartDate);
    [NotMapped]
    [Description(CCCmts.MaxEndDate)]
    public DateTime? EndDate => _contractorContracts.Max(x => x.EndDate);
    [NotMapped]
    [Description(CCCmts.FinalTotalAmount)]
    public decimal? FinalTotalAmount => !_contractorContracts.Any() ? 0 : _contractorContracts.Sum(x => x.TotalAmount);
    [NotMapped]
    [Description(CCCmts.TotalPercentageDoingJobWell)]
    public decimal? TotalPercentageDoingJobWell => !_contractorContracts.Any() ? 0 : (_contractorContracts.Sum(x => x.PercentageDoingJobWell) / _contractorContracts.Count);
    [NotMapped]
    [Description(CCCmts.TotalDoingJobWellAmount)]
    public decimal? TotalDoingJobWellAmount => !_contractorContracts.Any() ? 0 : (_contractorContracts.Sum(x => x.DoingJobWellAmount) / _contractorContracts.Count);
    [NotMapped]
    [Description(CCCmts.TotalAdvancePaymentAmount)]
    public decimal? TotalAdvancePaymentAmount => !_contractorContracts.Any() ? 0 : _contractorContracts.Sum(x => x.AdvancePaymentAmount);
    [NotMapped]
    [Description(CCCmts.TotalPercentageAdvancePayment)]
    public decimal? TotalPercentageAdvancePayment => TotalAdvancePaymentAmount == 0 || FinalTotalAmount == 0 ? 0 : (TotalAdvancePaymentAmount / FinalTotalAmount) * 100;
    [NotMapped]
    [Description(CCCmts.TotalDailyLatenessPenalty)]
    public decimal? TotalDailyLatenessPenalty => !_contractorContracts.Any() ? 0 : (_contractorContracts.Sum(x => x.DailyLatenessPenalty) / _contractorContracts.Count);
    [NotMapped]
    [Description(CCCmts.TotalWorkDonePercent)]
    public decimal? TotalWorkDonePercent => !_contractorContracts.Any() ? 0 : (_contractorContracts.Sum(x => x.WorkDonePercent) / _contractorContracts.Count);
    [NotMapped]
    [Description(CCCmts.TotalWorkDeliveryPercent)]
    public decimal? TotalWorkDeliveryPercent => !_contractorContracts.Any() ? 0 : (_contractorContracts.Sum(x => x.WorkDeliveryPercent) / _contractorContracts.Count);
    [NotMapped]
    [Description(CCCmts.TotalWorkCompletionPercent)]
    public decimal? TotalWorkCompletionPercent => !_contractorContracts.Any() ? 0 : (_contractorContracts.Sum(x => x.WorkCompletionPercent) / _contractorContracts.Count);


    #region Constructors

    public ContractorContractHeader(
        CostCenter costCenter,
        long contractorId,
        long currencyId,
        string? description,
        List<string>? documentUrls,
        long? companyId) : this()
    {
        SetCostCenter(costCenter);
        SetContractorId(contractorId);
        SetCurrencyId(currencyId);
        SetDescription(description);
        SetCompanyId(companyId);
        SetNewStatus(ContractorContractStatus.New);
        if (documentUrls != null && documentUrls.Any())
            AddDocuments(documentUrls, true);
    }

    #endregion

    #region Commands

    public void SetContractorId(long value)
    {
        ContractorId = Guard.Against.Null(value, nameof(value));
    }

    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetStatus(ContractorContractStatus value)
    {
        Status = value;
        if (value != ContractorContractStatus.ProjectManagerConfirmed)
            this.AddHistory();
    }

    public void SetNewStatus(ContractorContractStatus value)
    {
        Status = value;
    }

    public void SetCurrencyId(long value)
    {
        CurrencyId = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void AddHistory()
    {
        _contractorContractHeaderHistories.Add(ContractorContractHeaderHistory.Create(
            this,
            ContractorId,
            Status,
            CurrencyId,
            Description,
            StartDate ?? DateTime.Now,
            EndDate ?? DateTime.Now,
            FinalTotalAmount,
            TotalPercentageDoingJobWell,
            TotalDoingJobWellAmount,
            TotalPercentageAdvancePayment,
            TotalAdvancePaymentAmount,
            TotalDailyLatenessPenalty,
            TotalWorkDonePercent,
            TotalWorkDeliveryPercent,
            TotalWorkCompletionPercent
            ));
    }

    public void AddManagerHistory(string? description)
    {
        _contractorContractHeaderHistories.Add(ContractorContractHeaderHistory.Create(
            this,
            ContractorId,
            Status,
            CurrencyId,
            description,
            StartDate,
            EndDate,
            FinalTotalAmount,
            TotalPercentageDoingJobWell,
            TotalDoingJobWellAmount,
            TotalPercentageAdvancePayment,
            TotalAdvancePaymentAmount,
            TotalDailyLatenessPenalty,
            TotalWorkDonePercent,
            TotalWorkDeliveryPercent,
            TotalWorkCompletionPercent
            ));
    }

    public void AddDocuments(List<string>? urls, bool justAdd)
    {
        if (justAdd == false)
        {
            if (urls != null && urls.Count > 0)
            {
                if (_contractorContractHeaderDocuments.Any())
                    _contractorContractHeaderDocuments.ForEach(c => c.SoftDelete());
                foreach (var url in urls)
                    _contractorContractHeaderDocuments.Add(ContractorContractHeaderDocument.Create(url, this));
            }
            else
            {
                if (_contractorContractHeaderDocuments.Any())
                    _contractorContractHeaderDocuments.ForEach(c => c.SoftDelete());
            }
        }
        else
        {
            if (urls is not null)
            {
                foreach (var url in urls)
                    _contractorContractHeaderDocuments.Add(ContractorContractHeaderDocument.Create(url, this));
            }
        }
    }


    #endregion




    [Description(GlobalCmts.ContractorContract)]
    private List<ContractorContract> _contractorContracts;
    public IReadOnlyList<ContractorContract> ContractorContracts => _contractorContracts;
    [Description(CCCmts.ContractorContractHeaderDocument)]
    private List<ContractorContractHeaderDocument> _contractorContractHeaderDocuments;
    public IReadOnlyList<ContractorContractHeaderDocument> ContractorContractHeaderDocuments => _contractorContractHeaderDocuments;
    [Description(CCCmts.ContractorContractHeaderHistory)]
    private List<ContractorContractHeaderHistory> _contractorContractHeaderHistories;
    public IReadOnlyList<ContractorContractHeaderHistory> ContractorContractHeaderHistories => _contractorContractHeaderHistories;

    [Description(CCCmts.ContractorContractHeaderVersion)]
    private List<ContractorContractHeaderVersion> _contractorContractHeaderVersion;
    public IReadOnlyList<ContractorContractHeaderVersion> ContractorContractHeaderVersions => _contractorContractHeaderVersion;

    private ContractorContractHeader()
    {
        _contractorContracts = [];
        _contractorContractHeaderDocuments = [];
        _contractorContractHeaderHistories = [];
        _contractorContractHeaderVersion = [];
    }
}
