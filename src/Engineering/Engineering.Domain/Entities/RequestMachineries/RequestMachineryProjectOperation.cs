using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.RequestMachineries;

[Description(RequestMachineryCmts.RequestMachineryProjectOperation)]
public class RequestMachineryProjectOperation : AuditableEntity<RequestMachineryProjectOperation>
{
    #region Properties

    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    [Description(GlobalCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    #endregion

    public RequestMachineryProjectOperation(ProjectOperation projectOperation,
        RequestMachinery requestMachinery) : this()
    {
        SetProjectOperation(projectOperation);
        SetRequestMachinery(requestMachinery);
    }


    #region Commands

    public void SetData(ProjectOperation projectOperation,
        RequestMachinery requestMachinery)
    {
        SetProjectOperation(projectOperation);
        SetRequestMachinery(requestMachinery);
    }

    public void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryProjectOperation() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion

}
