namespace Engineering.Domain.Entities.FixAssetMachineries;

/// <summary>
/// پیوست درخواست ماشین آلات
/// </summary>
public class FixAssetMachineryDocument : AuditableEntity<FixAssetMachineryDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(FixAssetMachineryCmts.FixAssetMachinery)]
    public long FixAssetMachineryId { get; private set; }
    public FixAssetMachinery FixAssetMachinery { get; private set; }

    #endregion

    public FixAssetMachineryDocument(FixAssetMachinery fixAssetMachinery, string url)
    {
        FixAssetMachinery = Guard.Against.Null(fixAssetMachinery, nameof(fixAssetMachinery));
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
    }

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetUrl(string url)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url)); ;
    }
    public void SetFixAssetMachinery(FixAssetMachinery value)
    {
        FixAssetMachinery = Guard.Against.Null(value, nameof(value));
        FixAssetMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FixAssetMachineryDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    #endregion

}
