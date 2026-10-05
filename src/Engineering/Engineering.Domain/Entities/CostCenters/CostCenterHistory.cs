namespace Engineering.Domain.Entities.CostCenters;
[Description(GlobalCmts.CostCenterHistory)]
public class CostCenterHistory : ActivateEntity<CostCenterHistory, long>
{

    [Description(CCenterCmts.CostCenterCode)]
    public string CostCenterCode { get; private set; } = string.Empty;
    [Description(CCenterCmts.CostCenterName)]
    public string CostCenterName { get; private set; } = string.Empty;
    [Description(CCenterCmts.CostCenterEnName)]
    public string? CostCenterEnName { get; private set; } = string.Empty;
    [Description(CCenterCmts.NoOperationDays)]
    public int? NoOperationDays { get; private set; } = 0;
    [Description(GlobalCmts.CityId)]
    public long CityId { get; private set; }
    [Description(CCenterCmts.Address)]
    public string Address { get; private set; }
    [Description(CCenterCmts.PostalCode)]
    public string? PostalCode { get; private set; }
    [Description(CCenterCmts.Latitude)]
    public decimal Latitude { get; private set; } = decimal.Zero;
    [Description(CCenterCmts.Longitude)]
    public decimal Longitude { get; private set; } = decimal.Zero;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;
    [Description(GlobalCmts.Description)]
    public string? DescriptionEn { get; private set; } = string.Empty;
    [Description(CCenterCmts.WeatherState)]
    public bool? WeatherState { get; private set; } = false;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(CCenterCmts.PreferentialReferenceCode)]
    public Guid PreferentialReferenceCode { get; private set; }
    [Description(CCenterCmts.CostCenterType)]
    public long CostCenterTypesId { get; private set; }
    public CostCenterType CostCenterType { get; set; }
    [Description(GlobalCmts.CostCenter)]
    public CostCenter CostCenter { get; set; }
    public long CostCenterId { get; set; }
    [Description(CCenterCmts.IsDefault)]
    public bool IsDefault { get; private set; } = false;
    public CostCenterHistory(
        CostCenter costCenter) : this()
    {
        SetCostCenter(costCenter);
        SetCostCenterType(costCenter.CostCenterType);
        SetName(costCenter.CostCenterName);
        SetEnName(costCenter.CostCenterEnName);
        SetCode(costCenter.CostCenterCode);
        SetNoOperationDays(costCenter.NoOperationDays);
        SetCityId(costCenter.CityId);
        SetAddress(costCenter.Address);
        SetPostalCode(costCenter.PostalCode);
        SetCompanyId(costCenter.CompanyId);
        SetLatitude(costCenter.Latitude);
        SetLongitude(costCenter.Longitude);
        IsActive = costCenter.IsActive;
        SetDescription(costCenter.Description);
        SetDescriptionEn(costCenter.DescriptionEn);
        SetWeatherState(costCenter.WeatherState);
        SetPreferentialReferenceCode(costCenter.PreferentialReferenceCode);
        SetIsDefault(costCenter.IsDefault);
    }

    #region Set Date

    public void SetIsDefault(bool value)
    {
        IsDefault = Guard.Against.Null(value, nameof(value));
    }
    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetCostCenterType(CostCenterType value)
    {
        CostCenterTypesId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetPreferentialReferenceCode(Guid value)
    {
        PreferentialReferenceCode = Guard.Against.Null(value, nameof(value));
    }
    public void SetName(string value)
    {
        CostCenterName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        CostCenterCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetNoOperationDays(int? value)
    {
        NoOperationDays = value ?? 0;
    }

    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }
    public void SetEnName(string? value)
    {
        CostCenterEnName = value;
    }

    public void SetCityId(long value)
    {
        CityId = value;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetAddress(string value)
    {
        Address = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetPostalCode(string? value)
    {
        PostalCode = value;
    }
    public void SetLatitude(decimal? value)
    {
        Latitude = value ?? 0;
    }
    public void SetLongitude(decimal? value)
    {
        Longitude = value ?? 0;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetWeatherState(bool? value)
    {
        WeatherState = value;
    }

    #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CostCenterHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}