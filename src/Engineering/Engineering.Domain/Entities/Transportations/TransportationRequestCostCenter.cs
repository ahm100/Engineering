using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Domain.Entities.Transportations;

public class TransportationRequestCostCenter : AuditableEntity<TransportationRequestCostCenter>
{
    [Description(TransportationCmts.TransportationRequest)]
    public long TransportationRequestId { get; set; }
    public TransportationRequest TransportationRequest { get; set; }

    [Description(TransportationCmts.CostCenter)]
    public long CostCenterId { get; set; }
    public CostCenter CostCenter { get; set; }

    public TransportationRequestCostCenter(TransportationRequest transportationRequest,
        CostCenter costCenter) : this()
    {
        SetTransportationRequest(transportationRequest);
        SetCostCenter(costCenter);
    }

    #region Set data

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    public void SetTransportationRequest(TransportationRequest value)
    {
        TransportationRequest = Guard.Against.Null(value, nameof(value));
        TransportationRequestId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetTransportRequest(TransportationRequest request)
    {
        TransportationRequest = request;
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestCostCenter()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
