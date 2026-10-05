namespace Engineering.Domain.Entities.FixAssetMachineries;

/// <summary>
/// ثبت زمان بلا استفاده بودن ماشین آلات
/// </summary>
public class FixAssetMachineryRate : AuditableEntity<FixAssetMachineryRate>
{
    #region Properties

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime EndDate { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public decimal? HourlyRate { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public decimal? DailyRate { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public decimal? ServiceRate { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public decimal? VolumeRate { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public long FixAssetMachineryId { get; private set; }
    public FixAssetMachinery FixAssetMachinery { get; private set; }

    #endregion

    public FixAssetMachineryRate(
       DateTime startDate,
       DateTime endDate,
       decimal? hourlyRate,
       decimal? dailyRate,
       decimal? serviceRate,
       decimal? volumeRate,
       FixAssetMachinery fixAssetMachinery)
    {
        StartDate = Guard.Against.Null(startDate, nameof(startDate));
        SetStartDate(startDate);
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        SetEndDate(endDate);
        HourlyRate = hourlyRate;
        SetHourlyRate(hourlyRate);
        DailyRate = dailyRate;
        SetDailyRate(dailyRate);
        ServiceRate = serviceRate;
        SetServiceRate(serviceRate);
        VolumeRate = volumeRate;
        SetVolumeRate(volumeRate);
        FixAssetMachinery = Guard.Against.Null(fixAssetMachinery, nameof(fixAssetMachinery));
        SetFixAssetMachinery(fixAssetMachinery);
    }

    #region Commands

    public void SetFixAssetMachinery(FixAssetMachinery value)
    {
        FixAssetMachinery = Guard.Against.Null(value, nameof(value));
        FixAssetMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetDailyRate(decimal? value)
    {
        DailyRate = value;
    }
    public void SetServiceRate(decimal? value)
    {
        ServiceRate = value;
    }
    public void SetHourlyRate(decimal? value)
    {
        HourlyRate = value;
    }
    public void SetVolumeRate(decimal? value)
    {
        VolumeRate = value;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FixAssetMachineryRate()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
