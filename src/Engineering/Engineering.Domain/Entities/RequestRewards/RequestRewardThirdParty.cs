namespace Engineering.Domain.Entities.RequestRewards;

public class RequestRewardThirdParty : AuditableEntity<RequestRewardThirdParty>
{

    #region Properties

    [Description(GlobalCmts.ThirdPartyId)]
    public long ThirdPartyId { get; set; }

    [Description(GlobalCmts.RequestReward)]
    public long RequestRewardId { get; private set; }
    public RequestReward RequestReward { get; private set; }

    #endregion

    public RequestRewardThirdParty(long thirdPartyId,
        RequestReward requestReward) : this()
    {
        SetThirdPartyId(thirdPartyId);
        SetRequestReward(requestReward);
    }

    #region Commands

    public void SetData(long thirdPartyId,
        RequestReward requestReward)
    {
        SetThirdPartyId(thirdPartyId);
        SetRequestReward(requestReward);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetThirdPartyId(long value)
    {
        ThirdPartyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestReward(RequestReward value)
    {
        RequestReward = Guard.Against.Null(value, nameof(value));
        RequestRewardId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestRewardThirdParty() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
