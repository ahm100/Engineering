namespace Engineering.Application.Services.TransportationRequests.Models.Model;

public class AirplaneRequest
{
    public long? Id { get; set; }
    public long TripId { get; set; }
    public List<long>? CostCenterIds { get; set; }
    public List<long>? ProjectIds { get; set; }
    public List<long>? ProjectOperationIds { get; set; }
    public List<long>? ProjectOperationDetailIds { get; set; }
    public long? TransportationCostCategoryId { get; set; }
    public long? TransportationCostGroupId { get; set; }
    public long StartingCityId { get; set; }
    public long DestinationCityId { get; set; }
    public long? PassengerId { get; set; }
    public long? TicketPayerId { get; set; }
    public long? BankId { get; set; }
    public long? CurrencyUnitId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public List<string>? DocumentUrls { get; set; }
}