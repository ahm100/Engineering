using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Domain.Entities.EmployerStatusStatements;

/// <summary>
/// شرح عملیات
/// </summary>
public class EmployerStatusStatementProjectOperationDetailDaily : AuditableEntity<EmployerStatusStatementProjectOperationDetailDaily>
{
    private List<EmployerStatusStatementDailyDocument> _employerStatusStatementDetailDailyDocuments;

    /// <summary>
    /// طول پیمانکار
    /// </summary>
    public decimal ContractorLength { get; private set; } = 0;
    /// <summary>
    /// عرض پیمانکار
    /// </summary>
    public decimal ContractorWidth { get; private set; } = 0;
    /// <summary>
    /// ارتفاع پیمانکار
    /// </summary>
    public decimal ContractorHeight { get; private set; } = 0;
    /// <summary>
    /// وزن پیمانکار
    /// </summary>
    public decimal ContractorWeight { get; private set; } = 0;
    /// <summary>
    /// تعداد پیمانکار
    /// </summary>
    public decimal ContractorNumber { get; private set; } = 0;
    /// <summary>
    /// مقدار پیمانکار
    /// </summary>
    public decimal ContractorVolume { get; private set; } = 0;
    /// <summary>
    /// توضیحات پیمانکار
    /// </summary>
    public string? ContractorDescription { get; private set; } = string.Empty;

    /// <summary>
    /// طول ناظر
    /// </summary>
    public decimal SupervisorLength { get; private set; } = 0;
    /// <summary>
    /// عرض ناظر
    /// </summary>
    public decimal SupervisorWidth { get; private set; } = 0;
    /// <summary>
    /// ارتفاع ناظر
    /// </summary>
    public decimal SupervisorHeight { get; private set; } = 0;
    /// <summary>
    /// وزن ناظر
    /// </summary>
    public decimal SupervisorWeight { get; private set; } = 0;
    /// <summary>
    /// تعداد ناظر
    /// </summary>
    public decimal SupervisorNumber { get; private set; } = 0;
    /// <summary>
    /// مقدار ناظر
    /// </summary>
    public decimal SupervisorVolume { get; private set; } = 0;
    /// <summary>
    /// توضیحات ناظر
    /// </summary>
    public string? SupervisorDescription { get; private set; } = string.Empty;

    /// <summary>
    /// طول مشاور
    /// </summary>
    public decimal ConsultantLength { get; private set; } = 0;
    /// <summary>
    /// عرض مشاور
    /// </summary>
    public decimal ConsultantWidth { get; private set; } = 0;
    /// <summary>
    /// ارتفاع مشاور
    /// </summary>
    public decimal ConsultantHeight { get; private set; } = 0;
    /// <summary>
    /// وزن مشاور
    /// </summary>
    public decimal ConsultantWeight { get; private set; } = 0;
    /// <summary>
    /// تعداد مشاور
    /// </summary>
    public decimal ConsultantNumber { get; private set; } = 0;
    /// <summary>
    /// مقدار مشاور
    /// </summary>
    public decimal ConsultantVolume { get; private set; } = 0;
    /// <summary>
    /// توضیحات مشاور
    /// </summary>
    public string? ConsultantDescription { get; private set; } = string.Empty;

    /// <summary>
    /// طول نماینده کارفرما
    /// </summary>
    public decimal EmployerRepresentativeLength { get; private set; } = 0;
    /// <summary>
    /// عرض نماینده کارفرما
    /// </summary>
    public decimal EmployerRepresentativeWidth { get; private set; } = 0;
    /// <summary>
    /// ارتفاع نماینده کارفرما
    /// </summary>
    public decimal EmployerRepresentativeHeight { get; private set; } = 0;
    /// <summary>
    /// وزن نماینده کارفرما
    /// </summary>
    public decimal EmployerRepresentativeWeight { get; private set; } = 0;
    /// <summary>
    /// تعداد نماینده کارفرما
    /// </summary>
    public decimal EmployerRepresentativeNumber { get; private set; } = 0;
    /// <summary>
    /// مقدار نماینده کارفرما
    /// </summary>
    public decimal EmployerRepresentativeVolume { get; private set; } = 0;
    /// <summary>
    /// توضیحات نماینده کارفرما
    /// </summary>
    public string? EmployerRepresentativeDescription { get; private set; } = string.Empty;

    public IReadOnlyList<EmployerStatusStatementDailyDocument> EmployerStatusStatementDailyDocuments => _employerStatusStatementDetailDailyDocuments;

    public EmployerStatusStatementProjectOperationDetail EmployerStatusStatementProjectOperationDetail { get; set; }
    public DailyProjectOperation DailyProjectOperation { get; set; }

    public EmployerStatusStatementProjectOperationDetailDaily(
        EmployerStatusStatementProjectOperationDetail employerStatusStatementProjectOperationDetail,
        DailyProjectOperation dailyProjectOperation
        )
    {
        EmployerStatusStatementProjectOperationDetail = Guard.Against.Null(employerStatusStatementProjectOperationDetail, nameof(employerStatusStatementProjectOperationDetail));
        DailyProjectOperation = Guard.Against.Null(dailyProjectOperation, nameof(dailyProjectOperation));
        SetContractorLength(dailyProjectOperation.Length);
        SetContractorWidth(dailyProjectOperation.Width);
        SetContractorHeight(dailyProjectOperation.Height);
        SetContractorWeight(dailyProjectOperation.Weight);
        SetContractorNumber(dailyProjectOperation.Number);

        SetContractorVolume();
        SetSupervisorVolume();
        SetConsultantVolume();
        SetEmployerRepresentativeVolume();

        ContractorDescription = dailyProjectOperation.Description;

        _employerStatusStatementDetailDailyDocuments = new List<EmployerStatusStatementDailyDocument>();
    }

    #region Set data 

    public void SetContractorVolume()
    {
        ContractorVolume = ContractorLength * ContractorWidth * ContractorHeight * ContractorWeight * ContractorNumber;
    }
    public void SetContractorWorkVolumeToZero()
    {
        ContractorLength = 0;
        ContractorWidth = 0;
        ContractorHeight = 0;
        ContractorWeight = 0;
        ContractorNumber = 0;
        SetContractorVolume();
    }
    public void SetContractorWorkVolume(
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        string? description)
    {
        ContractorLength = length;
        ContractorWidth = width;
        ContractorHeight = height;
        ContractorWeight = weight;
        ContractorNumber = number;
        SetContractorVolume();
        SetContractorDescription(description);
    }
    public void SetSupervisorVolume()
    {
        SupervisorVolume = SupervisorLength * SupervisorWidth * SupervisorHeight * SupervisorWeight * SupervisorNumber;
    }
    public void SetSupervisorVolumeToZero()
    {
        SupervisorLength = 0;
        SupervisorWidth = 0;
        SupervisorHeight = 0;
        SupervisorWeight = 0;
        SupervisorNumber = 0;
        SetSupervisorVolume();
    }
    public void SetSupervisorVolume(
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        string? description)
    {
        SupervisorLength = length;
        SupervisorWidth = width;
        SupervisorHeight = height;
        SupervisorWeight = weight;
        SupervisorNumber = number;
        SetSupervisorVolume();
        SetSupervisorDescription(description);
    }
    public void SetConsultantVolume()
    {
        ConsultantVolume = ConsultantLength * ConsultantWidth * ConsultantHeight * ConsultantWeight * ConsultantNumber;
    }
    public void SetConsultantVolumeToZero()
    {
        ConsultantLength = 0;
        ConsultantWidth = 0;
        ConsultantHeight = 0;
        ConsultantWeight = 0;
        ConsultantNumber = 0;
        SetConsultantVolume();
    }
    public void SetConsultantVolume(
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        string? description)
    {
        ConsultantLength = length;
        ConsultantWidth = width;
        ConsultantHeight = height;
        ConsultantWeight = weight;
        ConsultantNumber = number;
        SetConsultantVolume();
        SetConsultantDescription(description);
    }
    public void SetEmployerRepresentativeVolume()
    {
        EmployerRepresentativeVolume = EmployerRepresentativeLength * EmployerRepresentativeWidth * EmployerRepresentativeHeight * EmployerRepresentativeWeight * EmployerRepresentativeNumber;
    }
    public void SetEmployerRepresentativeVolumeToZero()
    {
        EmployerRepresentativeLength = 0;
        EmployerRepresentativeWidth = 0;
        EmployerRepresentativeHeight = 0;
        EmployerRepresentativeWeight = 0;
        EmployerRepresentativeNumber = 0;
        SetEmployerRepresentativeVolume();
    }
    public void SetEmployerRepresentativeVolume(
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        string? description)
    {
        EmployerRepresentativeLength = length;
        EmployerRepresentativeWidth = width;
        EmployerRepresentativeHeight = height;
        EmployerRepresentativeWeight = weight;
        EmployerRepresentativeNumber = number;
        SetEmployerRepresentativeVolume();
        SetEmployerRepresentativeDescription(description);
    }

    public void SetContractorLength(decimal value)
    {
        ContractorLength = Guard.Against.Null(value, nameof(value));
        SetSupervisorLength(value);
    }
    public void SetSupervisorLength(decimal value)
    {
        SetConsultantLength(value);
    }
    public void SetConsultantLength(decimal value)
    {
        SetEmployerRepresentativeLength(value);
    }
    public void SetEmployerRepresentativeLength(decimal value)
    {
        EmployerRepresentativeLength = value;
    }

    public void SetContractorWidth(decimal value)
    {
        ContractorWidth = Guard.Against.Null(value, nameof(value));
        SetSupervisorWidth(value);
    }
    public void SetSupervisorWidth(decimal value)
    {
        SupervisorWidth = value;
        SetConsultantWidth(value);
    }
    public void SetConsultantWidth(decimal value)
    {
        ConsultantWidth = value;
        SetEmployerRepresentativeWidth(value);
    }
    public void SetEmployerRepresentativeWidth(decimal value)
    {
        EmployerRepresentativeWidth = value;
    }

    public void SetContractorHeight(decimal value)
    {
        ContractorHeight = Guard.Against.Null(value, nameof(value));
        SetSupervisorHeight(value);
    }
    public void SetSupervisorHeight(decimal value)
    {
        SupervisorHeight = value;
        SetConsultantHeight(value);
    }
    public void SetConsultantHeight(decimal value)
    {
        ConsultantHeight = value;
        SetEmployerRepresentativeHeight(value);
    }
    public void SetEmployerRepresentativeHeight(decimal value)
    {
        EmployerRepresentativeHeight = value;
    }

    public void SetContractorWeight(decimal value)
    {
        ContractorWeight = Guard.Against.Null(value, nameof(value));
        SetSupervisorWeight(value);
    }
    public void SetSupervisorWeight(decimal value)
    {
        SupervisorWeight = value;
        SetConsultantWeight(value);
    }
    public void SetConsultantWeight(decimal value)
    {
        ConsultantWeight = value;
        SetEmployerRepresentativeWeight(value);
    }
    public void SetEmployerRepresentativeWeight(decimal value)
    {
        EmployerRepresentativeWeight = value;
    }

    public void SetContractorNumber(decimal value)
    {
        ContractorNumber = Guard.Against.Null(value, nameof(value));
        SetSupervisorNumber(value);
    }
    public void SetSupervisorNumber(decimal value)
    {
        SupervisorNumber = value;
        SetConsultantNumber(value);
    }
    public void SetConsultantNumber(decimal value)
    {
        ConsultantNumber = value;
        SetEmployerRepresentativeNumber(value);
    }
    public void SetEmployerRepresentativeNumber(decimal value)
    {
        EmployerRepresentativeNumber = value;
    }

    public void SetContractorDescription(string? value)
    {
        ContractorDescription = value;
    }

    public void SetSupervisorDescription(string? value)
    {
        SupervisorDescription = value;
    }

    public void SetConsultantDescription(string? value)
    {
        ConsultantDescription = value;
    }

    public void SetEmployerRepresentativeDescription(string? value)
    {
        EmployerRepresentativeDescription = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    public void AddDocuments(List<string>? urls, bool justAdd)
    {
        if (urls != null && urls.Count > 0)
        {
            if (justAdd)
            {
                foreach (var url in urls)
                    _employerStatusStatementDetailDailyDocuments.Add(EmployerStatusStatementDailyDocument.Create(url, this));
            }
            else
            {
                if (_employerStatusStatementDetailDailyDocuments.Any())
                    _employerStatusStatementDetailDailyDocuments.ForEach(c => c.SetIsDeleted());

                foreach (var url in urls)
                    _employerStatusStatementDetailDailyDocuments.Add(EmployerStatusStatementDailyDocument.Create(url, this));
            }
        }
        else
        {
            if (_employerStatusStatementDetailDailyDocuments.Any())
                _employerStatusStatementDetailDailyDocuments.ForEach(c => c.SetIsDeleted());
        }
    }

    #region Methods 


    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerStatusStatementProjectOperationDetailDaily()
    {
        _employerStatusStatementDetailDailyDocuments = new List<EmployerStatusStatementDailyDocument>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
