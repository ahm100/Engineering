namespace Engineering.Domain.Entities.FixAssetMachineries;

[Description(FixAssetMachineryCmts.FixAssetMachineryNotWork)]
public class FixAssetMachineryNotWork : AuditableEntity<FixAssetMachineryNotWork>
{

    #region Properties

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(FixAssetMachineryCmts.FixAssetMachinery)]
    public long FixAssetMachineryId { get; private set; }
    public FixAssetMachinery FixAssetMachinery { get; private set; }

    #endregion

    public FixAssetMachineryNotWork(string? description,
        DateTime startDate,
        DateTime endDate,
        FixAssetMachinery fixAssetMachinery) : this()
    {
        Description = description;
        StartDate = Guard.Against.Null(startDate, nameof(startDate));
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        FixAssetMachinery = Guard.Against.Null(fixAssetMachinery, nameof(fixAssetMachinery));
    }

    #region Commands

    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetFixAssetMachinery(FixAssetMachinery value)
    {
        FixAssetMachinery = Guard.Against.Null(value, nameof(value));
        FixAssetMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetEndDate(DateTime value)
    {
        EndDate = Guard.Against.Null(value, nameof(value));
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private readonly List<FixAssetNotWorkDocument> _fixAssetNotWorkDocuments;
    public IReadOnlyList<FixAssetNotWorkDocument> FixAssetNotWorkDocuments => _fixAssetNotWorkDocuments;

    private FixAssetMachineryNotWork()
    {
        _fixAssetNotWorkDocuments = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
