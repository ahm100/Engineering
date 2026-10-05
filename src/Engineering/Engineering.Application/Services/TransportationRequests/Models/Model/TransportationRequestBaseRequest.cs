namespace Engineering.Application.Services.TransportationRequests.Models.Model;

public record TransportationRequestBaseRequest
{
    public long? TransportationContractorId { get; set; }
    public long? CompanyId { get; set; }
    public string? NumberPlates { get; set; }
    public long? DriverId { get; set; }
    public string? DriverName { get; set; }
    public DateTime? PostageDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public long? BankId { get; set; }
    public long? CurrencyUnitId { get; set; }
    public long? StartingCityId { get; set; }
    public long? DestinationCityId { get; set; }
    public long? TransportationId { get; set; }
    public long? TripId { get; set; }
    public long? MachineTypeId { get; set; }
    public List<long>? CostCenterIds { get; set; }
    public List<long>? ProjectIds { get; set; }
    public List<long>? ProjectOperationIds { get; set; }
    public List<long>? ProjectOperationDetailIds { get; set; }
    public long? TransportationCostCategoryId { get; set; }
    public long? TransportationCostGroupId { get; set; }
    public long? BillOfLadingId { get; set; }
}
