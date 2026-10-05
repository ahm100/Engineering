using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Domain.Entities.OperationInfos;

[Description(OperationInfoCmts.OperationInfoDependency)]
public class OperationInfoDependency : AuditableEntity<OperationInfoDependency>
{
    [Description(OperationInfoCmts.RelationId)]
    public long RelationId { get; private set; }

    [Description(OperationInfoCmts.WorkingDays)]
    public int WorkingDays { get; private set; } = 1;

    [Description(OperationInfoCmts.DependencyType)]
    public OperationInfoDependencyType DependencyType { get; private set; }

    [Description(GlobalCmts.OperationInfo)]
    public long OperationInfoId { get; set; }
    public OperationInfo OperationInfo { get; set; }

    public OperationInfoDependency(OperationInfo operationInfo,
        long relationId,
        int workingDays,
        OperationInfoDependencyType dependencyType) : this()
    {
        SetOperationInfo(operationInfo);
        SetRelationId(relationId);
        SetWorkingDays(workingDays);
        SetDependencyType(dependencyType);
    }

    #region Set data

    public void SetOperationInfo(OperationInfo operationInfo)
    {
        OperationInfo = Guard.Against.Null(operationInfo, nameof(operationInfo));
        OperationInfoId = Guard.Against.Null(operationInfo.Id, nameof(operationInfo.Id));
    }

    public void SetRelationId(long relationId)
    {
        RelationId = Guard.Against.NegativeOrZero(relationId, nameof(relationId));
    }

    public void SetWorkingDays(int workingDays)
    {
        WorkingDays = Guard.Against.NegativeOrZero(workingDays, nameof(workingDays));
    }

    public void SetDependencyType(OperationInfoDependencyType dependencyType)
    {
        DependencyType = Guard.Against.Null(dependencyType, nameof(dependencyType));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    #endregion


    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private OperationInfoDependency() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
