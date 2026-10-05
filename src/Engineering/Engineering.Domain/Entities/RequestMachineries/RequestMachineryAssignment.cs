namespace Engineering.Domain.Entities.RequestMachineries;

public class RequestMachineryAssignment : AuditableEntity<RequestMachineryAssignment>
{
    [Description(RequestMachineryCmts.MachineryIdentifier)]
    public string MachineryIdentifier { get; private set; } = string.Empty;

    [Description(GlobalCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    public RequestMachineryAssignment(string machineryIdentifier,
        RequestMachinery requestMachinery) : this()
    {
        SetMachineryIdentifier(machineryIdentifier);
        SetRequestMachinery(requestMachinery);
    }

    #region Commands

    public void SetData(string machineryIdentifier,
        RequestMachinery requestMachinery)
    {
        SetMachineryIdentifier(machineryIdentifier);
        SetRequestMachinery(requestMachinery);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetMachineryIdentifier(string value)
    {
        MachineryIdentifier = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryAssignment() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
