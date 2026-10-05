using MachineryEntity = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

/// <summary>
/// استاندارد های ماشین آلات
/// </summary>
public class ConsumptionStandardMachinery : AuditableEntity<ConsumptionStandardMachinery>
{
    public decimal MachineryNumber { get; private set; }
    public long TimeSpant { get; private set; }
    public decimal? UnusedPercentage { get; private set; } = 0;

    public OperationInfo OperationInfo { get; set; }
    public MachineryEntity Machinery { get; set; }


    public ConsumptionStandardMachinery(OperationInfo operationInfo, MachineryEntity machinery,
        decimal machineryNumber, long timeSpant, decimal? unusedPercentage)
    {
        OperationInfo = Guard.Against.Null(operationInfo, nameof(operationInfo));
        Machinery = Guard.Against.Null(machinery, nameof(machinery));
        MachineryNumber = Guard.Against.Null(machineryNumber, nameof(machineryNumber));
        TimeSpant = timeSpant;
        UnusedPercentage = unusedPercentage;
    }

    #region Set data

    public void SetMachineryEntity(MachineryEntity machinery)
    {
        Machinery = Guard.Against.Null(machinery, nameof(machinery));
    }
    public void SetMachineryNumber(decimal machineryNumber)
    {
        MachineryNumber = Guard.Against.Null(machineryNumber, nameof(machineryNumber));
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
    private ConsumptionStandardMachinery() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
