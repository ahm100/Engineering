using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Domain.Entities.DailyProjectOperations;

[Description(DailyProjectOperationCmts.DailyProjectOperation)]
public class DailyProjectOperation : AuditableEntity<DailyProjectOperation>
{
    #region Properties

    [Description(DailyProjectOperationCmts.LegacyId)]
    public long? LegacyId { get; private set; }

    [Description(DailyProjectOperationCmts.Status)]
    public ProjectOperationDetailStatus Status { get; private set; }

    [Description(DailyProjectOperationCmts.Type)]
    public DailyProjectOperationType Type { get; private set; } = DailyProjectOperationType.Daily;

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(DailyProjectOperationCmts.Length)]
    public decimal Length { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Width)]
    public decimal Width { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Height)]
    public decimal Height { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Weight)]
    public decimal Weight { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Number)]
    public decimal Number { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.FinalAmount)]
    public decimal FinalAmount => Length * Width * Height * Weight * Number;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(DailyProjectOperationCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    #endregion

    public DailyProjectOperation(
        ProjectOperationDetailStatus status,
        DateTime startDate,
        DateTime endDate,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        ProjectOperationDetail projectOperationDetail,
        string? description,
        long? companyId,
        long? legacyId) : this()
    {
        SetStatus(status);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetLength(length);
        SetWidth(width);
        SetHeight(height);
        SetWeight(weight);
        SetNumber(number);
        SetCompanyId(companyId);
        SetLegacyId(legacyId);
        SetDescription(description);
        SetProjectOperationDetail(projectOperationDetail);
        SetType();
        AddHistory();
    }

    #region SetData

    /// <summary>
    /// تنظیم وضعیت
    /// </summary>
    /// <param name="status"></param>
    public void SetStatus(ProjectOperationDetailStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetType()
    {
        if (StartDate != EndDate)
            Type = DailyProjectOperationType.Period;
        else
            Type = DailyProjectOperationType.Daily;
    }

    public void SetLegacyId(long? value)
    {
        LegacyId = value;
    }

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
    }

    /// <summary>
    /// تنظیم از تاریخ
    /// </summary>
    /// <param name="status"></param>
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }

    /// <summary>
    /// تنظیم تا تاریخ
    /// </summary>
    /// <param name="status"></param>
    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }

    /// <summary>
    /// تنظیم طول
    /// </summary>
    /// <param name="status"></param>
    public void SetLength(decimal value)
    {
        Length = Guard.Against.Null(value, nameof(value));
    }

    /// <summary>
    /// تنظیم عرض
    /// </summary>
    /// <param name="status"></param>
    public void SetWidth(decimal value)
    {
        Width = Guard.Against.Null(value, nameof(value));
    }

    public void SetWeight(decimal value)
    {
        Weight = Guard.Against.Null(value, nameof(value));
    }

    /// <summary>
    /// تنظیم تعداد
    /// </summary>
    /// <param name="status"></param>
    public void SetNumber(decimal value)
    {
        Number = Guard.Against.Null(value, nameof(value));
    }

    /// <summary>
    /// تنظیم ارتفاع
    /// </summary>
    /// <param name="status"></param>
    public void SetHeight(decimal value)
    {
        Height = Guard.Against.Null(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void AddHistory()
    {
        _dailyProjectOperationHistory.Add(new DailyProjectOperationHistory(this));
    }

    #endregion

    #region Commands

    /// <summary>
    /// افزودن خدمت
    /// </summary>
    /// <param name="service"></param>
    public void AddDailyProjectOperationService(DailyProjectOperationService service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _services.Add(service);
    }

    /// <summary>
    /// افزودن خدمت
    /// </summary>
    /// <param name="document"></param>
    public void AddDailyProjectOperationService(DailyProjectOperationDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        _documents.Add(document);
    }

    /// <summary>
    /// افزودن متخصصین
    /// </summary>
    /// <param name="expert"></param>
    public void AddDailyProjectOperationService(DailyProjectOperationExpert expert)
    {
        ArgumentNullException.ThrowIfNull(expert);
        _experts.Add(expert);
    }

    /// <summary>
    /// افزودن ماشین آلات
    /// </summary>
    /// <param name="machinery"></param>
    public void AddDailyProjectOperationService(DailyProjectOperationMachinery machinery)
    {
        ArgumentNullException.ThrowIfNull(machinery);
        _machineries.Add(machinery);
    }

    /// <summary>
    /// افزودن مصرفی
    /// </summary>
    /// <param name="product"></param>
    public void AddDailyProjectOperationService(DailyProjectOperationProduct product)
    {
        ArgumentNullException.ThrowIfNull(product);
        _products.Add(product);
    }

    /// <summary>
    /// افزودن پاداش جریمه
    /// </summary>
    /// <param name="requestReward"></param>
    public void AddDailyProjectOperationService(DailyProjectOperationRequestReward requestReward)
    {
        ArgumentNullException.ThrowIfNull(requestReward);
        _requestRewards.Add(requestReward);
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Description(DailyProjectOperationCmts.DailyProjectOperationServices)]
    private List<DailyProjectOperationService> _services;
    public IReadOnlyList<DailyProjectOperationService> DailyProjectOperationServices => _services;

    [Description(DailyProjectOperationCmts.DailyProjectOperationDocuments)]
    private List<DailyProjectOperationDocument> _documents;
    public IReadOnlyList<DailyProjectOperationDocument> DailyProjectOperationDocuments => _documents;

    [Description(DailyProjectOperationCmts.DailyProjectOperationExperts)]
    private List<DailyProjectOperationExpert> _experts;
    public IReadOnlyList<DailyProjectOperationExpert> DailyProjectOperationExperts => _experts;

    [Description(DailyProjectOperationCmts.DailyProjectOperationMachineries)]
    private List<DailyProjectOperationMachinery> _machineries;
    public IReadOnlyList<DailyProjectOperationMachinery> DailyProjectOperationMachineries => _machineries;

    [Description(DailyProjectOperationCmts.DailyProjectOperationProducts)]
    private List<DailyProjectOperationProduct> _products;
    public IReadOnlyList<DailyProjectOperationProduct> DailyProjectOperationProducts => _products;

    [Description(DailyProjectOperationCmts.DailyProjectOperationRequestRewards)]
    private List<DailyProjectOperationRequestReward> _requestRewards;
    public IReadOnlyList<DailyProjectOperationRequestReward> DailyProjectOperationRequestRewards => _requestRewards;

    [Description(DailyProjectOperationCmts.ContractorStatusStatementServices)]
    private List<ContractorStatusStatementService> _contractorStatusStatementServices;
    public IReadOnlyList<ContractorStatusStatementService> ContractorStatusStatementServices => _contractorStatusStatementServices;

    [Description(DailyProjectOperationCmts.EmployerStatusStatementProjectOperationDetailDailies)]
    private List<EmployerStatusStatementProjectOperationDetailDaily> _employerStatusStatementProjectOperationDetailDailies;
    public IReadOnlyList<EmployerStatusStatementProjectOperationDetailDaily> EmployerStatusStatementProjectOperationDetailDailies => _employerStatusStatementProjectOperationDetailDailies;

    [Description(DailyProjectOperationCmts.DailyProjectOperationHistories)]
    private List<DailyProjectOperationHistory> _dailyProjectOperationHistory;
    public IReadOnlyList<DailyProjectOperationHistory> DailyProjectOperationHistories => _dailyProjectOperationHistory;

    private DailyProjectOperation()
    {
        _documents = [];
        _experts = [];
        _services = [];
        _machineries = [];
        _products = [];
        _contractorStatusStatementServices = [];
        _employerStatusStatementProjectOperationDetailDailies = [];
        _requestRewards = [];
        _dailyProjectOperationHistory = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    #endregion
}
