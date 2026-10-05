namespace Engineering.Domain.Entities.Projects.ProjectUsers;

public class ProjectThirdParty : AuditableEntity<ProjectThirdParty, long>
{
    [Description(CCenterCmts.AuthorizedUserId)]
    public long AuthorizedThirdPartyId { get; private set; } = default;

    [Description(GlobalCmts.CostCenter)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    public ProjectThirdParty(
        Project project,
        long authorizedThirdPartyId) : this()
    {
        SetProject(project);
        SetAuthorizedThirdPartyId(authorizedThirdPartyId);
    }

    #region Set data

    public void SetAuthorizedThirdPartyId(long value)
    {
        AuthorizedThirdPartyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
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
    private ProjectThirdParty() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
