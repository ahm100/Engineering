using Engineering.Domain.Entities.Adjustment.Enums;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Adjustments;

[Description(AdjustmentCmts.AdjustmentIndexValue)]
public class AdjustmentIndexValue
    : ActivateEntity<AdjustmentIndexValue, long>
{
    public long AdjustmentIndexId { get; private set; }

    public AdjustmentIndex AdjustmentIndex { get; private set; } = null!;

    public string YearName { get; private set; }= string.Empty;

    public ContractAdjustmentPeriod Period { get; private set; }

    [Description(AdjustmentCmts.AdjustmentIndexType)]
    public AdjustmentIndexType Type { get; private set; }

    public decimal Value { get; private set; }

    [Description(AdjustmentCmts.Coefficient)]
    public decimal? Coefficient { get; private set; }

    [Description(AdjustmentCmts.NotificationNumber)]
    public string? NotificationNumber { get; private set; }

    [Description(AdjustmentCmts.NotificationDate)]
    public DateTime? NotificationDate { get; private set; }

 
    private AdjustmentIndexValue()
    {
    }

    public AdjustmentIndexValue(
        AdjustmentIndex adjustmentIndex,
        string YearName,
        AdjustmentIndexType type,
        decimal value,
        decimal? coefficient,
        string? notificationNumber,
        DateTime? notificationDate,
        ContractAdjustmentPeriod period,
        bool isActive)
    {
        SetAdjustmentIndex(adjustmentIndex);
        SetYearName(YearName);
        SetType(type);
        SetValue(value);
        SetCoefficient(coefficient);
        SetNotificationNumber(notificationNumber);
        SetNotificationDate(notificationDate);
        SetPeriod(period);
        IsActive = isActive;

    }

    public void Update(
        string YearName,
        AdjustmentIndexType type,
        decimal value,
        decimal? coefficient,
        string? notificationNumber,
        DateTime? notificationDate,
        ContractAdjustmentPeriod period,
        bool isActive)
    {
        SetYearName(YearName);
        SetPeriod(period);
        SetType(type);
        SetValue(value);
        SetCoefficient(coefficient);
        SetNotificationNumber(notificationNumber);
        SetNotificationDate(notificationDate);
        IsActive = isActive;
    }

    public void Activate()
        => IsActive = true;

    public void Deactivate()
        => IsActive = false;

    private void SetAdjustmentIndex(AdjustmentIndex value)
    {
        AdjustmentIndex =
            Guard.Against.Null(value, nameof(value));
    }

    private void SetYearName(string value)
    {
        YearName = value;
    }

    private void SetPeriod(ContractAdjustmentPeriod value)
    {
        Period = value;
    }

    private void SetType(AdjustmentIndexType value)
    {
        Type = value;
    }

    private void SetValue(decimal value)
    {
        Value = value;
    }

    private void SetCoefficient(decimal? value)
    {
        Coefficient = value;
    }

    private void SetNotificationNumber(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            NotificationNumber = null;
            return;
        }

        if (value.Length > 100)
            throw new ArgumentException(
                "NotificationNumber cannot exceed 100 characters.",
                nameof(value));

        NotificationNumber = value;
    }

    private void SetNotificationDate(DateTime? value)
    {
        NotificationDate = value;
    }
  
}