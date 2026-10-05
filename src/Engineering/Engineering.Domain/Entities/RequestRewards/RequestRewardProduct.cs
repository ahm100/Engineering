namespace Engineering.Domain.Entities.RequestRewards;


[Description(RequestRewardCmts.RequestRewardProduct)]
public class RequestRewardProduct : AuditableEntity<RequestRewardProduct>
{
    #region Properties

    [Description(GlobalCmts.ProductId)]
    public long ProductId { get; private set; }

    [Description(GlobalCmts.CurrencyId)]
    public long CurrencyId { get; private set; }

    [Description(GlobalCmts.Count)]
    public int Count { get; private set; }

    [Description(RequestRewardCmts.Price)]
    public decimal Price { get; private set; }

    [Description(GlobalCmts.RequestReward)]
    public long RequestRewardId { get; private set; }
    public RequestReward RequestReward { get; private set; } = null!;

    #endregion

    public RequestRewardProduct(long productId,
        long currencyId,
        int count,
        decimal price,
        RequestReward requestReward)
    {
        SetProductId(productId);
        SetCurrencyId(currencyId);
        SetCount(count);
        SetPrice(price);
        SetRequestReward(requestReward);
    }

    #region Commands

    public void SetData(long productId,
        long currencyId,
        int count,
        decimal price,
        RequestReward requestReward)
    {
        SetProductId(productId);
        SetCurrencyId(currencyId);
        SetCount(count);
        SetPrice(price);
        SetRequestReward(requestReward);
    }

    public void SetProductId(long productId)
    {
        ProductId = Guard.Against.Null(productId, nameof(productId));
    }

    public void SetCurrencyId(long currencyId)
    {
        CurrencyId = Guard.Against.Null(currencyId, nameof(currencyId));
    }

    public void SetCount(int count)
    {
        Count = Guard.Against.Null(count, nameof(count));
    }

    public void SetPrice(decimal price)
    {
        Price = Guard.Against.Null(price, nameof(price));
    }

    public void SetRequestReward(RequestReward requestReward)
    {
        RequestReward = Guard.Against.Null(requestReward, nameof(requestReward));
        RequestRewardId = Guard.Against.Null(requestReward.Id, nameof(requestReward.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #region Constructors
    private RequestRewardProduct() { }
    #endregion
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
