using Engineering.Domain.Entities.ContractorMachineries.Enums;
using Engineering.Domain.Entities.Machineries;

namespace Engineering.Domain.Entities.ContractorMachineries;

/// <summary>
/// موجودی ماشین آلات پیمانکار 
/// </summary>
public class ContractorMachinery : AuditableEntity<ContractorMachinery>
{
    #region Properties
    [Description(GlobalCmts.ContractorId)]
    public long ContractorId { get; private set; }

    [Description(ContractorMachineryCmts.MachineryPrice)]
    public decimal MachineryPrice { get; private set; }

    [Description(CCCmts.CurrencyId)]
    public long CurrencyId { get; private set; }

    [Description(ContractorMachineryCmts.Unit)]
    public ContractorMachineryUnit Unit { get; private set; }

    [Description(ContractorMachineryCmts.NumberPlates)]
    public string? NumberPlates { get; private set; }

    [Description(ContractorMachineryCmts.MachineryIdentifier)]
    public string? MachineryIdentifier { get; private set; }

    [Description(ContractorMachineryCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(ContractorMachineryCmts.Description)]
    public string? Description { get; private set; }

    [Description(ContractorMachineryCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(ContractorMachineryCmts.Machinery)]
    public long MachineryId { get; set; }
    public Machinery Machinery { get; set; }

    #endregion

    #region Constructors

    public ContractorMachinery(Machinery machinery, long contractorId, decimal machineryPrice, long currencyId, ContractorMachineryUnit unit, string? numberPlates,
        string? machineryIdentifier, bool isActive, string? description, long? companyId) : this()
    {
        SetMachinery(machinery);
        SetContractorId(contractorId);
        SetMachineryPrice(machineryPrice);
        SetCurrencyId(currencyId);
        SetContractorMachineryUnit(unit);
        SetNumberPlates(numberPlates);
        SetMachineryIdentifier(machineryIdentifier);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
        SetDescription(description);
        SetCompanyId(companyId);
    }

    #endregion

    #region Command

    public void SetContractorMachineryUnit(ContractorMachineryUnit value)
    {
        Unit = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetContractorId(long value)
    {
        ContractorId = Guard.Against.Null(value, nameof(value));
    }

    public void SetMachinery(Machinery value)
    {
        Machinery = Guard.Against.Null(value, nameof(value));
        MachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCurrencyId(long value)
    {
        CurrencyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetMachineryIdentifier(string? value)
    {
        MachineryIdentifier = value;
    }

    public void SetNumberPlates(string? value)
    {
        NumberPlates = value;
    }

    public void SetMachineryPrice(decimal value)
    {
        MachineryPrice = Guard.Against.Null(value, nameof(value)); ;
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<RequestMachinery> _requestMachineries;
    public IReadOnlyList<RequestMachinery> RequestMachineries => _requestMachineries;
    private ContractorMachinery()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _requestMachineries = [];
    }

}
