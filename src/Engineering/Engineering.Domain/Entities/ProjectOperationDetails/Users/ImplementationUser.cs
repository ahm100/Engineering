namespace Engineering.Domain.Entities.ProjectOperationDetails.Users;

/// <summary>
/// مسئولین اجرا
/// </summary>
public class UserImplementation : AuditableEntity<UserImplementation>
{
    public long ImplementationAssistantUserId { get; set; }

    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    public UserImplementation(ProjectOperationDetail projectOperationDetail, long implementationAssistantUserId)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        ImplementationAssistantUserId = Guard.Against.Null(implementationAssistantUserId, nameof(implementationAssistantUserId));
    }

    public void SetImplementationAssistantUserId(long value)
    {
        ImplementationAssistantUserId = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UserImplementation() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
