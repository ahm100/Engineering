
namespace Engineering.Domain.Entities.EmployerContracts;

public class EmployerConsiderationDep : AuditableEntity<EmployerConsiderationDep, long>
{
    public long EmployerConsiderationId { get; set; }
    public EmployerConsideration EmployerConsideration { get; set; }

    public long EmployerOperationId { get; set; }
    public EmployerOperation EmployerOperation { get; set; }

    public EmployerConsiderationDep(
        EmployerConsideration employerConsideration,
        EmployerOperation EmployerOperation) : this()
    {
        SetEmployerConsideration(employerConsideration);
        SetEmployerOperation(EmployerOperation);
    }

    private void SetEmployerOperation(EmployerOperation value)
    {
        EmployerOperation = Guard.Against.Null(value, nameof(value));
        EmployerOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetEmployerConsideration(EmployerConsideration value)
    {
        EmployerConsideration = Guard.Against.Null(value, nameof(value));
        EmployerConsiderationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }


    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<EmployerConsiderationDep> _employerConsiderationDeps;
    public IReadOnlyList<EmployerConsiderationDep> EmployerConsiderationDeps => _employerConsiderationDeps;
    private EmployerConsiderationDep()
    {
        _employerConsiderationDeps = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
