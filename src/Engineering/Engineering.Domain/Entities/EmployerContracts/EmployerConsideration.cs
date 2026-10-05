using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerConsideration)]
public class EmployerConsideration : ActivateEntity<EmployerConsideration, long>
{
    [Description(EContractCmts.EmployerConsideration)]
    public ConsiderationType Type { get; private set; }
    [Description(EContractCmts.EmployerConsideration)]
    public string? Description { get; private set; } = string.Empty;

    [Description(EContractCmts.EmployerContractId)]
    public long EmployerContractId { get; set; }
    public EmployerContract EmployerContract { get; set; }

    [NotMapped]
    public string? FlagId { get; private set; } = null;

    public EmployerConsideration(
        EmployerContract employerContract,
        ConsiderationType type,
        string? description,
        bool isActive,
        string? flag) : this()
    {
        SetEmployerContract(employerContract);
        SetType(type);
        SetDescription(description);

        if (isActive)
            SetActive();
        else
            SetDeactivate();

        FlagId = flag;
    }

    private void SetEmployerContract(EmployerContract value)
    {
        EmployerContract = Guard.Against.Null(value, nameof(value));
        EmployerContractId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void Update(
        ConsiderationType type,
        string? description,
        bool isActive,
        string? flag)
    {
        SetType(type);
        SetDescription(description);

        if (isActive)
            SetActive();
        else
            SetDeactivate();

        FlagId = flag;
    }

    private void SetDescription(string? value)
    {
        Description = value;
    }

    private void SetType(ConsiderationType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(EContractCmts.EmployerConsiderationDep)]
    private List<EmployerConsiderationDep> _employerConsiderationDeps;
    public IReadOnlyList<EmployerConsiderationDep> EmployerConsiderationDeps => _employerConsiderationDeps;
    private EmployerConsideration()
    {
        _employerConsiderationDeps = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
