using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.DailyProjectOperations;

[Description(DailyProjectOperationCmts.DailyProjectOperationService)]
public class DailyProjectOperationService : AuditableEntity<DailyProjectOperationService>
{
    #region Properties

    [Description(DailyProjectOperationCmts.DailyProjectOperation)]
    public long DailyProjectOperationId { get; private set; }
    public DailyProjectOperation DailyProjectOperation { get; private set; }

    [Description(DailyProjectOperationCmts.Volume)]
    public decimal Volume { get; private set; }

    [Description(DailyProjectOperationCmts.ProjectServiceVolume)]
    public decimal? ProjectServiceVolume { get; private set; }

    [Description(GlobalCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(GlobalCmts.ThirdPartyId)]
    public long? ThirdPartyId { get; private set; }

    [Description(DailyProjectOperationCmts.TimeSpant)]
    public long? TimeSpant { get; private set; } = 0;

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(DailyProjectOperationCmts.ProjectOperationDetailContractorService)]
    public long ContractorServiceId { get; private set; }
    public ProjectOperationDetailContractorService ProjectOperationDetailContractorService { get; private set; }

    #endregion

    public DailyProjectOperationService(
        DailyProjectOperation dailyProjectOperation,
        ProjectOperationDetailContractorService contractorService,
        decimal volume,
        decimal? pojectServiceVolume,
        long? contractorId,
        long? thirdPartyId,
        long? timeSpant,
        bool isActive) : this()
    {
        SetDailyProjectOperation(dailyProjectOperation);
        SetProjectOperationDetailContractorService(contractorService);
        SetVolume(volume);
        SetContractorId(contractorId);
        SetThirdPartyId(thirdPartyId);
        SetProjectServiceVolume(ProjectServiceVolume);
        SetTimeSpant(timeSpant);
        IsActive = isActive;
    }

    #region Commands

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void SetThirdPartyId(long? value)
    {
        ThirdPartyId = value;
    }

    public void SetDailyProjectOperation(DailyProjectOperation value)
    {
        DailyProjectOperation = Guard.Against.Null(value, nameof(value));
        DailyProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProjectOperationDetailContractorService(ProjectOperationDetailContractorService value)
    {
        ProjectOperationDetailContractorService = Guard.Against.Null(value, nameof(value));
        ContractorServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProjectServiceVolume(decimal? value)
    {
        ProjectServiceVolume = value;
    }

    public void SetVolume(decimal value)
    {
        Volume = value;
    }

    public void SetTimeSpant(long? value)
    {
        TimeSpant = value;
    }

    public void SetActive()
    {
        IsActive = true;
    }

    public void SetInActive()
    {
        IsActive = false;
    }

    /// <summary>
    /// حذف
    /// </summary>
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ContractorStatusStatementServiceDaily> _contractorStatusStatementServiceDailies;
    public IReadOnlyList<ContractorStatusStatementServiceDaily> ContractorStatusStatementServiceDailies => _contractorStatusStatementServiceDailies;
    private DailyProjectOperationService()
    {
        _contractorStatusStatementServiceDailies = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion


}
