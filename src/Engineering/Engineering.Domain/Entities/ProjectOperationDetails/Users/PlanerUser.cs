
namespace Engineering.Domain.Entities.ProjectOperationDetails.Users;

/// <summary>
/// مسئولین برنامه ریزی 
/// </summary>
public class UserPlaner : AuditableEntity<UserPlaner>
{
    public long UserPlanerId { get; set; }

    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    public UserPlaner(ProjectOperationDetail projectOperationDetail, long userPlanerId)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        UserPlanerId = Guard.Against.Null(userPlanerId, nameof(userPlanerId));
    }

    public void SetUserPlanerId(long value)
    {
        UserPlanerId = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UserPlaner() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
