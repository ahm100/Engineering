using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

public class ConsumableVolumeExpert : AuditableEntity<ConsumableVolumeExpert>
{
    private List<DailyProjectOperationExpert> _dailyOperationExperts;

    public long ExpertId { get; private set; }
    public decimal Number { get; private set; }
    public decimal? UnusedPercentage { get; private set; } = 0;
    public bool IsStandard { get; private set; }
    public long? StandardValue { get; private set; }
    public long FinalValue { get; private set; }

    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    public IReadOnlyList<DailyProjectOperationExpert> DailyOperationExperts => _dailyOperationExperts;
    public ConsumableVolumeExpert(ProjectOperationDetail projectOperationDetail, long expertId, decimal number, decimal? unusedPercentage,
        bool isStandard, long? standardValue, long finalValue)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        ExpertId = Guard.Against.Null(expertId, nameof(expertId));
        Number = Guard.Against.Null(number, nameof(number));
        UnusedPercentage = unusedPercentage;
        IsStandard = isStandard;
        StandardValue = standardValue;
        FinalValue = Guard.Against.Null(finalValue, nameof(finalValue));

        _dailyOperationExperts = [];
    }

    #region Set Date

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
    }
    public void SetExpertId(long value)
    {
        ExpertId = Guard.Against.Null(value, nameof(value));
    }
    public void SetFinalValue(long value)
    {
        FinalValue = Guard.Against.Null(value, nameof(value));
    }
    public void SetNumber(decimal value)
    {
        Number = Guard.Against.Null(value, nameof(value));
    }
    public void SetUnusedPercentage(decimal? value)
    {
        UnusedPercentage = value;
    }
    public void SetIsStandard(bool value)
    {
        IsStandard = value;
    }
    public void SetStandardValue(long? value)
    {
        StandardValue = value;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<ProjectOperationDetailContractorExpert> _projectOperationDetailContractorExperts;
    public IReadOnlyList<ProjectOperationDetailContractorExpert> ProjectOperationDetailContractorExperts => _projectOperationDetailContractorExperts;
    private ConsumableVolumeExpert()
    {
        _dailyOperationExperts = [];
        _projectOperationDetailContractorExperts = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
