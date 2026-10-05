using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;
using Engineering.Domain.Entities.Synonyms.MetaData.Regions;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Transportations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Logistics;

public class ShippingCost : ActivateEntity<ShippingCost, long>
{
    [Description(ShippingCostCmts.TransportationContractor)]
    [ForeignKey("TransportationContractor")]
    public long TransportationContractorId { get; set; }
    public TransportationContractor TransportationContractor { get; set; }

    [Description(ShippingCostCmts.MachineType)]
    [ForeignKey("MachineType")]
    public long MachineTypeId { get; set; }
    public MachineType MachineType { get; set; }

    [Description(ShippingCostCmts.SourceCity)]
    [ForeignKey("SourceCity")]
    public long? SourceCityId { get; set; }
    public ViewCity? SourceCity { get; set; }

    [Description(ShippingCostCmts.SourceCity)]
    [ForeignKey("Region")]
    public long? RegionId { get; set; }
    public ViewRegion? Region { get; set; }

    [Description(ShippingCostCmts.DestinationCity)]
    [ForeignKey("DestinationCity")]
    public long? DestinationCityId { get; set; }
    public ViewCity? DestinationCity { get; set; }

    [Description(ShippingCostCmts.Count)]
    public int? Count { get; private set; }

    [Description(ShippingCostCmts.LoadWeight)]
    public decimal? LoadWeight { get; private set; }

    [Description(ShippingCostCmts.Price)]
    public decimal Price { get; private set; }

    [Description(ShippingCostCmts.Tax)]
    public decimal? Tax { get; private set; }

    [ForeignKey("ThirdParty")]
    [Description(ShippingCostCmts.ThirdParty)]
    public long? ThirdPartyId { get; private set; }
    public ViewThirdParty? ThirdParty { get; private set; }
    public long? ThirdPartyCompanyId { get; private set; }

    [Description(ShippingCostCmts.Latitude)]
    public decimal? Latitude { get; private set; }

    [Description(ShippingCostCmts.Longitude)]
    public decimal? Longitude { get; private set; }

    public long? LegacyId { get; private set; }

    [Description(ShippingCostCmts.FromDate)]
    public DateTime? FromDate { get; set; }

    [Description(ShippingCostCmts.ToDate)]
    public DateTime? ToDate { get; set; }

    [Description(ShippingCostCmts.Tax)]
    public string? Description { get; private set; }

    public ShippingCost(
    TransportationContractor transportationContractor,
    MachineType machineType,
    long? sourceCityId,
    long? destinationCityId,
    long? regionId,
    int? count,
    decimal? loadWeight,
    decimal price,
    decimal? tax,
    string? description,
    DateTime? fromDate,
    DateTime? toDate,
    long? thirdPartyId,
    long? thirdPartyCompanyId,
    decimal? latitude,
    decimal? longitude,
    long? legacyId) : this()
    {
        SetTransportationContractor(transportationContractor);
        SetMachineType(machineType);
        SetSourceCityId(sourceCityId);
        SetDestinationCityId(destinationCityId);
        SetRegionId(regionId);
        SetCount(count);
        SetLoadWeight(loadWeight);
        SetPrice(price);
        SetTax(tax);
        SetDescription(description);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetThirdPartyId(thirdPartyId);
        SetThirdPartyCompanyId(thirdPartyCompanyId);
        SetLatitude(latitude);
        SetLongitude(longitude);
        SetLegacyId(legacyId);

        SetIsActive(true);
        SetIsDeleted(false);

        AddHistory();
    }

    public void Update(
        TransportationContractor transportationContractor,
        MachineType machineType,
        long? sourceCityId,
        long? destinationCityId,
        long? regionId,
        int? count,
        decimal? loadWeight,
        decimal price,
        decimal? tax,
        string? description,
        DateTime? fromDate,
        DateTime? toDate,
        long? thirdPartyId,
        long? thirdPartyCompanyId,
        decimal? latitude,
        decimal? longitude,
        bool isActive,
        long? legacyId)
    {
        SetTransportationContractor(transportationContractor);
        SetMachineType(machineType);
        SetSourceCityId(sourceCityId);
        SetDestinationCityId(destinationCityId);
        SetRegionId(regionId);
        SetCount(count);
        SetLoadWeight(loadWeight);
        SetPrice(price);
        SetTax(tax);
        SetDescription(description);
        SetFromDate(fromDate);
        SetToDate(toDate);
        SetThirdPartyId(thirdPartyId);
        SetThirdPartyCompanyId(thirdPartyCompanyId);
        SetLatitude(latitude);
        SetLongitude(longitude);
        SetLegacyId(legacyId);

        SetIsActive(isActive);
        SetIsDeleted(false);

        AddHistory();
    }

    public void SetTransportationContractor(TransportationContractor value)
    {
        TransportationContractor = Guard.Against.Null(value, nameof(value));
        TransportationContractorId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetMachineType(MachineType value)
    {
        MachineType = Guard.Against.Null(value, nameof(value));
        MachineTypeId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetSourceCityId(long? sourceCityId)
    {
        SourceCityId = sourceCityId;
    }

    public void SetDestinationCityId(long? destinationCityId)
    {
        DestinationCityId = destinationCityId;
    }

    public void SetRegionId(long? regionId)
    {
        RegionId = regionId;
    }

    public void SetCount(int? count)
    {
        Count = count;
    }

    public void SetLoadWeight(decimal? loadWeight)
    {
        LoadWeight = loadWeight;
    }

    public void SetPrice(decimal price)
    {
        Price = Guard.Against.NegativeOrZero(price, nameof(price));
    }

    public void SetTax(decimal? tax)
    {
        Tax = tax;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetFromDate(DateTime? fromDate)
    {
        FromDate = fromDate;
    }

    public void SetToDate(DateTime? toDate)
    {
        ToDate = toDate;
    }

    public void SetThirdPartyId(long? thirdPartyId)
    {
        ThirdPartyId = thirdPartyId;
    }

    public void SetThirdPartyCompanyId(long? thirdPartyCompanyId)
    {
        ThirdPartyCompanyId = thirdPartyCompanyId;
    }

    public void SetLatitude(decimal? latitude)
    {
        Latitude = latitude;
    }

    public void SetLongitude(decimal? longitude)
    {
        Longitude = longitude;
    }

    public void SetLegacyId(long? legacyId)
    {
        LegacyId = legacyId;
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetIsDeleted(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }

    public void AddHistory()
    {
        _shippingCostHistories.Add(new ShippingCostHistory(this, TransportationContractor,
            MachineType, SourceCityId, DestinationCityId, RegionId, Count, LoadWeight, Price,
            Tax, Description, FromDate, ToDate, ThirdPartyId, ThirdPartyCompanyId, Latitude, Longitude));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private List<TransportationCargoPallet> _transportationCargoPallets;
    public IReadOnlyList<TransportationCargoPallet> TransportationCargoPallets => _transportationCargoPallets;

    private List<ShippingCostHistory> _shippingCostHistories;
    public IReadOnlyList<ShippingCostHistory> ShippingCostHistories => _shippingCostHistories;
    private ShippingCost()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _transportationCargoPallets = [];
        _shippingCostHistories = [];
    }
}

