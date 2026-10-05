using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerOperationDetail)]
public class EmployerOperationDetail : AuditableEntity<EmployerOperationDetail, long>
{
    [Description(EContractCmts.EmployerOperation)]
    public long EmployerOperationId { get; private set; }
    public EmployerOperation EmployerOperation { get; private set; }

    [Description(EContractCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    public EmployerOperationDetail(
        EmployerOperation operation,
        ProjectOperationDetail detail) : this()
    {
        SetEmployerOperation(operation);
        SetProjectOperationDetail(detail);
    }

    private void SetEmployerOperation(EmployerOperation value)
    {
        EmployerOperation = value;
        EmployerOperationId = value.Id;
    }

    private void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = value;
        ProjectOperationDetailId = value.Id;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerOperationDetail()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
