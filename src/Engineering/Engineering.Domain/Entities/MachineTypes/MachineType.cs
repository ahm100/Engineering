using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.MachineTypes;

[Description(MachineTypeCmts.MachineType)]
public class MachineType : AuditableEntity<MachineType>
{
    [Description(GlobalCmts.Code)]
    public string MachineTypeCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.Code)]
    public string MachineTypeTitle { get; private set; } = string.Empty;
    [Description(MachineTypeCmts.FromWeight)]
    public int FromWeight { get; private set; }
    [Description(MachineTypeCmts.UntilWeight)]
    public int UntilWeight { get; private set; }
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(MachineTypeCmts.CabinType)]
    public long CabinTypeId { get; set; }
    public CabinType CabinType { get; set; }

    public MachineType(
        string code,
        string title,
        int fromWeight,
        int untilWeight,
        CabinType cabinType,
        bool isActive,
        long? companyId) : this()
    {
        SetCode(code);
        SetName(title);
        SetFromWeight(fromWeight);
        SetUntilWeight(untilWeight);
        SetCabinType(cabinType);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
        SetCompanyId(companyId);
    }

    #region Set data

    public void SetName(string value)
    {
        MachineTypeTitle = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetCode(string value)
    {
        MachineTypeCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetFromWeight(int value)
    {
        FromWeight = Guard.Against.Null(value, nameof(value));
    }
    public void SetCabinType(CabinType value)
    {
        CabinType = Guard.Against.Null(value, nameof(value));
    }
    public void SetUntilWeight(int value)
    {
        UntilWeight = Guard.Against.Null(value, nameof(value));
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetActive()
    {
        IsActive = true;
    }
    public void SetInActive()
    {
        IsActive = false;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<TransportationRequest> _transportationRequest;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequest;

    private List<TransportationContractorMachine> _transportationContractorMachines;
    public IReadOnlyList<TransportationContractorMachine> TransportationContractorMachines => _transportationContractorMachines;

    [Description(TransportationContractorCmts.ShippingCosts)]
    private List<ShippingCost> _shippingCosts;
    public IReadOnlyList<ShippingCost> ShippingCosts => _shippingCosts;

    private List<ShippingCostHistory> _shippingCostHistories;
    public IReadOnlyList<ShippingCostHistory> ShippingCostHistories => _shippingCostHistories;
    private MachineType()
    {
        _transportationRequest = [];
        _shippingCosts = [];
        _shippingCostHistories = [];
        _transportationContractorMachines = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
