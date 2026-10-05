namespace Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;


public class GetWarehouseByIdsModel
{
    public long? Id { get; set; }
    public long? WarehouseTypeId { get; set; }
    public string? WarehouseTypeName { get; set; } = string.Empty;
    public long? WarehouseNatureId { get; set; }
    public string? WarehouseNatureName { get; set; } = string.Empty;

    public long? ManagerId { get; set; }
    public string? ManagerFullName { get; set; } = string.Empty;
    public long? OwnerId { get; set; }
    public long? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public string? OwnerFullName { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Contact { get; set; } = string.Empty;
    public double? MinimumTemperature { get; set; }
    public double? MaximumTemperature { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsMain { get; set; }
    public bool? IsReference { get; set; }
    public GetWarehousesByIdsAddressModel Address { get; set; } = new();
}

public class GetWarehousesByIdsAddressModel
{
    public long Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? AddressText { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    public string? ApartmentNo { get; set; } = string.Empty;
    public string? BuzzerNo { get; set; } = string.Empty;
    public string? FloorNo { get; set; } = string.Empty;
    public long CityId { get; set; }
    public bool IsActive { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CityName { get; set; } = string.Empty;
}