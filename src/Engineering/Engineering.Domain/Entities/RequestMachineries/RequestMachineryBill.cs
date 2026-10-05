namespace Engineering.Domain.Entities.RequestMachineries;

[Description(RequestMachineryCmts.RequestMachineryBill)]
public class RequestMachineryBill : AuditableEntity<RequestMachineryBill>
{
    #region Properties

    [Description(RequestMachineryCmts.QRCodeUrl)]
    public string? QRCodeUrl { get; private set; }

    [Description(RequestMachineryCmts.BillNumber)]
    public long? BillNumber { get; private set; }

    [Description(RequestMachineryCmts.BillDate)]
    public DateTime BillDate { get; private set; }

    [Description(RequestMachineryCmts.SupplierId)]
    public long? SupplierId { get; private set; }

    [Description(RequestMachineryCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(RequestMachineryCmts.DriverId)]
    public long? DriverId { get; private set; }

    [Description(RequestMachineryCmts.DriverName)]
    public string? DriverName { get; private set; }

    [Description(RequestMachineryCmts.NumberPlate)]
    public string? NumberPlate { get; private set; }

    [Description(RequestMachineryCmts.MachineryAssignment)]
    public string? MachineryAssignment { get; private set; }

    [Description(RequestMachineryCmts.UnitPrice)]
    public decimal? UnitPrice { get; private set; }

    [Description(RequestMachineryCmts.TotalPrice)]
    public decimal? TotalPrice { get; private set; }

    [Description(RequestMachineryCmts.FromDate)]
    public DateTime? FromDate { get; private set; }

    [Description(RequestMachineryCmts.ToDate)]
    public DateTime? ToDate { get; private set; }

    [Description(RequestMachineryCmts.OperationDuration)]
    public long? OperationDuration { get; private set; }

    [Description(RequestMachineryCmts.Description)]
    public string? Description { get; private set; }

    [Description(RequestMachineryCmts.BillConfirmerId)]
    public long? BillConfirmerId { get; private set; }

    [Description(GlobalCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    #endregion

    public RequestMachineryBill(
        RequestMachinery requestMachinery,
        string? qRCodeUrl,
        DateTime billDate,
        long? contractorId,
        long? driverId,
        string? driverName,
        string? numberPlate,
        string? machineryAssignment,
        DateTime? fromDate,
        DateTime? toDate,
        long? operationDuration,
        string? description,
        long? supplierId,
        decimal? unitPrice,
        decimal? totalPrice,
        long? billConfirmerId
        ) : this()
    {
        SetRequestMachinery(requestMachinery);
        SetQRCodeUrl(qRCodeUrl);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetBillDate(billDate);
        SetContractorId(contractorId);
        SetDescription(description);
        SetDriverId(driverId);
        SetDriverName(driverName);
        SetNumberPlate(numberPlate);
        SetMachineryAssignment(machineryAssignment);
        SetOperationDuration(operationDuration);
        SetSupplierId(supplierId);
        SetUnitPrice(unitPrice);
        SetTotalPrice(totalPrice);
        SetBillConfirmerId(billConfirmerId);
    }

    public void SetData(
        RequestMachinery requestMachinery,
        string? qRCodeUrl,
        DateTime billDate,
        long? contractorId,
        long? driverId,
        string? driverName,
        string? numberPlate,
        string? machineryAssignment,
        DateTime? fromDate,
        DateTime? toDate,
        long? operationDuration,
        string? description,
        long? supplierId,
        decimal? unitPrice,
        decimal? totalPrice,
        long? billConfirmerId)
    {
        SetRequestMachinery(requestMachinery);
        SetQRCodeUrl(qRCodeUrl);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetBillDate(billDate);
        SetContractorId(contractorId);
        SetDescription(description);
        SetDriverId(driverId);
        SetDriverName(driverName);
        SetNumberPlate(numberPlate);
        SetMachineryAssignment(machineryAssignment);
        SetOperationDuration(operationDuration);
        SetSupplierId(supplierId);
        SetUnitPrice(unitPrice);
        SetTotalPrice(totalPrice);
        SetBillConfirmerId(billConfirmerId);
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetQRCodeUrl(string? value)
    {
        QRCodeUrl = value;
    }

    public void SetSupplierId(long? value)
    {
        SupplierId = value;
    }

    public void SetUnitPrice(decimal? value)
    {
        UnitPrice = value;
    }

    public void SetTotalPrice(decimal? value)
    {
        TotalPrice = value;
    }

    public void SetBillNumber(long value)
    {
        BillNumber = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public void SetFromDate(DateTime? value)
    {
        FromDate = value;
    }

    public void SetToDate(DateTime? value)
    {
        ToDate = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetBillDate(DateTime value)
    {
        BillDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void SetDriverId(long? value)
    {
        DriverId = value;
    }

    public void SetBillConfirmerId(long? value)
    {
        BillConfirmerId = value;
    }

    public void SetDriverName(string? value)
    {
        DriverName = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetNumberPlate(string? value)
    {
        NumberPlate = value;
    }

    public void SetMachineryAssignment(string? value)
    {
        MachineryAssignment = value;
    }

    public void SetOperationDuration(long? value)
    {
        OperationDuration = value;
    }

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryBill()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    #endregion
}
