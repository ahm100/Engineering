using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerContractHead)]
public class EmployerContractHead : AuditableEntity<EmployerContractHead, long>
{
    [Description(EContractCmts.Code)]
    public string? Code { get; private set; }
    [Description(EContractCmts.VolumeTolerance)]
    public decimal? VolumeTolerance { get; private set; }
    [Description(EContractCmts.PriceTolerance)]
    public decimal? PriceTolerance { get; private set; }
    [Description(EContractCmts.EmployerId)]
    public long EmployerId { get; private set; }
    [Description(EContractCmts.CurrencyId)]
    public long CurrencyId { get; private set; }
    [Description(GlobalCmts.StartDate)]
    public DateTime? StartDate { get; private set; }
    [Description(GlobalCmts.EndDate)]
    public DateTime? EndDate { get; private set; }
    [Description(EContractCmts.EContractType)]
    public EContractType Type { get; private set; }

    [Description(GlobalCmts.CompanyId)]
    public long CompanyId { get; private set; }

    [Description(GlobalCmts.CostCenterId)]
    public long CostCenterId { get; set; }
    public CostCenter CostCenter { get; set; }

    public EmployerContractHead(
        CostCenter costCenter,
        long employerId,
        long currencyId,
        string? code,
        decimal? volumeTolerance,
        EContractType contractType,
        long companyId) : this()
    {
        SetCostCenter(costCenter);
        SetEmployerId(employerId);
        SetCode(code);
        SetVolumeTolerance(volumeTolerance);
        SetCurrencyId(currencyId);
        SetContractType(contractType);
        SetStartDate();
        SetEndDate();

        CompanyId = companyId;
    }

    public void Update(
        string? code,
        decimal? volumeTolerance,
        EContractType contractType)
    {
        SetCode(code);
        SetVolumeTolerance(volumeTolerance);
        SetContractType(contractType);
        SetStartDate();
        SetEndDate();
    }

    public void UpdateVolume(
        decimal? volumeTolerance)
    {
        SetVolumeTolerance(volumeTolerance);
    }

    public void UpdateCost(
        decimal? priceTolerance)
    {
        SetPriceTolerance(priceTolerance);
    }

    #region Methods 
    private void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetEmployerId(long value)
    {
        EmployerId = value;
    }

    private void SetCode(string? value)
    {
        Code = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    private void SetVolumeTolerance(decimal? value)
    {
        VolumeTolerance = value;
    }

    private void SetPriceTolerance(decimal? value)
    {
        PriceTolerance = value;
    }

    private void SetCurrencyId(long value)
    {
        CurrencyId = Guard.Against.Null(value, nameof(value));
    }

    private void SetStartDate()
    {
        if (_employerContracts.Any(x => x.StartDate.HasValue))
            StartDate = _employerContracts.Where(x => x.StartDate != null).Min(x => x.StartDate);
    }

    private void SetEndDate()
    {
        if (_employerContracts.Any(x => x.EndDate.HasValue))
            EndDate = _employerContracts.Where(x => x.EndDate != null).Max(x => x.EndDate);
    }

    private void SetContractType(EContractType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }
    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<EmployerContract> _employerContracts;
    public IReadOnlyList<EmployerContract> EmployerContracts => _employerContracts;
    private EmployerContractHead()
    {
        _employerContracts = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
