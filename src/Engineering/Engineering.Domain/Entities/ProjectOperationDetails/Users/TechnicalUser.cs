namespace Engineering.Domain.Entities.ProjectOperationDetails.Users;

/// <summary>
/// مسئولین فنی 
/// </summary>
public class UserTechnical : AuditableEntity<UserTechnical>
{
    public long TechnicalAssistantUserId { get; set; }

    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    public UserTechnical(ProjectOperationDetail projectOperationDetail, long technicalAssistantUserId)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        TechnicalAssistantUserId = Guard.Against.Null(technicalAssistantUserId, nameof(technicalAssistantUserId));
    }

    public void SetTechnicalAssistantUserId(long value)
    {
        TechnicalAssistantUserId = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UserTechnical() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
