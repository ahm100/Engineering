namespace Engineering.Domain.Entities.RequestMachineries;

[Description(RequestMachineryCmts.RequestMachineryInquiryOperator)]
public class RequestMachineryInquiryOperator : AuditableEntity<RequestMachineryInquiryOperator>
{
    #region Properties

    [Description(RequestMachineryCmts.OperatorAppoinmentId)]
    public long OperatorAppoinmentId { get; private set; }

    [Description(RequestMachineryCmts.OperatorAppoinmentUserId)]
    public long OperatorAppoinmentUserId { get; private set; }

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(GlobalCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    #endregion

    public RequestMachineryInquiryOperator(long operatorAppoinmentId,
        long operatorAppoinmentUserId,
        RequestMachinery requestMachinery) : this()
    {
        SetOperatorAppoinmentId(operatorAppoinmentId);
        SetOperatorAppoinmentUserId(operatorAppoinmentUserId);
        SetRequestMachinery(requestMachinery);
    }

    #region Commands

    public void SetData(long operatorAppoinmentId,
        long operatorAppoinmentUserId,
        RequestMachinery requestMachinery)
    {
        SetOperatorAppoinmentId(operatorAppoinmentId);
        SetOperatorAppoinmentUserId(operatorAppoinmentUserId);
        SetRequestMachinery(requestMachinery);
    }

    public void SetActive()
    {
        IsActive = true;
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetOperatorAppoinmentId(long value)
    {
        OperatorAppoinmentId = Guard.Against.Null(value, nameof(value));
    }

    public void SetOperatorAppoinmentUserId(long value)
    {
        OperatorAppoinmentUserId = Guard.Against.Null(value, nameof(value));
    }

    public void SetInActive()
    {
        IsActive = false;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    #region Constructors

    private List<RequestMachineryInquiry> _inquiries;
    public IReadOnlyList<RequestMachineryInquiry> Inquiries => _inquiries;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private RequestMachineryInquiryOperator()
    {
        _inquiries = [];
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion

}
