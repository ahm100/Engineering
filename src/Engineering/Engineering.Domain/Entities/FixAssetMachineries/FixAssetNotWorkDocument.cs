namespace Engineering.Domain.Entities.FixAssetMachineries;

[Description(FixAssetMachineryCmts.FixAssetNotWorkDocument)]
public class FixAssetNotWorkDocument : AuditableEntity<FixAssetNotWorkDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(FixAssetMachineryCmts.FixAssetMachineryNotWork)]
    public long FixAssetMachineryNotWorkId { get; private set; }
    public FixAssetMachineryNotWork FixAssetMachineryNotWork { get; private set; }

    #endregion

    public FixAssetNotWorkDocument(FixAssetMachineryNotWork FixAssetNotWork,
        string url) : this()
    {
        SetFixAssetMachineryNotWork(FixAssetNotWork);
        SetUrl(url);
    }

    #region Commands

    public void SetFixAssetMachineryNotWork(FixAssetMachineryNotWork value)
    {
        FixAssetMachineryNotWork = Guard.Against.Null(value, nameof(value));
        FixAssetMachineryNotWorkId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetUrl(string url)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url)); ;
    }

    #endregion
    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FixAssetNotWorkDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
