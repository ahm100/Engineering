namespace Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;

public record GetSupplierDriversModel
{
    public string? DriverName { get; set; }
}

public record GetRequestMachineryDriverModel
{
    public long? DriverId { get; set; }
    public string? DriverName { get; set; }
}

