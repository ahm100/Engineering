using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerDoc)]
public class EmployerCostOver : AuditableEntity<EmployerCostOver>
{
    [Description(EContractCmts.Percent)]
    public decimal Percent { get; private set; }
    [Description(EContractCmts.EmployerContract)]
    public long EmployerContractId { get; set; }
    public EmployerContract EmployerContract { get; set; }

    [Description(EContractCmts.CostOver)]
    public long CostOverId { get; set; }
    public CostOver CostOver { get; set; }

    public EmployerCostOver(
        EmployerContract contract,
        CostOver costOver,
        decimal percent) : this()
    {
        SetEmployerContract(contract);
        SetCostOver(costOver);
        SetPercent(percent);
    }

    public void Update(
        CostOver costOver,
        decimal percent)
    {
        SetCostOver(costOver);
        SetPercent(percent);
    }

    private void SetEmployerContract(EmployerContract value)
    {
        EmployerContract = Guard.Against.Null(value, nameof(value));
    }

    private void SetCostOver(CostOver value)
    {
        CostOver = Guard.Against.Null(value, nameof(value));
        CostOverId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetPercent(decimal value)
    {
        Percent = Guard.Against.Null(value, nameof(value));
    }

    public void AddCostOverImpact(EmployerCostOver childCostOver, decimal percent)
    {
        ArgumentNullException.ThrowIfNull(childCostOver);

        _childCostOverImpacts.Add(new(this, childCostOver, percent));
    }

    public void DeleteCostOverImpacts()
    {
        if (_childCostOverImpacts.Count > 0)
            foreach (var value in _childCostOverImpacts)
                value.SoftDelete();
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(EContractCmts.EmployerCostOverImpact)]
    private List<EmployerCostOverImpact> _parentCostOverImpacts;
    public IReadOnlyList<EmployerCostOverImpact> ParentCostOverImpacts => _parentCostOverImpacts;

    [Description(EContractCmts.EmployerCostOverImpact)]
    private List<EmployerCostOverImpact> _childCostOverImpacts;
    public IReadOnlyList<EmployerCostOverImpact> ChildCostOverImpacts => _childCostOverImpacts;

    private EmployerCostOver()
    {
        _parentCostOverImpacts = [];
        _childCostOverImpacts = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
