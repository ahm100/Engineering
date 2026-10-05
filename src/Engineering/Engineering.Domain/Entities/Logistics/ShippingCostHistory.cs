using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;
using Engineering.Domain.Entities.Synonyms.MetaData.Regions;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Logistics;

public class ShippingCostHistory : ActivateEntity<ShippingCostHistory, long>
{
    [Description(ShippingCostCmts.ShippingCost)]
    [ForeignKey("ShippingCost")]
    public long ShippingCostId { get; set; }
    public ShippingCost ShippingCost { get; set; }

    [Description(ShippingCostCmts.TransportationContractor)]
    [ForeignKey("TransportationContractor")]
    public long TransportationContractorId { get; set; }

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
    public ViewThirdParty? Creator { get; private set; }
    public long? ThirdPartyCompanyId { get; private set; }

    [Description(ShippingCostCmts.Latitude)]
    public decimal? Latitude { get; private set; }

    [Description(ShippingCostCmts.Longitude)]
    public decimal? Longitude { get; private set; }

    [Description(ShippingCostCmts.FromDate)]
    public DateTime? FromDate { get; set; }

    [Description(ShippingCostCmts.ToDate)]
    public DateTime? ToDate { get; set; }

    [Description(ShippingCostCmts.Tax)]
    public string? Description { get; private set; }

    public ShippingCostHistory(
        ShippingCost shippingCost,
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
        decimal? longitude) : this()
    {
        SetShippingCost(shippingCost);
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

        SetActive();
    }

    public void SetShippingCost(ShippingCost value)
    {
        ShippingCost = Guard.Against.Null(value, nameof(value));
        ShippingCostId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetTransportationContractor(TransportationContractor transportationContractor)
    {
        TransportationContractorId = Guard.Against.Null(transportationContractor, nameof(transportationContractor)).Id;
    }

    public void SetMachineType(MachineType machineType)
    {
        MachineTypeId = Guard.Against.Null(machineType, nameof(machineType)).Id;
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ShippingCostHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}

