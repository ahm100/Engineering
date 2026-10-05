namespace Engineering.Domain.Entities.OperationInfoHistorys;

[Description(OperationInfoCmts.OperationInfoHistory)]
public class OperationInfoHistory : AuditableEntity<OperationInfoHistory>
{
    [Description(OperationInfoCmts.OperationInfoName)]
    public string OperationInfoName { get; private set; } = string.Empty;
    [Description(OperationInfoCmts.OperationInfoCode)]
    public string OperationInfoCode { get; private set; } = string.Empty;
    [Description(OperationInfoCmts.OperationLatinName)]
    public string? OperationLatinName { get; private set; } = string.Empty;
    [Description(OperationInfoCmts.Priority)]
    public int? Priority { get; private set; }
    [Description(OperationInfoCmts.BasePrice)]
    public decimal BasePrice { get; private set; } = 0;
    [Description(OperationInfoCmts.UnitOfMeasurementId)]
    public long UnitOfMeasurementId { get; private set; }
    [Description(OperationInfoCmts.HaveStandard)]
    public bool HaveStandard { get; private set; } = false;
    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;
    [Description(GlobalCmts.OperationInfo)]
    public OperationInfo OperationInfo { get; set; }
    public long OperationInfoId { get; set; }
    [Description(OperationInfoCmts.IsPriceList)]
    public bool IsPriceList { get; private set; } = false;
    public OperationInfoHistory(
        OperationInfo operationInfo,
        string operationInfoName,
        string operationInfoCode,
        string? operationLatinName,
        decimal basePrice,
        int? priority,
        long unitOfMeasurement,
        bool isPriceList,
        bool isActive) : this()
    {
        SetOperationInfo(operationInfo);
        SetName(operationInfoName);
        SetCode(operationInfoCode);
        SetPriority(priority);
        SetUnitOfMeasurement(unitOfMeasurement);
        SetLatinName(operationLatinName);
        SetIsPriceList(isPriceList);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
    }

    #region Set data 
    public void SetIsPriceList(bool value)
    {
        IsPriceList = Guard.Against.Null(value, nameof(value));
    }
    public void SetOperationInfo(OperationInfo value)
    {
        OperationInfo = Guard.Against.Null(value, nameof(value));
        OperationInfoId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetPriority(int? value)
    {
        Priority = value;
    }
    public void SetBasePrice(decimal value)
    {
        BasePrice = Guard.Against.Null(value, nameof(value));
    }
    public void SetName(string value)
    {
        OperationInfoName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        OperationInfoCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetLatinName(string? value)
    {
        OperationLatinName = value;
    }
    public void SetUnitOfMeasurement(long value)
    {
        UnitOfMeasurementId = Guard.Against.Null(value, nameof(value));
    }
    public void SetActive()
    {
        IsActive = true;
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

    #region Methods 

    public static OperationInfoHistory Create(
        OperationInfo operationInfo,
        string operationInfoHistoryName,
        string operationInfoHistoryCode,
        string? operationLatinName,
        decimal basePrice,
        int? priority,
        long unitOfMeasurement,
        bool isPriceList,
        bool isActive)
    {
        return new OperationInfoHistory(
            operationInfo,
            operationInfoHistoryName,
            operationInfoHistoryCode,
            operationLatinName,
            basePrice,
            priority,
            unitOfMeasurement,
            isPriceList,
            isActive);
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private OperationInfoHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
