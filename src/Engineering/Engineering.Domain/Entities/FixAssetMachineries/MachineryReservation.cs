using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Domain.Entities.FixAssetMachineries;

/// <summary>
/// رزرو ماشین آلات
/// </summary>
public class MachineryReservation : AuditableEntity<MachineryReservation>
{
    #region Properties

    [Description(FixAssetMachineryCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(FixAssetMachineryCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.Status)]
    public MachineryReservationStatus Status { get; private set; } = MachineryReservationStatus.NotsStarted;

    [Description(FixAssetMachineryCmts.Unit)]
    public MachineryReservationUnit Unit { get; private set; }

    [Description(FixAssetMachineryCmts.FixAssetMachinery)]
    public long FixAssetMachineryId { get; set; }
    public FixAssetMachinery FixAssetMachinery { get; set; }

    [Description(FixAssetMachineryCmts.RequestMachinery)]
    public long RequestMachineryId { get; set; }
    public RequestMachinery RequestMachinery { get; set; }

    #endregion

    public MachineryReservation(DateTime startDate,
        DateTime endDate,
        string? description,
        MachineryReservationUnit unit,
        FixAssetMachinery fixAssetMachinery,
        RequestMachinery requestMachinery) : this()
    {
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetMachineryReservationUnit(unit);
        SetFixAssetMachinery(fixAssetMachinery);
        SetRequestMachinery(requestMachinery);
    }

    #region Command

    public void SetMachineryReservationUnit(MachineryReservationUnit value)
    {
        Unit = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value)); ;
    }

    public void SetFixAssetMachinery(FixAssetMachinery value)
    {
        FixAssetMachinery = Guard.Against.Null(value, nameof(value));
        FixAssetMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetMachineryReservationStatus(MachineryReservationStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MachineryReservation()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    #endregion
}
