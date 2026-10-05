using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Domain.Entities.RequestMachineries;

[Description(GlobalCmts.Histories)]
public class RequestMachineryHistory : AuditableEntity<RequestMachineryHistory>
{
    #region Properties

    [Description(GlobalCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; } = null!;

    [Description(GlobalCmts.Status)]
    public RequestMachineryStatus Status { get; private set; } = RequestMachineryStatus.New;

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(RequestMachineryCmts.OperatorAppoinmentId)]
    public long? OperatorAppoinmentId { get; private set; }

    [Description(RequestMachineryCmts.OperatorAppoinmentUserId)]
    public long? OperatorAppoinmentUserId { get; private set; }

    [Description(RequestMachineryCmts.RequestDescription)]
    public string? RequestDescription { get; private set; }

    [Description(RequestMachineryCmts.ConfirmedDescription)]
    public string? ConfirmedDescription { get; private set; }

    [Description(RequestMachineryCmts.ManagerDescription)]
    public string? ManagerDescription { get; private set; }

    [Description(RequestMachineryCmts.ConfirmFromDate)]
    public DateTime? ConfirmFromDate { get; private set; }

    [Description(RequestMachineryCmts.ConfirmToDate)]
    public DateTime? ConfirmToDate { get; private set; }

    [Description(RequestMachineryCmts.ConfirmedTimeRequired)]
    public decimal? ConfirmedTimeRequired { get; private set; }

    [Description(GlobalCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(RequestMachineryCmts.MachineryId)]
    public long? MachineryId { get; private set; }

    #endregion

    #region Constructors

    public RequestMachineryHistory(
        RequestMachineryStatus status,
        string? description,
        long? operatorAppoinmentId,
        long? operatorAppoinmentUserId,
        string? requestDescription,
        string? confirmedDescription,
        string? managerDescription,
        DateTime? confirmFromDate,
        DateTime? confirmToDate,
        decimal? confirmedTimeRequired,
        long? contractorId,
        long? machineryId,
        RequestMachinery requestMachinery)
    {
        SetRequestMachinery(requestMachinery);
        SetStatus(status);
        SetDescription(description);
        SetOperatorAppoinmentId(operatorAppoinmentId);
        SetOperatorAppoinmentUserId(operatorAppoinmentUserId);
        SetRequestDescription(requestDescription);
        SetConfirmedDescription(confirmedDescription);
        SetManagerDescription(managerDescription);
        SetConfirmFromDate(confirmFromDate);
        SetConfirmToDate(confirmToDate);
        SetConfirmedTimeRequired(confirmedTimeRequired);
        SetContractorId(contractorId);
        SetMachineryId(machineryId);
    }

    #endregion

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetStatus(RequestMachineryStatus value)
    {
        Status = Guard.Against.EnumOutOfRange(value, nameof(value));
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetOperatorAppoinmentId(long? value)
    {
        OperatorAppoinmentId = value;
    }

    public void SetOperatorAppoinmentUserId(long? value)
    {
        OperatorAppoinmentUserId = value;
    }

    public void SetRequestDescription(string? value)
    {
        RequestDescription = value;
    }

    public void SetConfirmedDescription(string? value)
    {
        ConfirmedDescription = value;
    }

    public void SetManagerDescription(string? value)
    {
        ManagerDescription = value;
    }

    public void SetConfirmFromDate(DateTime? value)
    {
        ConfirmFromDate = value;
    }

    public void SetConfirmToDate(DateTime? value)
    {
        ConfirmToDate = value;
    }

    public void SetConfirmedTimeRequired(decimal? value)
    {
        ConfirmedTimeRequired = value;
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void SetMachineryId(long? value)
    {
        MachineryId = value;
    }

    private RequestMachineryHistory() { }
}
