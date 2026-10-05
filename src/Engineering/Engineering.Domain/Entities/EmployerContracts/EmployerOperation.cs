using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerOperation)]
public class EmployerOperation : AuditableEntity<EmployerOperation, long>
{
    [Description(EContractCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; } = 0;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(EContractCmts.EmployerContract)]
    public long EmployerContractId { get; private set; }
    public EmployerContract EmployerContract { get; private set; }

    [Description(EContractCmts.IncreaseRate)]
    public decimal IncreaseRate { get; private set; }

    [Description(EContractCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    public EmployerOperation(
        EmployerContract employerContract,
        ProjectOperation projectOperation,
        string? description,
        List<EmployerConsideration>? considerations,
        List<long>? selectedDetails) : this()
    {
        SetEmployerContract(employerContract);
        SetProjectOperation(projectOperation);
        SetTotalPrice();
        SetDescription(description);
        SetIncreaseRate(1);
        if (considerations is not null && considerations.Count > 0)
            foreach (var item in considerations)
                AddEmployerConsiderationDeps(item);

        if (selectedDetails is not null && selectedDetails.Count > 0)
        {
            var details = projectOperation.ProjectOperationDetails
                .Where(x => selectedDetails.Contains(x.Id)).ToList();
            foreach (var item in details)
                AddOperationDetail(item);
        }
        else if (projectOperation.ProjectOperationDetails.Count > 0)
        {
            foreach (var item in projectOperation.ProjectOperationDetails)
                AddOperationDetail(item);
        }
    }

    public void AddHistory(
        decimal workLoad,
        decimal price,
        decimal increaseRate,
        string? description)
    {
        _employerOperationHistories.Add(new EmployerOperationHistory(
            this,
            workLoad,
            price,
            increaseRate,
            description));
    }

    public void Update(
        string? description)
    {
        SetDescription(description);
    }

    public void UpdateVolume(
        decimal workLoad,
        string? description)
    {
        ProjectOperation.UpdateVolume(workLoad);
        SetDescription(description);
        AddHistory(workLoad, ProjectOperation.Price ?? 0, IncreaseRate, description);
    }

    public void UpdateCost(
        decimal? price,
        decimal? tolerancePercentage,
        decimal increaseRate,
        string? description)
    {
        ProjectOperation.UpdateCost(price, tolerancePercentage);
        SetDescription(description);
        SetIncreaseRate(increaseRate);
    }

    private void SetEmployerContract(EmployerContract value)
    {
        EmployerContract = Guard.Against.Null(value, nameof(value));
        EmployerContractId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetTotalPrice()
    {
        TotalPrice = ProjectOperation.Workload * ProjectOperation.Price;
    }

    private void SetIncreaseRate(decimal value)
    {
        IncreaseRate = Guard.Against.Null(value, nameof(value));
    }


    private void SetDescription(string? value)
    {
        Description = value;
    }

    public void AddOperationDetail(ProjectOperationDetail detail)
    {
        ArgumentNullException.ThrowIfNull(detail);

        _employerOperationDetails.Add(new(this, detail));
    }

    public void AddEmployerConsiderationDeps(EmployerConsideration detail)
    {
        ArgumentNullException.ThrowIfNull(detail);

        _employerConsiderationDeps.Add(new(detail, this));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(EContractCmts.EmployerConsiderationDep)]
    private List<EmployerConsiderationDep> _employerConsiderationDeps;
    public IReadOnlyList<EmployerConsiderationDep> EmployerConsiderationDeps => _employerConsiderationDeps;

    [Description(EContractCmts.EmployerOperationDetail)]
    private List<EmployerOperationDetail> _employerOperationDetails;
    public IReadOnlyList<EmployerOperationDetail> EmployerOperationDetails => _employerOperationDetails;

    [Description(EContractCmts.EmployerOperationHistory)]
    private List<EmployerOperationHistory> _employerOperationHistories;
    public IReadOnlyList<EmployerOperationHistory> EmployerOperationHistories => _employerOperationHistories;

    [Description(EContractCmts.EmployerOperationProduct)]
    private List<EmployerOperationProduct> _employerOperationProducts;
    public IReadOnlyList<EmployerOperationProduct> EmployerOperationProducts => _employerOperationProducts;

    [Description(EContractCmts.EmployerOperationService)]
    private List<EmployerOperationService> _employerOperationServices;
    public IReadOnlyList<EmployerOperationService> EmployerOperationServices => _employerOperationServices;

    private EmployerOperation()
    {
        _employerConsiderationDeps = [];
        _employerOperationDetails = [];
        _employerOperationHistories = [];
        _employerOperationProducts = [];
        _employerOperationServices = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
