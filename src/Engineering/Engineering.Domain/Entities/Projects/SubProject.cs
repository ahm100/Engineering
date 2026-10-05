using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects;

[Description(SubProjectCmts.SubProject)]
public class SubProject : AuditableEntity<SubProject>
{
    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    [Description(SubProjectCmts.SubProjectCode)]
    public string SubProjectCode { get; private set; } = string.Empty;

    [Description(SubProjectCmts.SequenceNumber)]
    public long SequenceNumber { get; private set; }

    [Description(SubProjectCmts.Name)]
    public string Name { get; private set; } = string.Empty;

    [Description(SubProjectCmts.SubProjectType)]
    public SubProjectType Type { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(SubProjectCmts.ManagerId)]
    public long? ManagerId { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime? EndDate { get; private set; }

    [Description(SubProjectCmts.SubProjectStatus)]
    public SubProjectStatus Status { get; private set; }

    public SubProject(
        Project project,
        string code,
        long sequenceNumber,
        string name,
        SubProjectType type,
        string? description,
        long? managerId,
        DateTime startDate,
        DateTime? endDate) : this()
    {
        SetProject(project);
        SetSubProjectCode(code);
        SetSequenceNumber(sequenceNumber);
        Update(name, type, description, managerId, startDate, endDate);
        SetStatus(SubProjectStatus.Draft);
    }

    public void Update(
        string name,
        SubProjectType type,
        string? description,
        long? managerId,
        DateTime startDate,
        DateTime? endDate)
    {
        if (endDate.HasValue && endDate.Value.Date < startDate.Date)
            throw new ArgumentException("EndDate cannot be before StartDate.", nameof(endDate));

        SetName(name);
        SetType(type);
        SetDescription(description);
        SetManagerId(managerId);
        SetStartDate(startDate);
        SetEndDate(endDate);
    }

    public bool CanChangeStatusTo(SubProjectStatus status)
    {
        return (Status == SubProjectStatus.Draft && status == SubProjectStatus.Active) ||
               (Status == SubProjectStatus.Active && status == SubProjectStatus.Completed);
    }

    public void ChangeStatus(SubProjectStatus status)
    {
        if (!CanChangeStatusTo(status))
            throw new InvalidOperationException("Invalid SubProject status transition.");

        SetStatus(status);
    }

    public void Delete()
    {
        SoftDelete();
    }

    private void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = value.Id;
    }

    private void SetSubProjectCode(string value)
    {
        SubProjectCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    private void SetSequenceNumber(long value)
    {
        SequenceNumber = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    private void SetName(string value)
    {
        Name = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    private void SetType(SubProjectType value)
    {
        Type = value;
    }

    private void SetDescription(string? value)
    {
        Description = value;
    }

    private void SetManagerId(long? value)
    {
        ManagerId = value;
    }

    private void SetStartDate(DateTime value)
    {
        StartDate = value;
    }

    private void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }

    private void SetStatus(SubProjectStatus value)
    {
        Status = value;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SubProject()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
