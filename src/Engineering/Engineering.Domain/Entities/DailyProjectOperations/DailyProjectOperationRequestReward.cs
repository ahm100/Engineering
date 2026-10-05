using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Domain.Entities.DailyProjectOperations;

/// <summary>
///  عملکرد روزانه
/// </summary>
public class DailyProjectOperationRequestReward : AuditableEntity<DailyProjectOperationRequestReward>
{
    #region Properties

    [Description(DailyProjectOperationCmts.DailyProjectOperation)]
    public long DailyProjectOperationId { get; private set; }
    public DailyProjectOperation DailyProjectOperation { get; private set; }

    [Description(GlobalCmts.RequestReward)]
    public long RequestRewardId { get; private set; }
    public RequestReward RequestReward { get; private set; } = default!;

    #endregion

    public DailyProjectOperationRequestReward(long dailyProjectOperation,
        long requestRewardId) : this()
    {
        SetDailyProjectOperation(dailyProjectOperation);
        SetRequestReward(requestRewardId);
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DailyProjectOperationRequestReward() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    #endregion

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetDailyProjectOperation(long value)
    {
        DailyProjectOperationId = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestReward(long value)
    {
        RequestRewardId = Guard.Against.Null(value, nameof(value));
    }

    #endregion
}
