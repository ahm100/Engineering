using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.Transportations;

public class TransportationRequestProjectOperation : AuditableEntity<TransportationRequestProjectOperation>
{
    [Description(GlobalCmts.TransportationRequest)]
    public long TransportationRequestId { get; set; }
    public TransportationRequest TransportationRequest { get; set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; set; }
    public ProjectOperation ProjectOperation { get; set; }

    public TransportationRequestProjectOperation(TransportationRequest transportationRequest,
        ProjectOperation projectOperation) : this()
    {
        SetTransportationRequest(transportationRequest);
        SetProjectOperation(projectOperation);
    }

    #region Set data

    public void SetTransportationRequest(TransportationRequest value)
    {
        TransportationRequest = Guard.Against.Null(value, nameof(value));
        TransportationRequestId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestProjectOperation()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
