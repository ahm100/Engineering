using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerContract)]
public class EmployerContract : ActivateEntity<EmployerContract, long>
{
    [Description(EContractCmts.IsFirst)]
    public bool IsFirst { get; private set; }
    [Description(EContractCmts.ContractStatus)]
    public EContractStatus Status { get; private set; }
    [Description(GlobalCmts.Code)]
    public string? Code { get; private set; }
    [Description(EContractCmts.CurrencyRate)]
    public decimal? CurrencyRate { get; private set; }
    [Description(GlobalCmts.StartDate)]
    public DateTime? StartDate { get; private set; }
    [Description(GlobalCmts.EndDate)]
    public DateTime? EndDate { get; private set; }
    [Description(EContractCmts.TotalAmount)]
    public decimal TotalAmount { get; private set; } = 0;
    [Description(EContractCmts.AdvancePayment)]
    public decimal AdvancePayment { get; private set; } = 0;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(GlobalCmts.LastDescription)]
    public string? LastDescription { get; private set; }

    [Description(EContractCmts.EmployerContractHead)]
    public long EmployerContractHeadId { get; private set; }
    public EmployerContractHead EmployerContractHead { get; private set; }

    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; set; }
    public Project Project { get; set; }


    public EmployerContract(
        EmployerContractHead head,
        Project project,
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        string? code,
        bool isActive) : this()
    {
        SetEmployerContractHead(head);
        SetProject(project);
        SetIsFirst(head);
        SetStatus(EContractStatus.New);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetCode(code);
        SetDescription(description);

        IsActive = isActive;
    }

    #region Methods 

    public void Update(
        DateTime? startDate,
        DateTime? endDate,
        string? code,
        string? description)
    {
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetCode(code);
        SetAdvancePayment(0);
        SetDescription(description);
    }

    public void UpdateCost(
        decimal? currencyRate,
        decimal advancePayment,
        string? description)
    {
        SetCurrencyRate(currencyRate);
        SetAdvancePayment(advancePayment);
        SetDescription(description);
    }

    public void UpdateStatus(EContractStatus value, string? desc)
    {
        if (!EContractStatusRules.AllowManager.Contains(value) && !EContractStatusRules.AllowEmployer.Contains(value))
        {
            SetStatus(value);
            SetLastDescription(desc);
            AddHistory();
        }
        else if (EContractStatusRules.AllowManager.Contains(value))
            AddManagmentsHistory(value);
    }

    public void AddManagmentsHistory(EContractStatus status)
    {
        _employerContractHistory.Add(new EmployerContractHistory(
                this,
                CurrencyRate,
                status,
                StartDate,
                EndDate,
                AdvancePayment,
                Code,
                Description,
                TotalAmount,
                IsFirst,
                IsActive));
    }

    public void AddHistory()
    {
        _employerContractHistory.Add(new EmployerContractHistory(
                this,
                CurrencyRate,
                Status,
                StartDate,
                EndDate,
                AdvancePayment,
                Code,
                Description,
                TotalAmount,
                IsFirst,
                IsActive));
    }

    public void AddHistory(EContractStatus status)
    {
        _employerContractHistory.Add(new EmployerContractHistory(
                this,
                CurrencyRate,
                status,
                StartDate,
                EndDate,
                AdvancePayment,
                Code,
                Description,
                TotalAmount,
                IsFirst,
                IsActive));
    }

    public void SetEmployerContractHead(EmployerContractHead value)
    {
        EmployerContractHead = Guard.Against.Null(value, nameof(value));
        EmployerContractHeadId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCode(string? value)
    {
        Code = value;
    }

    private void SetIsFirst(EmployerContractHead value)
    {
        IsFirst = !value.EmployerContracts.Any(x => x != this);
    }

    private void SetStatus(EContractStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    private void SetCurrencyRate(decimal? value)
    {
        CurrencyRate = value;
    }

    private void SetStartDate(DateTime? value)
    {
        StartDate = value;
    }

    private void SetDescription(string? value)
    {
        Description = value;
    }

    private void SetLastDescription(string? value)
    {
        LastDescription = value;
    }

    private void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }

    private void SetTotalAmount(decimal value)
    {
        TotalAmount = Guard.Against.Null(value, nameof(value));
    }

    private void SetAdvancePayment(decimal value)
    {
        AdvancePayment = Guard.Against.Null(value, nameof(value));
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Description(EContractCmts.EmployerDoc)]
    private List<EmployerDoc> _employerDocs;
    public IReadOnlyList<EmployerDoc> EmployerDocs => _employerDocs;

    [Description(EContractCmts.EmployerConsideration)]
    private List<EmployerConsideration> _employerConsiderations;
    public IReadOnlyList<EmployerConsideration> EmployerConsiderations => _employerConsiderations;

    [Description(EContractCmts.ContractCostOver)]
    private List<EmployerCostOver> _contractCostOver;
    public IReadOnlyList<EmployerCostOver> EmployerCostOvers => _contractCostOver;

    [Description(EContractCmts.EmployerOperation)]
    private List<EmployerOperation> _employerOperations;
    public IReadOnlyList<EmployerOperation> EmployerOperations => _employerOperations;

    [Description(EContractCmts.EmployerStatusStatement)]
    private List<EmployerStatusStatement> _employerStatusStatement;
    public IReadOnlyList<EmployerStatusStatement> EmployerStatusStatements => _employerStatusStatement;

    [Description(EContractCmts.EmployerContractHistory)]
    private List<EmployerContractHistory> _employerContractHistory;
    public IReadOnlyList<EmployerContractHistory> EmployerContractHistory => _employerContractHistory;

    private EmployerContract()
    {
        _employerDocs = [];
        _employerConsiderations = [];
        _employerOperations = [];
        _contractCostOver = [];
        _employerStatusStatement = [];
        _employerContractHistory = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
