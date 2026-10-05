using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects.Histories;

[Description(SubProjectCmts.SubProjectHistory)]
public class SubProjectHistory : AuditableEntity<SubProjectHistory>
{
    [Description(SubProjectCmts.SubProjectId)]
    public long SubProjectId { get; private set; }
    public SubProject SubProject { get; private set; }

    [Description(SubProjectCmts.SubProjectHistory)]
    public SubProjectHistoryOperation Operation { get; private set; }

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

    public SubProjectHistory(
        SubProject value,
        SubProjectHistoryOperation operation) : this()
    {
        SubProject = value;
        SubProjectId = value.Id;
        Operation = operation;
        Name = value.Name;
        Type = value.Type;
        Description = value.Description;
        ManagerId = value.ManagerId;
        StartDate = value.StartDate;
        EndDate = value.EndDate;
        Status = value.Status;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SubProjectHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
