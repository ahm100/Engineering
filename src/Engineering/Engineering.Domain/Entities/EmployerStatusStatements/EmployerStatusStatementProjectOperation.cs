using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.EmployerStatusStatements;

/// <summary>
/// شرح عملیات
/// </summary>
public class EmployerStatusStatementProjectOperation : AuditableEntity<EmployerStatusStatementProjectOperation>
{
    private List<EmployerStatusStatementProjectOperationDetail> _employerStatusStatementProjectOperationDetails;
    private List<EmployerStatusStatementProjectOperationDocument> _employerStatusStatementProjectOperationDocuments;

    /// <summary>
    /// حجم کل
    /// </summary>
    public decimal TotalWorkVolume { get; private set; } = 0;
    /// <summary>
    /// حجم براورد کل
    /// </summary>
    public decimal TotalDetailWorkVolume { get; private set; } = 0;
    /// <summary>
    /// حجم براورد کل
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
    /// قیمت واحد پیمانکار
    /// </summary>
    public decimal ContractorUnitPrice { get; private set; } = 0;
    /// <summary>
    /// قیمت کل پیمانکار
    /// </summary>
    public decimal ContractorTotalPrice { get; private set; } = 0;
    /// <summary>
    /// قیمت کل تایید شده پیمانکار
    /// </summary>
    public decimal ContractorConfirmeTotalPrice { get; private set; } = 0;

    /// <summary>
    /// قیمت واحد بازرگانی
    /// </summary>
    public decimal EmployerCommercialUnitPrice { get; private set; } = 0;
    /// <summary>
    /// قیمت کل بازرگانی
    /// </summary>
    public decimal EmployerCommercialTotalPrice { get; private set; } = 0;
    /// <summary>
    /// قیمت کل تایید شده بازرگانی
    /// </summary>
    public decimal EmployerCommercialConfirmeTotalPrice { get; private set; } = 0;

    /// <summary>
    /// توضیحات
    /// </summary>
    public string? Description { get; private set; }

    public IReadOnlyList<EmployerStatusStatementProjectOperationDetail> EmployerStatusStatementProjectOperationDetails => _employerStatusStatementProjectOperationDetails;
    public IReadOnlyList<EmployerStatusStatementProjectOperationDocument> EmployerStatusStatementProjectOperationDocuments => _employerStatusStatementProjectOperationDocuments;

    public EmployerStatusStatement EmployerStatusStatement { get; set; }
    public ProjectOperation ProjectOperation { get; set; }

    public EmployerStatusStatementProjectOperation(
        EmployerStatusStatement employerStatusStatement,
        ProjectOperation projectOperation,
        decimal? contractorUnitPrice,
        decimal contractorConfirmeTotalPrice,
        string? description)
    {
        EmployerStatusStatement = Guard.Against.Null(employerStatusStatement, nameof(employerStatusStatement));
        ProjectOperation = Guard.Against.Null(projectOperation, nameof(projectOperation));
        TotalWorkVolume = Guard.Against.Null(ProjectOperation.Workload, nameof(ProjectOperation.Workload));

        if (projectOperation.Price is null)
            projectOperation.SetPrice(contractorUnitPrice);
        else
        {
            if (projectOperation.Price != contractorUnitPrice)
                projectOperation.SetPrice(contractorUnitPrice);
        }

        ContractorUnitPrice = projectOperation.Price!.Value;
        ContractorConfirmeTotalPrice = Guard.Against.Null(contractorConfirmeTotalPrice, nameof(contractorConfirmeTotalPrice));
        ContractorTotalPrice = ContractorWorkVolume * ContractorUnitPrice;
        Description = description ?? "";

        _employerStatusStatementProjectOperationDetails = new List<EmployerStatusStatementProjectOperationDetail>();
        _employerStatusStatementProjectOperationDocuments = new List<EmployerStatusStatementProjectOperationDocument>();
    }

    #region Set data 

    public void SetTotalDetailWorkVolume()
    {
        TotalDetailWorkVolume = _employerStatusStatementProjectOperationDetails.Sum(x => x.TotalWorkVolume);
    }
    public void SetTotalDailyWorkVolume()
    {
        TotalDailyWorkVolume = _employerStatusStatementProjectOperationDetails.Sum(x => x.TotalDailyWorkVolume);
    }

    public void SetContractorWorkVolume()
    {
        ContractorWorkVolume = _employerStatusStatementProjectOperationDetails.Sum(x => x.ContractorWorkVolume);
    }
    public void SetContractorWorkVolumeToZero(string? description)
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetails)
            daily.SetContractorWorkVolumeToZero();
        SetContractorWorkVolume();
        SetDescription(description);
    }

    public void SetSupervisorWorkVolume()
    {
        SupervisorWorkVolume = _employerStatusStatementProjectOperationDetails.Sum(x => x.SupervisorWorkVolume);
    }
    public void SetSupervisorWorkVolumeToZero(string? description)
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetails)
            daily.SetSupervisorWorkVolumeToZero();
        SetSupervisorWorkVolume();
        SetDescription(description);
    }

    public void SetConsultantWorkVolume()
    {
        ConsultantWorkVolume = _employerStatusStatementProjectOperationDetails.Sum(x => x.ConsultantWorkVolume);
    }
    public void SetConsultantWorkVolumeToZero(string? description)
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetails)
            daily.SetConsultantWorkVolumeToZero();
        SetConsultantWorkVolume();
        SetDescription(description);
    }

    public void SetEmployerRepresentativeWorkVolume()
    {
        EmployerRepresentativeWorkVolume = _employerStatusStatementProjectOperationDetails.Sum(x => x.EmployerRepresentativeWorkVolume);
    }
    public void SetEmployerRepresentativeWorkVolumeToZero(string? description)
    {
        foreach (var daily in _employerStatusStatementProjectOperationDetails)
            daily.SetEmployerRepresentativeWorkVolumeToZero();
        SetEmployerRepresentativeWorkVolume();
        SetDescription(description);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void AddDocuments(List<string>? urls, bool justAdd)
    {
        if (urls != null && urls.Count > 0)
        {
            if (justAdd)
            {
                foreach (var url in urls)
                    _employerStatusStatementProjectOperationDocuments.Add(EmployerStatusStatementProjectOperationDocument.Create(url, this));
            }
            else
            {
                if (_employerStatusStatementProjectOperationDocuments.Any())
                    _employerStatusStatementProjectOperationDocuments.ForEach(c => c.SetIsDeleted());

                foreach (var url in urls)
                    _employerStatusStatementProjectOperationDocuments.Add(EmployerStatusStatementProjectOperationDocument.Create(url, this));
            }
        }
        else
        {
            if (_employerStatusStatementProjectOperationDocuments.Any())
                _employerStatusStatementProjectOperationDocuments.ForEach(c => c.SetIsDeleted());
        }
    }

    #endregion

    #region Methods 

    public void AddEmployerStatusStatementProjectOperationDetail(EmployerStatusStatementProjectOperationDetail newData)
    {
        ArgumentNullException.ThrowIfNull(newData);

        _employerStatusStatementProjectOperationDetails.Add(newData);
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerStatusStatementProjectOperation()
    {
        _employerStatusStatementProjectOperationDetails = new List<EmployerStatusStatementProjectOperationDetail>();
        _employerStatusStatementProjectOperationDocuments = new List<EmployerStatusStatementProjectOperationDocument>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
