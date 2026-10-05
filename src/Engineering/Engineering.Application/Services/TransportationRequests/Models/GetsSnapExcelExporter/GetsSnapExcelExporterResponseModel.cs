
namespace Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelExporter;

public record GetsSnapExcelExporterResponseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public string TransportationRequestStatusTitle { get; set; } = string.Empty;
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? StartDateShamsi { get; set; }
    public string? EndDateShamsi { get; set; }
    public string TransportationName { get; set; } = string.Empty;
    public string TripName { get; set; } = string.Empty;
    public string? SnapRequesterName { get; set; } = string.Empty;
    public string? Creator { get; set; }
    public string? CurrencyUnitName { get; set; }
    public string? StartingCityName { get; set; }
    public string? StartingCityAddress { get; set; }
    public string? DestinationCityName { get; set; }
    public string? DestinationAddress { get; set; }
    public string? SecondDestinationCityName { get; set; }
    public string? SecondDestinationAddress { get; set; }
    public string? CostCenterNames { get; set; }
    public string? ProjectNames { get; set; }
    public string? Description { get; set; }
    public long? DriverId { get; set; }
    public string? DriverUserName { get; set; }
    public string? DriverName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CarSpecifications { get; set; }
    public string? NumberPlates { get; set; }
    public decimal? FareAmount { get; set; }
    public int? StopRate { get; set; }
    public bool PersonalPayment { get; set; }
    public bool ReturnToStart { get; set; }
    public string? TransportationCostGroupTitle { get; set; }
    public string? TransportationCostGroupCode { get; set; }
    public string? TransportationCostCategoryTitle { get; set; }
    public string? TransportationCostCategoryCode { get; set; }
    public string? ManagerDescription { get; set; }
    public string? ConfrimName { get; set; }
    public string? ConfrimDateShamsi { get; set; }
    public string? RecipientName { get; set; }
};