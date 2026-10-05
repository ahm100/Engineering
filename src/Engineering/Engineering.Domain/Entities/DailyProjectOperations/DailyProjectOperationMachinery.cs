using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Domain.Entities.DailyProjectOperations;

/// <summary>
/// عملکرد ماشین آلات و تجهیزات
/// </summary>
public class DailyProjectOperationMachinery : AuditableEntity<DailyProjectOperationMachinery>
{

    #region Properties

    [Description(DailyProjectOperationCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    [Description(DailyProjectOperationCmts.FinalValue)]
    public long? FinalValue { get; private set; }

    [Description(DailyProjectOperationCmts.UnusedValue)]
    public long? UnusedValue { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.Number)]
    public decimal? Number { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.DailyProjectOperation)]
    public long DailyProjectOperationId { get; private set; }
    public DailyProjectOperation DailyProjectOperation { get; private set; }

    [Description(DailyProjectOperationCmts.ConsumableVolumeMachinery)]
    public long ConsumableVolumeMachineryId { get; private set; }
    public ConsumableVolumeMachinery ConsumableVolumeMachinery { get; private set; }

    #endregion


    public DailyProjectOperationMachinery(RequestMachinery requestMachinery,
                                          long? finalValue,
                                          long? unusedValue,
                                          decimal? number,
                                          DailyProjectOperation dailyProjectOperation,
                                          ConsumableVolumeMachinery consumableVolumeMachinery) : this()
    {
        SetRequestMachinery(requestMachinery);
        SetFinalValue(finalValue);
        SetUnusedValue(unusedValue);
        SetNumber(number);
        SetDailyProjectOperation(dailyProjectOperation);
        SetConsumableVolumeMachinery(consumableVolumeMachinery);
    }

    #region Constructors

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetFinalValue(long? value)
    {
        FinalValue = value;
    }

    public void SetUnusedValue(long? value)
    {
        UnusedValue = value;
    }

    public void SetNumber(decimal? value)
    {
        Number = value;
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetConsumableVolumeMachinery(ConsumableVolumeMachinery value)
    {
        ConsumableVolumeMachinery = Guard.Against.Null(value, nameof(value));
        ConsumableVolumeMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetDailyProjectOperation(DailyProjectOperation value)
    {
        DailyProjectOperation = Guard.Against.Null(value, nameof(value));
        DailyProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DailyProjectOperationMachinery() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    #endregion
}
