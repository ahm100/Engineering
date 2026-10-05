
namespace Engineering.Domain.Entities.OperationInfos;

public class PublicGroup : AuditableEntity<PublicGroup>
{
    [Description(GlobalCmts.ProductGroupId)]
    public long ProductGroupId { get; private set; }

    public PublicGroup(long productGroupId)
    {
        SetProductGroupId(productGroupId);
    }

    #region Set data

    public void SetProductGroupId(long value)
    {
        ProductGroupId = Guard.Against.Null(value, nameof(value)); ;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Methods 

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PublicGroup()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
