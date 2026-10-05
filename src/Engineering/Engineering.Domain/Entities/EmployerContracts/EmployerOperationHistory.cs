namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerOperation)]
public class EmployerOperationHistory : AuditableEntity<EmployerOperationHistory>
{
    [Description(EContractCmts.UnitPrice)]
    public decimal UnitPrice { get; private set; }
    [Description(EContractCmts.Workload)]
    public decimal Workload { get; private set; }
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(EContractCmts.IncreaseRate)]
    public decimal IncreaseRate { get; private set; }
    [Description(EContractCmts.EmployerOperation)]
    public EmployerOperation EmployerOperation { get; private set; }
    public long EmployerOperationId { get; private set; }

    public EmployerOperationHistory(
        EmployerOperation employerOperation,
        decimal workload,
        decimal price,
        decimal increaseRate,
        string? description) : this()
    {
        SetEmployerOperation(employerOperation);
        SetWorkLoad(workload);
        SetPrice(price);
        SetDescription(description);
        SetIncreaseRate(increaseRate);
    }

    public EmployerOperationHistory Create(
        EmployerOperation employerOperation,
        decimal workload,
        decimal price,
        decimal increaseRate,
        string description)
    {
        return new EmployerOperationHistory(
            employerOperation,
            workload,
            price,
            increaseRate,
            description);
    }

    #region Set data

    private void SetWorkLoad(decimal value)
    {
        Workload = Guard.Against.Null(value, nameof(value));
    }

    private void SetDescription(string? value)
    {
        Description = value;
    }

    private void SetEmployerOperation(EmployerOperation value)
    {
        EmployerOperation = Guard.Against.Null(value, nameof(value));
        EmployerOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetPrice(decimal value)
    {
        UnitPrice = Guard.Against.Null(value, nameof(value));
    }

    private void SetIncreaseRate(decimal value)
    {
        IncreaseRate = Guard.Against.Null(value, nameof(value));
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private EmployerOperationHistory() { }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
