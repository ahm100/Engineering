using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Domain.Entities.DailyProjectOperations;

[Description(DailyProjectOperationCmts.DailyProjectOperationExpert)]
public class DailyProjectOperationExpert : AuditableEntity<DailyProjectOperationExpert>
{
    [Description(DailyProjectOperationCmts.ThirdPartyId)]
    public long ThirdPartyId { get; private set; }

    [Description(DailyProjectOperationCmts.FinalValue)]
    public long FinalValue { get; private set; }

    [Description(DailyProjectOperationCmts.UnusedValue)]
    public long? UnusedValue { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.DailyProjectOperation)]
    public long DailyProjectOperationId { get; private set; }
    public DailyProjectOperation DailyProjectOperation { get; private set; }

    [Description(DailyProjectOperationCmts.ConsumableVolumeExpert)]
    public long ConsumableVolumeExpertId { get; private set; }
    public ConsumableVolumeExpert ConsumableVolumeExpert { get; private set; }

    public DailyProjectOperationExpert(long thirdPartyId, long finalValue, long? unusedValue, DailyProjectOperation dailyProjectOperation, ConsumableVolumeExpert consumableVolumeExpert)
    {
        ThirdPartyId = Guard.Against.Null(thirdPartyId, nameof(thirdPartyId));
        FinalValue = Guard.Against.Null(finalValue, nameof(finalValue));
        UnusedValue = unusedValue;
        DailyProjectOperation = Guard.Against.Null(dailyProjectOperation, nameof(dailyProjectOperation));
        ConsumableVolumeExpert = Guard.Against.Null(consumableVolumeExpert, nameof(consumableVolumeExpert));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetThirdPartyId(long value)
    {
        ThirdPartyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetFinalValue(long value)
    {
        FinalValue = Guard.Against.Null(value, nameof(value));
    }

    public void SetUnusedValue(long? value)
    {
        UnusedValue = value;
    }

    public void SetDailyProjectOperation(DailyProjectOperation value)
    {
        DailyProjectOperation = Guard.Against.Null(value, nameof(value));
        DailyProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetConsumableVolumeExpert(ConsumableVolumeExpert value)
    {
        ConsumableVolumeExpert = Guard.Against.Null(value, nameof(value));
        ConsumableVolumeExpertId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DailyProjectOperationExpert() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
