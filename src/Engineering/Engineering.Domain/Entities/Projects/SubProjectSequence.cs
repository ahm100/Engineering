namespace Engineering.Domain.Entities.Projects;

public class SubProjectSequence
{
    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }

    [Description(SubProjectCmts.SequenceNumber)]
    public long LastSequenceNumber { get; private set; }

    public Project Project { get; private set; }

    public SubProjectSequence(
        long projectId,
        long lastSequenceNumber) : this()
    {
        SetProjectId(projectId);
        SetLastSequenceNumber(lastSequenceNumber);
    }

    private void SetProjectId(long value)
    {
        ProjectId = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    private void SetLastSequenceNumber(long value)
    {
        LastSequenceNumber = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SubProjectSequence()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
