namespace Engineering.Domain.Entities.RequestRewards;

[Description(GlobalCmts.Document)]
public class RequestRewardDocument : AuditableEntity<RequestRewardDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.RequestReward)]
    public long RequestRewardId { get; private set; }
    public RequestReward RequestReward { get; private set; } = null!;

    public RequestRewardDocument(string url,
        RequestReward requestReward) : this()
    {
        SetUrl(url);
        SetRequestReward(requestReward);
    }

    #region Commands

    public void SetData(string url,
        RequestReward requestReward)
    {
        SetUrl(url);
        SetRequestReward(requestReward);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestReward(RequestReward value)
    {
        RequestReward = Guard.Against.Null(value, nameof(value));
        RequestRewardId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    private RequestRewardDocument() { }
}
