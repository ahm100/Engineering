using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.EmployerStatusStatements;

/// <summary>
/// صورت وضعیت کارفرما
/// </summary>
public class EmployerStatusStatement : AuditableEntity<EmployerStatusStatement>
{
    /// <summary>
    /// کد صورت وضعیت
    /// </summary>
    public string? StatusStatementCode { get; private set; }
    /// <summary>
    /// وضعیت ارسالی
    /// </summary>
    public EmployerStatusStatementStatus Status { get; private set; }
    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public DateTime StartDate { get; private set; }
    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public DateTime EndDate { get; private set; }
    /// <summary>
    /// درصد کار انجام شده
    /// </summary>
    public decimal PercentageOfWorkDone { get; private set; }
    /// <summary>
    /// مبلغ محاسبه شده
    /// </summary>
    public decimal CalculatedAmount { get; private set; }
    /// <summary>
    /// حجم صورت وضعیت
    /// </summary>
    public decimal StatusStatementVolume { get; private set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string? Description { get; private set; } = string.Empty;
    /// <summary>
    /// کمپانی
    /// </summary>
    public long? CompanyId { get; private set; }

    public EmployerStatusStatement? LastEmployerStatusStatement { get; private set; }
    public EmployerContract EmployerContract { get; set; }
    public Project Project { get; set; }

    public EmployerStatusStatement(
        EmployerContract? employerContract,
        Project project,
        EmployerStatusStatement? lastEmployerStatusStatement,
        DateTime startDate,
        DateTime endDate,
        decimal percentageOfWorkDone,
        decimal calculatedAmount,
        decimal statusStatementVolume,
        string? statusStatementCode,
        string? description,
        long? companyId) : this()
    {
        EmployerContract = Guard.Against.Null(employerContract, nameof(employerContract));
        Project = Guard.Against.Null(project, nameof(project));
        LastEmployerStatusStatement = lastEmployerStatusStatement;
        StartDate = Guard.Against.Null(startDate, nameof(startDate));
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        PercentageOfWorkDone = Guard.Against.Null(percentageOfWorkDone, nameof(percentageOfWorkDone));
        StatusStatementVolume = Guard.Against.Null(statusStatementVolume, nameof(statusStatementVolume));
        CalculatedAmount = Guard.Against.Null(calculatedAmount, nameof(calculatedAmount));
        Description = description;
        StatusStatementCode = statusStatementCode;
        CompanyId = companyId;
        Status = EmployerStatusStatementStatus.Registration;

        AddHistory();
    }

    #region Set data 

    public void SetStatusStatementCode(string? value)
    {
        StatusStatementCode = value;
    }

    public void SetStatus(EmployerStatusStatementStatus value, string? description)
    {
        Status = Guard.Against.Null(value, nameof(value));
        Description = description;
    }

    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetPercentageOfWorkDone(decimal value)
    {
        PercentageOfWorkDone = Guard.Against.Null(value, nameof(value));
    }

    public void SetStatusStatementVolume(decimal value)
    {
        StatusStatementVolume = Guard.Against.Null(value, nameof(value));
    }

    public void SetCalculatedAmount(decimal value)
    {
        CalculatedAmount = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    #endregion

    #region Methods 

    public void AddEmployerStatusStatementProjectOperation(EmployerStatusStatementProjectOperation newData)
    {
        ArgumentNullException.ThrowIfNull(newData);

        _employerStatusStatementProjectOperations.Add(newData);
    }

    public void AddHistory()
    {
        _employerStatusStatementHistories.Add(new EmployerStatusStatementHistory(
            this,
            Status,
            StartDate,
            EndDate,
            PercentageOfWorkDone,
            CalculatedAmount,
            StatusStatementVolume,
            StatusStatementCode,
            Description));
    }

    public void AddDocuments(List<string>? urls, bool justAdd)
    {
        if (urls != null && urls.Count > 0)
        {
            if (justAdd)
            {
                foreach (var url in urls)
                    _employerStatusStatementDocuments.Add(EmployerStatusStatementDocument.Create(url, this));
            }
            else
            {
                if (_employerStatusStatementDocuments.Any())
                    _employerStatusStatementDocuments.ForEach(c => c.SetIsDeleted());

                foreach (var url in urls)
                    _employerStatusStatementDocuments.Add(EmployerStatusStatementDocument.Create(url, this));
            }
        }
        else
        {
            if (_employerStatusStatementDocuments.Any())
                _employerStatusStatementDocuments.ForEach(c => c.SetIsDeleted());
        }
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<EmployerStatusStatementProjectOperation> _employerStatusStatementProjectOperations;
    public IReadOnlyList<EmployerStatusStatementProjectOperation> EmployerStatusStatementProjectOperations => _employerStatusStatementProjectOperations;
    private List<EmployerStatusStatementHistory> _employerStatusStatementHistories;
    public IReadOnlyList<EmployerStatusStatementHistory> EmployerStatusStatementHistories => _employerStatusStatementHistories;
    private List<EmployerStatusStatementDocument> _employerStatusStatementDocuments;
    public IReadOnlyList<EmployerStatusStatementDocument> EmployerStatusStatementDocuments => _employerStatusStatementDocuments;

    private EmployerStatusStatement()
    {
        _employerStatusStatementProjectOperations = [];
        _employerStatusStatementHistories = [];
        _employerStatusStatementDocuments = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
