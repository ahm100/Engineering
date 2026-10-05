namespace Engineering.Application.Services.TransportationRequests.Models.Model;

public class SnapRequest
{
    public long? Id { get; set; }
    public long TripId { get; set; }
    public List<long>? CostCenterIds { get; set; }
    public List<long>? ProjectIds { get; set; }
    public long? TransportationCostCategoryId { get; set; }
    public long? TransportationCostGroupId { get; set; }
    public long? StartingCityId { get; set; }
    public long? DestinationCityId { get; set; }
    public long? SecondDestinationCityId { get; set; }
    public long? SnapRequester { get; set; }
    public long? DriverId { get; set; }
    public long? CurrencyUnitId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public string? NumberPlates { get; set; }
}