
namespace Engineering.Domain.Entities.OperationInfos;

/// <summary>
/// خدمات شرح عملیات
/// </summary>
public class OperationInfoGroupRelation : AuditableEntity<OperationInfo>
{
    [Description(GlobalCmts.OperationInfo)]
    public OperationInfo OperationInfo { get; set; }

    [Description(OperationInfoCmts.OperationInfoGroup)]
    public OperationInfoGroup OperationInfoGroup { get; set; }

    public OperationInfoGroupRelation(OperationInfo operationInfo, OperationInfoGroup operationInfoGroup)
    {
        OperationInfo = Guard.Against.Null(operationInfo, nameof(operationInfo));
        OperationInfoGroup = Guard.Against.Null(operationInfoGroup, nameof(operationInfoGroup));

    }

    #region Set data 

    public void SetOperationInfo(OperationInfo value)
    {
        OperationInfo = value;
    }

    public void SetOperationInfoGroup(OperationInfoGroup value)
    {
        OperationInfoGroup = value;
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
    private OperationInfoGroupRelation()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
