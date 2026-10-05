
namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerCostOverImpact)]
public class EmployerCostOverImpact : AuditableEntity<EmployerCostOverImpact, long>
{
    [Description(EContractCmts.Percent)]
    public decimal Percent { get; private set; }

    public long ParentCostOverId { get; set; }
    public EmployerCostOver ParentCostOver { get; set; }

    public long ChildCostOverId { get; set; }
    public EmployerCostOver ChildCostOver { get; set; }

    public EmployerCostOverImpact(
        EmployerCostOver parentCostOver,
        EmployerCostOver childCostOver,
        decimal percent) : this()
    {
        SetParentCostOver(parentCostOver);
        SetChildCostOver(childCostOver);

        SetPercent(percent);
    }

    private void SetParentCostOver(EmployerCostOver value)
    {
        ParentCostOver = Guard.Against.Null(value, nameof(value));
        ParentCostOverId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetChildCostOver(EmployerCostOver value)
    {
        ChildCostOver = Guard.Against.Null(value, nameof(value));
        ChildCostOverId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetPercent(decimal value)
    {
        Percent = Guard.Against.Null(value, nameof(value));
    }


    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerCostOverImpact() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
