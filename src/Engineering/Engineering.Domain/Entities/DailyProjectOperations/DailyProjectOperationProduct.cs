using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Domain.Entities.DailyProjectOperations;

[Description(DailyProjectOperationCmts.DailyProjectOperationProduct)]
public class DailyProjectOperationProduct : AuditableEntity<DailyProjectOperationProduct>
{
    #region Properties

    [Description(GlobalCmts.ProductId)]
    public long ProductId { get; private set; }

    [Description(DailyProjectOperationCmts.FinalValue)]
    public decimal FinalValue { get; private set; }

    [Description(DailyProjectOperationCmts.UnusedValue)]
    public decimal? UnusedValue { get; private set; } = 0;

    [Description(DailyProjectOperationCmts.DailyProjectOperation)]
    public long DailyProjectOperationId { get; private set; }
    public DailyProjectOperation DailyProjectOperation { get; private set; }

    [Description(DailyProjectOperationCmts.ConsumableVolumeProduct)]
    public long ConsumableVolumeProductId { get; private set; }
    public ConsumableVolumeProduct ConsumableVolumeProduct { get; private set; }

    #endregion

    public DailyProjectOperationProduct(long productId,
                                 decimal finalValue,
                                 decimal? unusedPercentage,
                                 DailyProjectOperation dailyProjectOperation,
                                 ConsumableVolumeProduct consumableVolumeProduct) : this()
    {
        SetProductId(productId);
        SetFinalValue(finalValue);
        SetUnusedValue(unusedPercentage);
        SetDailyProjectOperation(dailyProjectOperation);
        SetConsumableVolumeProduct(consumableVolumeProduct);
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DailyProjectOperationProduct() { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetFinalValue(decimal value)
    {
        FinalValue = Guard.Against.Null(value, nameof(value));
    }

    public void SetProductId(long? value)
    {
        ProductId = Guard.Against.Null(value, nameof(value));
    }

    public void SetUnusedValue(decimal? value)
    {
        UnusedValue = value;
    }

    public void SetDailyProjectOperation(DailyProjectOperation value)
    {
        DailyProjectOperation = Guard.Against.Null(value, nameof(value));
        DailyProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetConsumableVolumeProduct(ConsumableVolumeProduct value)
    {
        ConsumableVolumeProduct = Guard.Against.Null(value, nameof(value));
        ConsumableVolumeProductId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion
}
