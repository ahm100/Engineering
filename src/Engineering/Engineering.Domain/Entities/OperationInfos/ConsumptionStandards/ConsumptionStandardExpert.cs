namespace Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

/// <summary>
/// استاندارد های متخصص ها
/// </summary>
public class ConsumptionStandardExpert : AuditableEntity<ConsumptionStandardExpert>
{
    public long ExpertUnitId { get; private set; }
    public decimal ExpertNumber { get; private set; }
    public long TimeSpant { get; private set; }
    public decimal? UnusedPercentage { get; private set; } = 0;

    public OperationInfo OperationInfo { get; set; }

    public ConsumptionStandardExpert(OperationInfo operationInfo, long expertUnitId,
        decimal expertNumber, long timeSpant, decimal? unusedPercentage)
    {
        OperationInfo = Guard.Against.Null(operationInfo, nameof(operationInfo));
        ExpertUnitId = Guard.Against.NegativeOrZero(expertUnitId, nameof(expertUnitId));
        ExpertNumber = Guard.Against.Null(expertNumber, nameof(expertNumber));
        TimeSpant = timeSpant;
        UnusedPercentage = unusedPercentage;
    }

    #region Set data

    public void SetExpertUnitId(long expertUnitId)
    {
        ExpertUnitId = Guard.Against.NegativeOrZero(expertUnitId, nameof(expertUnitId));
    }
    public void SetExpertNumber(decimal expertNumber)
    {
        ExpertNumber = Guard.Against.Null(expertNumber, nameof(expertNumber));
    }
    public void SetTimeSpant(long timeSpant)
    {
        TimeSpant = timeSpant;
    }
    public void SetUnusedPercentage(decimal? unusedPercentage)
    {
        UnusedPercentage = unusedPercentage;
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
    private ConsumptionStandardExpert() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
