using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Domain.Entities.Transportations;

/// <summary>
/// 
/// </summary>
public class TransportationRequestProjectOperationDetail : AuditableEntity<TransportationRequestProjectOperationDetail>
{
    [Description(GlobalCmts.TransportationRequest)]
    public long TransportationRequestId { get; set; }
    public TransportationRequest TransportationRequest { get; set; }

    [Description(GlobalCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; set; }
    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    public TransportationRequestProjectOperationDetail(TransportationRequest transportationRequest,
        ProjectOperationDetail projectOperationDetail) : this()
    {
        SetTransportationRequest(transportationRequest);
        SetProjectOperationDetail(projectOperationDetail);
    }

    #region Set data

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetTransportationRequest(TransportationRequest value)
    {
        TransportationRequest = Guard.Against.Null(value, nameof(value));
        TransportationRequestId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
        ProjectOperationDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestProjectOperationDetail()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
