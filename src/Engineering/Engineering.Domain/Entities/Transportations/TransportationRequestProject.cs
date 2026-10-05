using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.Transportations;

/// <summary>
/// 
/// </summary>
public class TransportationRequestProject : AuditableEntity<TransportationRequestProject>
{
    [Description(GlobalCmts.TransportationRequest)]
    public long TransportationRequestId { get; set; }
    public TransportationRequest TransportationRequest { get; set; }

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; set; }
    public Project Project { get; set; }

    public TransportationRequestProject(TransportationRequest transportationRequest,
        Project project) : this()
    {
        SetTransportationRequest(transportationRequest);
        SetProject(project);
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

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    #region Methods 

    #endregion
    public void SetTransportRequest(TransportationRequest request)
    {
        TransportationRequest = request;
    }
    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestProject()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
