using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Domain.Entities.RequestRewards;

[Description(GlobalCmts.Histories)]
public class RequestRewardHistory : AuditableEntity<RequestRewardHistory>
{
    #region Properties

    [Description(RequestRewardCmts.OfferedPrice)]
    public decimal? OfferedPrice { get; private set; }

    [Description(RequestRewardCmts.ConfirmedPrice)]
    public decimal ConfirmedPrice { get; private set; }

    [Description(RequestRewardCmts.Status)]
    public RequestRewardStatus Status { get; private set; }

    [Description(GlobalCmts.Description)]
    public string Description { get; private set; } = string.Empty;

    [Description(RequestRewardCmts.RegistrationDate)]
    public DateTime RegistrationDate { get; private set; }

    [Description(RequestRewardCmts.CurrencyId)]
    public long? CurrencyId { get; private set; }

    [Description(RequestRewardCmts.ManagerDescription)]
    public string? ManagerDescription { get; private set; }

    [Description(GlobalCmts.RequestReward)]
    public long RequestRewardId { get; private set; }
    public RequestReward RequestReward { get; private set; }

    #endregion

    public RequestRewardHistory(decimal? offeredPrice,
        decimal confirmedPrice,
        RequestRewardStatus status,
        string description,
        DateTime registrationDate,
        long? currencyId,
        string? managerDescription,
        RequestReward requestReward) : this()
    {
        SetOfferedPrice(offeredPrice);
        SetConfirmedPrice(confirmedPrice);
        SetStatus(status);
        SetDescription(description);
        SetRegistrationDate(registrationDate);
        SetCurrencyId(currencyId);
        SetRequestReward(requestReward);
        SetManagerDescription(managerDescription);
    }

    public static RequestRewardHistory Create(decimal offeredPrice,
        decimal confirmedPrice,
        RequestRewardStatus status,
        string description,
        DateTime registrationDate,
        long? currencyUnitId,
        string? managerDescription,
        RequestReward requestReward) => new(offeredPrice, confirmedPrice, status, description, registrationDate, currencyUnitId, managerDescription, requestReward);

    public void SetOfferedPrice(decimal? offeredPrice)
    {
        OfferedPrice = offeredPrice;
    }

    public void SetConfirmedPrice(decimal confirmedPrice)
    {
        ConfirmedPrice = Guard.Against.Null(confirmedPrice, nameof(confirmedPrice));
    }

    public void SetStatus(RequestRewardStatus status)
    {
        Status = Guard.Against.EnumOutOfRange(status, nameof(status));
    }

    public void SetDescription(string description)
    {
        Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
    }

    public void SetRegistrationDate(DateTime registrationDate)
    {
        RegistrationDate = Guard.Against.Null(registrationDate, nameof(registrationDate));
    }

    public void SetRequestReward(RequestReward value)
    {
        RequestReward = Guard.Against.Null(value, nameof(value));
        RequestRewardId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCurrencyId(long? currencyId)
    {
        CurrencyId = currencyId;
    }

    public void SetManagerDescription(string? managerDescription)
    {
        ManagerDescription = managerDescription;
    }

    #region Construtors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestRewardHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
