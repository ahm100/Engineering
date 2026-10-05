using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.EmployerStatusStatements;

/// <summary>
/// شرح عملیات
/// </summary>
public class EmployerStatusStatementProjectOperationDetail : AuditableEntity<EmployerStatusStatementProjectOperationDetail>
{
    private List<EmployerStatusStatementProjectOperationDetailDaily> _employerStatusStatementProjectOperationDetailDailies;

    /// <summary>
    /// حجم کل
    /// </summary>
    public decimal TotalWorkVolume { get; private set; } = 0;
    /// <summary>
    /// حجم کل کارکرد روزانه
    /// </summary>
    public decimal TotalDailyWorkVolume { get; private set; } = 0;

    /// <summary>
    /// حجم تایید پیمانکار
    /// </summary>
    public decimal ContractorWorkVolume { get; private set; } = 0;

    /// <summary>
    /// حجم تایید ناظر
    /// </summary>
    public decimal SupervisorWorkVolume { get; private set; } = 0;

    /// <summary>
    /// حجم تایید مشاور
    /// </summary>
    public decimal ConsultantWorkVolume { get; private set; } = 0;

    /// <summary>
    /// حجم تایید نماینده کارفرما
    /// </summary>
    public decimal EmployerRepresentativeWorkVolume { get; private set; } = 0;

    /// <summary>
    /// توضیحات
    /// </summary>
    public string? Description { get; private set; }

    public IReadOnlyList<EmployerStatusStatementProjectOperationDetailDaily> EmployerStatusStatementProjectOperationDetailDailies => _employerStatusStatementProjectOperationDetailDailies;

    public EmployerStatusStatementProjectOperation EmployerStatusStatementProjectOperation { get; set; }
    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    public EmployerStatusStatementProjectOperationDetail(
        EmployerStatusStatementProjectOperation employerStatusStatementProjectOperation,
        ProjectOperationDetail projectOperationDetail,
        string? description)
    {
        EmployerStatusStatementProjectOperation = Guard.Against.Null(employerStatusStatementProjectOperation, nameof(employerStatusStatementProjectOperation));
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        TotalWorkVolume = Guard.Against.Null(ProjectOperationDetail.FinalAmount, nameof(ProjectOperationDetail.FinalAmount));
        Description = projectOperationDetail.Description;

        _employerStatusStatementProjectOperationDetailDailies = new List<EmployerStatusStatementProjectOperationDetailDaily>();
    }

    #region Set data 

    public void SetTotalDailyWorkVolume()
    {
        TotalDailyWorkVolume = _employerStatusStatementProjectOperationDetailDailies.Sum(x => x.DailyProjectOperation.FinalAmount);
    }

    public void SetContractorWorkVolume()
    {
        ContractorWorkVolume = _employerStatusStatementProjectOperationDetailDailies.Sum(x => x.ContractorVolume);
    }
    public void SetContractorWorkVolumeToZero()
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetailDailies)
            daily.SetContractorWorkVolumeToZero();
        SetContractorWorkVolume();
    }

    public void SetSupervisorWorkVolume()
    {
        SupervisorWorkVolume = _employerStatusStatementProjectOperationDetailDailies.Sum(x => x.SupervisorVolume);
    }
    public void SetSupervisorWorkVolumeToZero()
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetailDailies)
            daily.SetSupervisorVolumeToZero();
        SetSupervisorWorkVolume();
    }

    public void SetConsultantWorkVolume()
    {
        ConsultantWorkVolume = _employerStatusStatementProjectOperationDetailDailies.Sum(x => x.ConsultantVolume);
    }
    public void SetConsultantWorkVolumeToZero()
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetailDailies)
            daily.SetConsultantVolumeToZero();
        SetConsultantWorkVolume();
    }

    public void SetEmployerRepresentativeWorkVolume()
    {
        EmployerRepresentativeWorkVolume = _employerStatusStatementProjectOperationDetailDailies.Sum(x => x.EmployerRepresentativeVolume);
    }
    public void SetEmployerRepresentativeWorkVolumeToZero()
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetailDailies)
            daily.SetEmployerRepresentativeVolumeToZero();
        SetEmployerRepresentativeWorkVolume();
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Methods 


    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerStatusStatementProjectOperationDetail()
    {
        _employerStatusStatementProjectOperationDetailDailies = new List<EmployerStatusStatementProjectOperationDetailDaily>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
