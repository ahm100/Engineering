using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Domain.Entities.RequestMachineries;

[Description(GlobalCmts.RequestMachinery)]
public class RequestMachinery : AuditableEntity<RequestMachinery>
{
    #region Properties

    [Description(RequestMachineryCmts.TimeRequired)]
    public decimal TimeRequired { get; private set; }

    [Description(RequestMachineryCmts.Unit)]
    public RequestMachineryUnit Unit { get; private set; }

    [Description(RequestMachineryCmts.Status)]
    public RequestMachineryStatus Status { get; private set; } = RequestMachineryStatus.New;

    [Description(RequestMachineryCmts.RequestCount)]
    public int RequestCount { get; private set; }

    [Description(RequestMachineryCmts.FromDate)]
    public DateTime? FromDate { get; private set; }

    [Description(RequestMachineryCmts.ToDate)]
    public DateTime? ToDate { get; private set; }

    [Description(RequestMachineryCmts.Description)]
    public string? Description { get; private set; }

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

    [Description(RequestMachineryCmts.Project)]
    public Project Project { get; private set; } = null!;

    [Description(RequestMachineryCmts.Machinery)]
    public long MachineryId { get; private set; }
    public Machinery Machinery { get; private set; }

    [Description(RequestMachineryCmts.ContractorMachinery)]
    public long? ContractorMachineryId { get; private set; }
    public ContractorMachinery? ContractorMachinery { get; private set; }

    [Description(RequestMachineryCmts.OperatorAppoinmentId)]
    public long? OperatorAppoinmentId { get; private set; }

    [Description(RequestMachineryCmts.OperatorAppoinmentUserId)]
    public long? OperatorAppoinmentUserId { get; private set; }

    [Description(RequestMachineryCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(RequestMachineryCmts.DriverId)]
    public long? DriverId { get; private set; }

    [Description(RequestMachineryCmts.DriverName)]
    public string? DriverName { get; private set; }

    [Description(RequestMachineryCmts.RequestNumber)]
    public long? RequestNumber { get; private set; }

    [Description(RequestMachineryCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    #endregion

    public RequestMachinery(long? requestNumber,
        decimal timeRequired,
        RequestMachineryUnit unit,
        int requestCount,
        DateTime? fromDate,
        DateTime? toDate,
        string? description,
        Machinery machinery,
        long? companyId) : this()
    {
        SetTimeRequired(timeRequired);
        SetUnit(unit);
        SetRequestCount(requestCount);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetDescription(description);
        SetCompanyId(companyId);
        SetMachinery(machinery);
        SetRequestNumber(requestNumber);
    }

    public void SetData(long? requestNumber, decimal timeRequired, RequestMachineryUnit unit, int requestCount, DateTime? fromDate, DateTime? toDate, string? description, Machinery machinery, long? companyId)
    {
        SetTimeRequired(timeRequired);
        SetUnit(unit);
        SetRequestCount(requestCount);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetDescription(description);
        SetCompanyId(companyId);
        SetMachinery(machinery);
        SetRequestNumber(requestNumber);
    }

    public void AddHistory(string? requestDescription)
    {
        _histories.Add(new RequestMachineryHistory(
            Status,
            Description,
            OperatorAppoinmentId,
            OperatorAppoinmentUserId,
            requestDescription,
            ConfirmedDescription,
            ManagerDescription,
            ConfirmFromDate,
            ConfirmToDate,
            ConfirmedTimeRequired,
            ContractorId,
            Machinery.Id,
            this));
    }

    public void ChangeStatus(RequestMachineryStatus value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Status = value;
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void SetRequestNumber(long? value)
    {
        RequestNumber = value;
    }

    public void SetContractorMachinery(ContractorMachinery? value)
    {
        ContractorMachinery = value;
        ContractorMachineryId = value?.Id;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetTimeRequired(decimal value)
    {
        TimeRequired = Guard.Against.Null(value, nameof(value));
    }

    public void SetConfirmedTimeRequired(decimal? value)
    {
        ConfirmedTimeRequired = value;
    }

    public void SetConfirmedDescription(string? value)
    {
        ConfirmedDescription = value;
    }

    public void SetManagerDescription(string? value)
    {
        ManagerDescription = value;
    }

    public void SetUnit(RequestMachineryUnit value)
    {
        Unit = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestCount(int value)
    {
        RequestCount = Guard.Against.Null(value, nameof(value));
    }

    public void SetFromDate(DateTime? value)
    {
        FromDate = value;
    }

    public void SetToDate(DateTime? value)
    {
        ToDate = value;
    }
    public void SetConfirmFromDate(DateTime? value)
    {
        ConfirmFromDate = value;
    }

    public void SetConfirmToDate(DateTime? value)
    {
        ConfirmToDate = value;
    }

    public void SetMachinery(Machinery value)
    {
        Machinery = Guard.Against.Null(value, nameof(value));
        MachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetDriverId(long? value)
    {
        DriverId = value;
    }
    public void SetDriverName(string? value)
    {
        DriverName = value;
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
    }

    public void SetOperatorAppoinmentId(long value)
    {
        OperatorAppoinmentId = Guard.Against.Null(value, nameof(value));
    }

    public void SetOperatorAppoinmentUserId(long value)
    {
        OperatorAppoinmentUserId = Guard.Against.Null(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private readonly List<RequestMachineryHistory> _histories;
    public IReadOnlyList<RequestMachineryHistory> Histories => _histories;

    private readonly List<RequestMachineryBill> _requestMachineryBills;
    public IReadOnlyList<RequestMachineryBill> RequestMachineryBills => _requestMachineryBills;

    private readonly List<RequestMachineryInquiryOperator> _inquiryOperators;
    public IReadOnlyList<RequestMachineryInquiryOperator> InquiryOperators => _inquiryOperators;

    private readonly List<RequestMachineryProjectOperation> _projectOperations;
    public IReadOnlyList<RequestMachineryProjectOperation> ProjectOperations => _projectOperations;

    private readonly List<RequestMachineryProjectOperationDetail> _projectOperationDetails;
    public IReadOnlyList<RequestMachineryProjectOperationDetail> ProjectOperationDetails => _projectOperationDetails;

    private readonly List<DailyProjectOperationMachinery> _dailyMachineries;
    public IReadOnlyList<DailyProjectOperationMachinery> DailyMachineries => _dailyMachineries;

    private readonly List<RequestMachineryAssignment> _requestMachineryAssignments;
    public IReadOnlyList<RequestMachineryAssignment> RequestMachineryAssignments => _requestMachineryAssignments;

    private readonly List<RequestMachineryDocument> _requestMachineryDocuments;
    public IReadOnlyList<RequestMachineryDocument> RequestMachineryDocuments => _requestMachineryDocuments;

    private readonly List<RequestMachineryBillDocument> _requestMachineryBillDocuments;
    public IReadOnlyList<RequestMachineryBillDocument> RequestMachineryBillDocuments => _requestMachineryBillDocuments;

    private readonly List<MachineryReservation> _machineryReservations;
    public IReadOnlyList<MachineryReservation> MachineryReservations => _machineryReservations;

    private readonly List<RequestMachineryStatusStatementDetail> _requestMachineryStatusStatementDetails;
    public IReadOnlyList<RequestMachineryStatusStatementDetail> RequestMachineryStatusStatementDetails => _requestMachineryStatusStatementDetails;

    private RequestMachinery()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _histories = [];
        _inquiryOperators = [];
        _projectOperations = [];
        _projectOperationDetails = [];
        _dailyMachineries = [];
        _requestMachineryAssignments = [];
        _requestMachineryDocuments = [];
        _requestMachineryBillDocuments = [];
        _machineryReservations = [];
        _requestMachineryStatusStatementDetails = [];
        _requestMachineryBills = [];
    }

    #endregion
}
