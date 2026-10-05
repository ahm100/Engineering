using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

public class ConsumableVolumeMachinery : AuditableEntity<ConsumableVolumeMachinery>
{
    private List<DailyProjectOperationMachinery> _dailyOperationMachineries;
    public decimal Number { get; private set; }
    public decimal? UnusedPercentage { get; private set; } = 0;
    public bool IsStandard { get; private set; }
    public long? StandardValue { get; private set; }
    public decimal FinalValue { get; private set; }

    public RequestMachineryUnit? Unit { get; private set; }

    public ProjectOperationDetail ProjectOperationDetail { get; set; }
    public Machinery Machinery { get; set; }

    public IReadOnlyList<DailyProjectOperationMachinery> DailyOperationMachineries => _dailyOperationMachineries;

    public ConsumableVolumeMachinery(ProjectOperationDetail projectOperationDetail, Machinery machinery, decimal number, decimal? unusedPercentage,
        bool isStandard, long? standardValue, decimal finalValue, RequestMachineryUnit? unit)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        Machinery = Guard.Against.Null(machinery, nameof(machinery));
        FinalValue = Guard.Against.Null(finalValue, nameof(finalValue));
        Number = Guard.Against.Null(number, nameof(number));
        UnusedPercentage = unusedPercentage;
        IsStandard = isStandard;
        StandardValue = standardValue;
        Unit = unit;

        _dailyOperationMachineries = [];
    }

    #region Set Date

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
    }

    public void SetMachinery(Machinery value)
    {
        Machinery = Guard.Against.Null(value, nameof(value));
    }
    public void SetFinalValue(decimal value)
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
    public void SetUnit(RequestMachineryUnit? value)
    {
        Unit = value ?? Unit;
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
    private ConsumableVolumeMachinery()
    {
        _dailyOperationMachineries = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
